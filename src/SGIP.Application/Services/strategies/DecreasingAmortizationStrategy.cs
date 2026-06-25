using SGIP.Application.Interfaces;

namespace SGIP.Application.Services;

// Sistema Aleman
// Amortización = Monto / n .
// Cuota = Amortización + Interés.
public class DecreasingAmortizationStrategy : IAmortizationStrategy
{
    public IReadOnlyList<AmortizationEntry> GenerateSchedule(decimal amount, int term, decimal tem, DateTime startDate)
    {
        decimal principalPayment = Math.Round(amount / term, 2, MidpointRounding.AwayFromZero);

        var entries = new List<AmortizationEntry>(term);
        decimal remaining = amount;

        for (int i = 1; i <= term; i++)
        {
            decimal interest = Math.Round(remaining * tem, 2, MidpointRounding.AwayFromZero);
            decimal principal = i == term ? remaining : principalPayment;
            decimal totalPayment = Math.Round(principal + interest, 2, MidpointRounding.AwayFromZero);

            remaining = Math.Round(remaining - principal, 2, MidpointRounding.AwayFromZero);
            if (remaining < 0m) remaining = 0m;

            entries.Add(new AmortizationEntry(
                i,
                startDate.AddMonths(i),
                totalPayment,
                principal,
                interest,
                remaining));
        }

        return entries;
    }
}
