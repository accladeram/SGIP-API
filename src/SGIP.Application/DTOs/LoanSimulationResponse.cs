namespace SGIP.Application.DTOs;

using SGIP.Domain.Enums;

public record LoanSimulationResponse(
    decimal Amount,
    int Term,
    decimal InterestRate,
    LoanType LoanType,
    decimal MonthlyPayment,
    decimal TotalInterest,
    decimal TotalPayment,
    bool MeetsRiskPolicy,
    IReadOnlyList<ScheduleEntryDto> Schedule
);
