using SGIP.Application.Interfaces;

namespace SGIP.Application.Services;


public class FixedAmortizationStrategy : IAmortizationStrategy
{
    // Sistema Francés: cuota fija = Amount * [TEM * (1+TEM)^n] / [(1+TEM)^n - 1]
    public IReadOnlyList<AmortizationEntry> GenerateSchedule(decimal amount, int term, decimal tem, DateTime startDate)
    {
        double dTem = (double)tem;
        double temPowN = Math.Pow(1.0 + dTem, term);
        decimal monthlyPayment = Math.Round(
            (decimal)((double)amount * (dTem * temPowN) / (temPowN - 1.0)),
            2, MidpointRounding.AwayFromZero);

        var entries = new List<AmortizationEntry>(term);
        decimal remaining = amount;

        for (int i = 1; i <= term; i++)
        {
            decimal interest = Math.Round(remaining * tem, 2, MidpointRounding.AwayFromZero);
            decimal principal;
            decimal payment;

            if (i == term)
            {
                principal = remaining;
                payment = Math.Round(principal + interest, 2, MidpointRounding.AwayFromZero);
            }
            else
            {
                principal = Math.Round(monthlyPayment - interest, 2, MidpointRounding.AwayFromZero);
                payment = monthlyPayment;
            }

            remaining = Math.Round(remaining - principal, 2, MidpointRounding.AwayFromZero);
            if (remaining < 0m) remaining = 0m;

            entries.Add(new AmortizationEntry(
                i,
                startDate.AddMonths(i),
                payment,
                principal,
                interest,
                remaining));
        }

        return entries;
    }
}
