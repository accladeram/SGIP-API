namespace SGIP.Application.DTOs;

public record ScheduleEntryDto(
    int PaymentNumber,
    DateTime DueDate,
    decimal TotalPayment,
    decimal Principal,
    decimal Interest,
    decimal RemainingBalance
);
