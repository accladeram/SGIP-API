using System;
using System.Collections.Generic;

namespace SGIP.Application.Interfaces;

public record AmortizationEntry(
    int PaymentNumber,
    DateTime DueDate,
    decimal TotalPayment,
    decimal Principal,
    decimal Interest,
    decimal RemainingBalance
);

public interface IAmortizationStrategy
{
    IReadOnlyList<AmortizationEntry> GenerateSchedule(decimal amount, int term, decimal tem, DateTime startDate);
}
