namespace SGIP.Domain.Entities;

using SGIP.Domain.Enums;
using System.ComponentModel.DataAnnotations;

public class Loan
{
    [Key]
    public Guid Id { get; set; }
    public required string UserId { get; set; } 
    public decimal Amount { get; set; }
    public int Term { get; set; }
    public decimal InterestRate { get; set; } // TEA
    public LoanType LoanType { get; set; }
    public LoanStatus Status { get; set; }
    public decimal MonthlyPayment { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public virtual ICollection<PaymentSchedule> PaymentSchedules { get; set; } = new List<PaymentSchedule>();
}
