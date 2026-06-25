namespace SGIP.Application.Services;

using SGIP.Application.Interfaces;
using SGIP.Domain.Enums;

public class LoanCalculatorService
{
    private static readonly IReadOnlyDictionary<LoanType, IAmortizationStrategy> Strategies =
        new Dictionary<LoanType, IAmortizationStrategy>
        {
            [LoanType.Fixed] = new FixedAmortizationStrategy(),
            [LoanType.Decreasing] = new DecreasingAmortizationStrategy()
        };

    // TEM = [(1 + TEA)^(1/12)] - 1
    public static decimal ComputeMonthlyEffectiveRate(decimal tea)
        => (decimal)(Math.Pow(1.0 + (double)tea, 1.0 / 12.0) - 1.0);

    public IReadOnlyList<AmortizationEntry> GenerateSchedule(
        decimal amount, int term, decimal tea, LoanType loanType, DateTime startDate)
    {
        decimal tem = ComputeMonthlyEffectiveRate(tea);
        return Strategies[loanType].GenerateSchedule(amount, term, tem, startDate);
    }
}
