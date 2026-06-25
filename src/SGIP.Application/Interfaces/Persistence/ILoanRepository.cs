namespace SGIP.Application.Interfaces.Persistence;

using SGIP.Domain.Entities;

public interface ILoanRepository
{
    Task<Loan?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Loan?> GetByIdWithScheduleAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<Loan>> GetAllAsync(string? userId = null, CancellationToken ct = default);
    Task<IReadOnlyList<Loan>> GetByUserIdAsync(string userId, CancellationToken ct = default);
    Task<IReadOnlyList<Loan>> GetActiveLoansByUserIdAsync(string userId, CancellationToken ct = default);
    Task<int> CountNonRejectedByUserIdAsync(string userId, CancellationToken ct = default);
    Task AddAsync(Loan loan, CancellationToken ct = default);
    void Update(Loan loan);
}
