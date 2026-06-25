namespace SGIP.Infrastructure.Persistence.Repositories;

using Microsoft.EntityFrameworkCore;
using SGIP.Application.Interfaces.Persistence;
using SGIP.Domain.Entities;
using SGIP.Domain.Enums;

public class LoanRepository(ApplicationDbContext context) : ILoanRepository
{
    public async Task<Loan?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await context.Loans.FindAsync([id], ct);

    public async Task<IReadOnlyList<Loan>> GetAllAsync(string? userId = null, CancellationToken ct = default)
        => await context.Loans
            .Where(l => userId == null || l.UserId == userId)
            .OrderByDescending(l => l.CreatedAt)
            .ToListAsync(ct);

    public async Task<IReadOnlyList<Loan>> GetByUserIdAsync(string userId, CancellationToken ct = default)
        => await context.Loans
            .Where(l => l.UserId == userId)
            .OrderByDescending(l => l.CreatedAt)
            .ToListAsync(ct);

    public async Task<IReadOnlyList<Loan>> GetActiveLoansByUserIdAsync(string userId, CancellationToken ct = default)
        => await context.Loans
            .Where(l => l.UserId == userId && l.Status == LoanStatus.Active)
            .ToListAsync(ct);

    public async Task<int> CountNonRejectedByUserIdAsync(string userId, CancellationToken ct = default)
        => await context.Loans
            .CountAsync(l => l.UserId == userId && l.Status != LoanStatus.Rejected, ct);

    public async Task<Loan?> GetByIdWithScheduleAsync(Guid id, CancellationToken ct = default)
        => await context.Loans
            .Include(l => l.PaymentSchedules)
            .FirstOrDefaultAsync(l => l.Id == id, ct);

    public async Task AddAsync(Loan loan, CancellationToken ct = default)
        => await context.Loans.AddAsync(loan, ct);

    public void Update(Loan loan)
        => context.Loans.Update(loan);
}
