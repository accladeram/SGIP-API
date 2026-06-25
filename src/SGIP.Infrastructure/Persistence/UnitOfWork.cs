namespace SGIP.Infrastructure.Persistence;

using SGIP.Application.Interfaces.Persistence;

public class UnitOfWork(
    ApplicationDbContext context,
    ILoanRepository loans,
    IPaymentScheduleRepository paymentSchedules,
    ITransactionRepository transactions) : IUnitOfWork
{
    public ILoanRepository Loans => loans;
    public IPaymentScheduleRepository PaymentSchedules => paymentSchedules;
    public ITransactionRepository Transactions => transactions;

    public async Task<int> SaveChangesAsync(CancellationToken ct = default)
        => await context.SaveChangesAsync(ct);
}
