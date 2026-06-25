namespace SGIP.Application.Interfaces.Persistence;

public interface IUnitOfWork
{
    ILoanRepository Loans { get; }
    IPaymentScheduleRepository PaymentSchedules { get; }
    ITransactionRepository Transactions { get; }
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
