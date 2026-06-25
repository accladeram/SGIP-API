namespace SGIP.Application.Services;

using SGIP.Application.DTOs;
using SGIP.Application.Interfaces.Persistence;
using SGIP.Application.Interfaces.Services;
using SGIP.Domain.Entities;
using SGIP.Domain.Enums;
using SGIP.Domain.Validation;

public class LoanService(IUnitOfWork unitOfWork, LoanCalculatorService calculator) : ILoanService
{
    private const decimal TEA = 0.24m;


    public LoanSimulationResponse SimulateLoan(LoanSimulationRequest request)
    {
        LoanValidatorExtensions.EnsureValidLimits(request.Amount, request.Term);

        var entries = calculator.GenerateSchedule(
            request.Amount, request.Term, TEA, request.LoanType, DateTime.UtcNow);

        decimal firstPayment = entries[0].TotalPayment;
        decimal totalPayment = entries.Sum(e => e.TotalPayment);
        decimal totalInterest = entries.Sum(e => e.Interest);
        bool meetsRiskPolicy = firstPayment <= request.MonthlyIncome * 0.30m;

        var schedule = entries
            .Select(e => new ScheduleEntryDto(
                e.PaymentNumber, e.DueDate, e.TotalPayment, e.Principal, e.Interest, e.RemainingBalance))
            .ToList();

        return new LoanSimulationResponse(
            request.Amount, request.Term, TEA, request.LoanType,
            firstPayment, totalInterest, totalPayment,
            meetsRiskPolicy, schedule);
    }

    public async Task<LoanResponse> ApplyForLoanAsync(LoanApplicationRequest request, CancellationToken ct)
    {
        LoanValidatorExtensions.EnsureValidLimits(request.Amount, request.Term);

        var nonRejectedCount = await unitOfWork.Loans.CountNonRejectedByUserIdAsync(request.UserId, ct);
        if (nonRejectedCount >= 3)
            throw new InvalidOperationException("El cliente no puede tener más de 3 préstamos activos simultáneamente");

        var scheduleEntries = calculator.GenerateSchedule(
            request.Amount, request.Term, TEA, request.LoanType, DateTime.UtcNow);

        decimal newMonthlyPayment = scheduleEntries[0].TotalPayment;

        var activeLoans = await unitOfWork.Loans.GetActiveLoansByUserIdAsync(request.UserId, ct);
        decimal existingObligations = activeLoans.Sum(l => l.MonthlyPayment);

        if (existingObligations + newMonthlyPayment > request.MonthlyIncome * 0.40m)
            throw new InvalidOperationException(
                "La suma de cuotas de todos sus préstamos no puede exceder el 40% de sus ingresos mensuales");

        var loanId = Guid.NewGuid();
        bool autoApprove = request.Amount < 10_000m && nonRejectedCount < 2;

        var loan = new Loan
        {
            Id = loanId,
            UserId = request.UserId,
            Amount = request.Amount,
            Term = request.Term,
            InterestRate = TEA,
            LoanType = request.LoanType,
            Status = LoanStatus.Pending,
            MonthlyPayment = newMonthlyPayment,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var paymentSchedules = scheduleEntries.Select(e => new PaymentSchedule
        {
            Id = Guid.NewGuid(),
            LoanId = loanId,
            PaymentNumber = e.PaymentNumber,
            DueDate = DateTime.SpecifyKind(e.DueDate, DateTimeKind.Utc),
            TotalPayment = e.TotalPayment,
            Principal = e.Principal,
            Interest = e.Interest,
            RemainingBalance = e.RemainingBalance,
            Status = ScheduleStatus.Pending
        }).ToList();

        await unitOfWork.Loans.AddAsync(loan, ct);
        await unitOfWork.PaymentSchedules.AddRangeAsync(paymentSchedules, ct);

        if (autoApprove)
        {
            loan.Status = LoanStatus.Approved;
            loan.UpdatedAt = DateTime.UtcNow;

            var disbursement = new Transaction
            {
                Id = Guid.NewGuid(),
                IdempotencyKey = $"auto-disburse-{loanId}",
                Type = TransactionType.Disbursement,
                Amount = loan.Amount,
                Status = TransactionStatus.Completed,
                LoanId = loanId,
                Description = $"Desembolso automático - {loanId}",
                CreatedAt = DateTime.UtcNow
            };

            await unitOfWork.Transactions.AddAsync(disbursement, ct);

            loan.Status = LoanStatus.Active;
            loan.UpdatedAt = DateTime.UtcNow;
        }

        await unitOfWork.SaveChangesAsync(ct);

        var scheduleDtos = paymentSchedules
            .Select(s => new ScheduleEntryDto(
                s.PaymentNumber, s.DueDate, s.TotalPayment, s.Principal, s.Interest, s.RemainingBalance))
            .ToList();

        return new LoanResponse(
            loan.Id, loan.UserId, loan.Amount, loan.Term,
            loan.InterestRate, loan.LoanType, loan.Status,
            loan.MonthlyPayment, loan.CreatedAt, loan.UpdatedAt, scheduleDtos);
    }

    public async Task<LoanResponse> ApproveLoanAsync(Guid loanId, string idempotencyKey, CancellationToken ct)
    {
        var existingTx = await unitOfWork.Transactions.GetByIdempotencyKeyAsync(idempotencyKey, ct);
        if (existingTx != null)
        {
            var cachedLoan = await unitOfWork.Loans.GetByIdAsync(existingTx.LoanId ?? loanId, ct);
            if (cachedLoan != null)
                return MapToResponse(cachedLoan);
        }

        var loan = await unitOfWork.Loans.GetByIdAsync(loanId, ct)
            ?? throw new KeyNotFoundException($"Prestamo {loanId} no encontrado.");

        if (loan.Status != LoanStatus.Pending)
            throw new InvalidOperationException($"No se puede aprobar el prestamo con el estado '{loan.Status}'.");

        loan.Status = LoanStatus.Approved;
        loan.UpdatedAt = DateTime.UtcNow;

        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            IdempotencyKey = idempotencyKey,
            Type = TransactionType.Disbursement,
            Amount = loan.Amount,
            Status = TransactionStatus.Completed,
            LoanId = loanId,
            Description = $"Desembolso - {loanId}",
            CreatedAt = DateTime.UtcNow
        };

        unitOfWork.Loans.Update(loan);
        await unitOfWork.Transactions.AddAsync(transaction, ct);
        await unitOfWork.SaveChangesAsync(ct);

        return MapToResponse(loan);
    }

