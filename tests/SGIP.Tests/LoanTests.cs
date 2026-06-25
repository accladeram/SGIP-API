using SGIP.Application.Services;
using SGIP.Domain.Enums;
using SGIP.Domain.Validation;
using Xunit;

namespace SGIP.Tests;

public class LoanTests
{
    private static readonly LoanCalculatorService Calculator = new();
    private static readonly DateTime Start = new(2026, 6, 25, 0, 0, 0, DateTimeKind.Utc);

    // 1. TEM: (1 + TEA)^(1/12) - 1
    [Fact]
    public void ComputeMonthlyEffectiveRate_Tea20_ReturnsExpectedTem()
    {
        decimal tem = LoanCalculatorService.ComputeMonthlyEffectiveRate(0.20m);
        Assert.InRange(tem, 0.0153m, 0.0154m);
    }

    // 2. Fixed (Francés): saldo final debe ser 0
    [Fact]
    public void GenerateSchedule_Fixed_LastRemainingBalanceIsZero()
    {
        var schedule = Calculator.GenerateSchedule(5000m, 12, 0.20m, LoanType.Fixed, Start);

        Assert.Equal(0m, schedule[^1].RemainingBalance);
    }

    // 3. Fixed (Francés): todas las cuotas deben ser iguales (excepto posible redondeo en última)
    [Fact]
    public void GenerateSchedule_Fixed_AllPaymentsEqual()
    {
        var schedule = Calculator.GenerateSchedule(3000m, 6, 0.20m, LoanType.Fixed, Start);
        decimal first = schedule[0].TotalPayment;

        // Tolerancia de 1 centavo por redondeo en la última cuota
        foreach (var entry in schedule.Take(schedule.Count - 1))
            Assert.Equal(first, entry.TotalPayment);
    }

    // 4. Decreasing (Alemán): amortización constante = Monto / Plazo
    [Fact]
    public void GenerateSchedule_Decreasing_PrincipalIsConstant()
    {
        decimal amount = 1800m;
        int term = 6;
        var schedule = Calculator.GenerateSchedule(amount, term, 0.20m, LoanType.Decreasing, Start);
        decimal expectedPrincipal = Math.Round(amount / term, 2);

        foreach (var entry in schedule)
            Assert.Equal(expectedPrincipal, entry.Principal);
    }

    // 5. Validación: monto fuera de rango lanza excepción
    [Theory]
    [InlineData(499)]
    [InlineData(50001)]
    public void EnsureValidLimits_InvalidAmount_Throws(decimal amount)
    {
        Assert.Throws<InvalidOperationException>(() =>
            LoanValidatorExtensions.EnsureValidLimits(amount, 12));
    }
}
