namespace SGIP.Application.Interfaces.Services;

using SGIP.Application.DTOs;

public interface ITransactionService
{
    Task<TransactionResponse> CreateAsync(CreateTransactionRequest request, CancellationToken ct);
    Task<IReadOnlyList<TransactionResponse>> GetAllAsync(Guid? loanId, CancellationToken ct);
    Task<TransactionResponse> GetByIdAsync(Guid id, CancellationToken ct);
}
