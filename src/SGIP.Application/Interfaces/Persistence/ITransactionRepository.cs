namespace SGIP.Application.Interfaces.Persistence;

using SGIP.Domain.Entities;

public interface ITransactionRepository
{
    Task<Transaction?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Transaction?> GetByIdempotencyKeyAsync(string key, CancellationToken ct = default);
    Task<IReadOnlyList<Transaction>> GetByLoanIdAsync(Guid loanId, CancellationToken ct = default);
    Task<IReadOnlyList<Transaction>> GetAllAsync(Guid? loanId = null, CancellationToken ct = default);
    Task AddAsync(Transaction transaction, CancellationToken ct = default);
    void Update(Transaction transaction);
}
