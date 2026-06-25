namespace SGIP.Domain.Entities;

using SGIP.Domain.Enums;
using System.ComponentModel.DataAnnotations;

public class Transaction
{
    [Key]
    public Guid Id { get; set; }
    public required string IdempotencyKey { get; set; } 
    public TransactionType Type { get; set; }
    public decimal Amount { get; set; }
    public TransactionStatus Status { get; set; }
    public Guid? LoanId { get; set; } 
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
