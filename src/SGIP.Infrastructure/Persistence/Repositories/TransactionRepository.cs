namespace SGIP.Infrastructure.Persistence.Repositories;

using Microsoft.EntityFrameworkCore;
using SGIP.Application.Interfaces.Persistence;
using SGIP.Domain.Entities;

public class TransactionRepository(ApplicationDbContext context) : ITransactionRepository
{
    public async Task<Transaction?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await context.Transactions.FindAsync([id], ct);

    public async Task<Transaction?> GetByIdempotencyKeyAsync(string key, CancellationToken ct = default)
        => await context.Transactions
            .FirstOrDefaultAsync(t => t.IdempotencyKey == key, ct);

    public async Task<IReadOnlyList<Transaction>> GetByLoanIdAsync(Guid loanId, CancellationToken ct = default)
        => await context.Transactions
            .Where(t => t.LoanId == loanId)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync(ct);

    public async Task<IReadOnlyList<Transaction>> GetAllAsync(Guid? loanId = null, CancellationToken ct = default)
        => await context.Transactions
            .Where(t => loanId == null || t.LoanId == loanId)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync(ct);

    public async Task AddAsync(Transaction transaction, CancellationToken ct = default)
        => await context.Transactions.AddAsync(transaction, ct);

    public void Update(Transaction transaction)
        => context.Transactions.Update(transaction);
}
