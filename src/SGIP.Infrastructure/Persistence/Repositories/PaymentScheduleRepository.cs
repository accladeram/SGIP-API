namespace SGIP.Infrastructure.Persistence.Repositories;

using Microsoft.EntityFrameworkCore;
using SGIP.Application.Interfaces.Persistence;
using SGIP.Domain.Entities;
using SGIP.Domain.Enums;

public class PaymentScheduleRepository(ApplicationDbContext context) : IPaymentScheduleRepository
{
    public async Task<PaymentSchedule?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await context.PaymentSchedules.FindAsync([id], ct);

    public async Task<IReadOnlyList<PaymentSchedule>> GetByLoanIdAsync(Guid loanId, CancellationToken ct = default)
        => await context.PaymentSchedules
            .Where(ps => ps.LoanId == loanId)
            .OrderBy(ps => ps.PaymentNumber)
            .ToListAsync(ct);

    public async Task AddRangeAsync(IEnumerable<PaymentSchedule> schedules, CancellationToken ct = default)
        => await context.PaymentSchedules.AddRangeAsync(schedules, ct);

    public async Task<PaymentSchedule?> GetNextPendingByLoanIdAsync(Guid loanId, CancellationToken ct = default)
        => await context.PaymentSchedules
            .Where(ps => ps.LoanId == loanId && ps.Status == ScheduleStatus.Pending)
            .OrderBy(ps => ps.PaymentNumber)
            .FirstOrDefaultAsync(ct);

    public void Update(PaymentSchedule schedule)
        => context.PaymentSchedules.Update(schedule);
}
