namespace SGIP.Application.DTOs;

using SGIP.Domain.Enums;

public record TransactionResponse(
    Guid Id,
    string IdempotencyKey,
    TransactionType Type,
    decimal Amount,
    TransactionStatus Status,
    Guid? LoanId,
    string? Description,
    DateTime CreatedAt
);
