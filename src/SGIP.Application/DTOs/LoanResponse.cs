namespace SGIP.Application.DTOs;

using SGIP.Domain.Enums;

public record LoanResponse(
    Guid Id,
    string UserId,
    decimal Amount,
    int Term,
    decimal InterestRate,
    LoanType LoanType,
    LoanStatus Status,
    decimal MonthlyPayment,
    DateTime CreatedAt,

    DateTime? UpdateAt,
    IReadOnlyList<ScheduleEntryDto>? Schedule = null
);
