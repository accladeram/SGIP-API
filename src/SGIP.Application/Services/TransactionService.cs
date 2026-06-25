namespace SGIP.Application.Services;

using Microsoft.EntityFrameworkCore;
using SGIP.Application.DTOs;
using SGIP.Application.Interfaces.Persistence;
using SGIP.Application.Interfaces.Services;
using SGIP.Domain.Entities;
using SGIP.Domain.Enums;

public class TransactionService(IUnitOfWork unitOfWork) : ITransactionService
{
    public async Task<TransactionResponse> CreateAsync(CreateTransactionRequest request, CancellationToken ct)
    {
        // Phase 1: Atomic registration — try to INSERT; catch Postgres unique-key violation (SQLSTATE 23505)
        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            IdempotencyKey = request.IdempotencyKey,
            Type = request.Type,
            Amount = request.Amount,
            Status = TransactionStatus.Pending,
            LoanId = request.LoanId,
            Description = request.Description,
            CreatedAt = DateTime.UtcNow
        };

        try
        {
            await unitOfWork.Transactions.AddAsync(transaction, ct);
            await unitOfWork.SaveChangesAsync(ct);
        }
        catch (DbUpdateException ex) when (ex.InnerException?.Message.Contains("23505") == true)
        {
            var existing = await unitOfWork.Transactions.GetByIdempotencyKeyAsync(request.IdempotencyKey, ct);
            return MapToResponse(existing!);
        }

        // Phase 2: Financial logic — modifies related entities in the change tracker (no intermediate save)
        try
        {
            await ProcessFinancialLogicAsync(transaction, ct);
            transaction.Status = TransactionStatus.Completed;
        }
        catch
        {
            transaction.Status = TransactionStatus.Failed;
        }

        // Phase 3: Persist final status along with any related-entity changes from Phase 2
        unitOfWork.Transactions.Update(transaction);
        await unitOfWork.SaveChangesAsync(ct);

        return MapToResponse(transaction);
    }

    public async Task<IReadOnlyList<TransactionResponse>> GetAllAsync(Guid? loanId, CancellationToken ct)
    {
        var transactions = await unitOfWork.Transactions.GetAllAsync(loanId, ct);
        return transactions.Select(MapToResponse).ToList();
    }

    public async Task<TransactionResponse> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var transaction = await unitOfWork.Transactions.GetByIdAsync(id, ct)
            ?? throw new KeyNotFoundException($"Transaccion no encontrada {id}");
        return MapToResponse(transaction);
    }

    private async Task ProcessFinancialLogicAsync(Transaction transaction, CancellationToken ct)
    {
        switch (transaction.Type)
        {
            case TransactionType.Disbursement:
            {
                if (!transaction.LoanId.HasValue)
                    throw new InvalidOperationException("Disbursement requires a LoanId.");
                var loan = await unitOfWork.Loans.GetByIdAsync(transaction.LoanId.Value, ct)
                    ?? throw new KeyNotFoundException($"Loan {transaction.LoanId.Value} not found.");
                loan.Status = LoanStatus.Active;
                loan.UpdatedAt = DateTime.UtcNow;
                unitOfWork.Loans.Update(loan);
                break;
            }
            case TransactionType.Payment:
            {
                if (!transaction.LoanId.HasValue)
                    throw new InvalidOperationException("Payment requires a LoanId.");
                var schedule = await unitOfWork.PaymentSchedules.GetNextPendingByLoanIdAsync(transaction.LoanId.Value, ct)
                    ?? throw new InvalidOperationException($"No pending payment schedule found for loan {transaction.LoanId.Value}.");
                schedule.Status = ScheduleStatus.Paid;
                unitOfWork.PaymentSchedules.Update(schedule);
                break;
            }
        }
    }

    private static TransactionResponse MapToResponse(Transaction t) => new(
        t.Id, t.IdempotencyKey, t.Type, t.Amount, t.Status, t.LoanId, t.Description, t.CreatedAt);
}
