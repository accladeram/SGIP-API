namespace SGIP.Application.DTOs;

using SGIP.Domain.Enums;

public record CreateTransactionRequest(
    string IdempotencyKey,
    TransactionType Type,
    decimal Amount,
    Guid? LoanId,
    string? Description
);
