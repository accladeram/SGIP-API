namespace SGIP.Application.Interfaces.Services;

using SGIP.Application.DTOs;

public interface ILoanService
{
    LoanSimulationResponse SimulateLoan(LoanSimulationRequest request);
    Task<LoanResponse> ApplyForLoanAsync(LoanApplicationRequest request, CancellationToken ct);
    Task<LoanResponse> ApproveLoanAsync(Guid loanId, string idempotencyKey, CancellationToken ct);
    Task<LoanResponse> RejectLoanAsync(Guid loanId, CancellationToken ct);
    Task<IReadOnlyList<LoanResponse>> GetAllAsync(string? userId, CancellationToken ct);
    Task<LoanResponse> GetLoanWithScheduleAsync(Guid id, CancellationToken ct);
}
