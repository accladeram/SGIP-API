namespace SGIP.Application.Interfaces.Persistence;

using SGIP.Domain.Entities;

public interface IPaymentScheduleRepository
{
    Task<PaymentSchedule?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<PaymentSchedule>> GetByLoanIdAsync(Guid loanId, CancellationToken ct = default);
    Task AddRangeAsync(IEnumerable<PaymentSchedule> schedules, CancellationToken ct = default);
    Task<PaymentSchedule?> GetNextPendingByLoanIdAsync(Guid loanId, CancellationToken ct = default);
    void Update(PaymentSchedule schedule);
}