    public async Task<LoanResponse> RejectLoanAsync(Guid loanId, CancellationToken ct)
    {
        var loan = await unitOfWork.Loans.GetByIdAsync(loanId, ct)
            ?? throw new KeyNotFoundException($"No encontrado {loanId}");

        if (loan.Status != LoanStatus.Pending)
            throw new InvalidOperationException($"No se puede rechazar el pr�stamo con el estado: '{loan.Status}'.");

        loan.Status = LoanStatus.Rejected;
        loan.UpdatedAt = DateTime.UtcNow;

        unitOfWork.Loans.Update(loan);
        await unitOfWork.SaveChangesAsync(ct);

        return MapToResponse(loan);
    }

    public async Task<IReadOnlyList<LoanResponse>> GetAllAsync(string? userId, CancellationToken ct)
    {
        var loans = await unitOfWork.Loans.GetAllAsync(userId, ct);
        return loans.Select(MapToResponse).ToList();
    }

    public async Task<LoanResponse> GetLoanWithScheduleAsync(Guid id, CancellationToken ct)
    {
        var loan = await unitOfWork.Loans.GetByIdWithScheduleAsync(id, ct)
            ?? throw new KeyNotFoundException($"Prestamo {id} no encontrado.");

        var schedule = loan.PaymentSchedules
            .OrderBy(s => s.PaymentNumber)
            .Select(s => new ScheduleEntryDto(
                s.PaymentNumber, s.DueDate, s.TotalPayment, s.Principal, s.Interest, s.RemainingBalance))
            .ToList();

        return new LoanResponse(
            loan.Id, loan.UserId, loan.Amount, loan.Term,
            loan.InterestRate, loan.LoanType, loan.Status,
            loan.MonthlyPayment, loan.CreatedAt, loan.UpdatedAt, schedule );
    }

    private static LoanResponse MapToResponse(Loan loan) => new(
        loan.Id, loan.UserId, loan.Amount, loan.Term,
        loan.InterestRate, loan.LoanType, loan.Status,
        loan.MonthlyPayment, loan.CreatedAt, loan.UpdatedAt);

    
}
