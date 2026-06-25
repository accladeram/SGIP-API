using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGIP.Domain.Entities;
using SGIP.Domain.Enums;

namespace SGIP.Infrastructure.Persistence.Configurations;

public class LoanConfiguration : IEntityTypeConfiguration<Loan>
{
    public void Configure(EntityTypeBuilder<Loan> builder)
    {
        builder.ToTable("loans");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Id)
            .HasColumnName("id");

        builder.Property(l => l.UserId)
            .HasColumnName("user_id")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(l => l.Amount)
            .HasColumnName("amount")
            .HasColumnType("numeric(18,2)")
            .IsRequired();

        builder.Property(l => l.Term)
            .HasColumnName("term")
            .IsRequired();

        builder.Property(l => l.InterestRate)
            .HasColumnName("interest_rate")
            .HasColumnType("numeric(5,4)")
            .IsRequired();

        builder.Property(l => l.LoanType)
            .HasColumnName("loan_type")
            .HasConversion<int>() 
            .IsRequired();

        builder.Property(l => l.Status)
            .HasColumnName("status")
            .HasConversion<int>()
            .IsRequired();

        builder.Property(l => l.MonthlyPayment)
            .HasColumnName("monthly_payment")
            .HasColumnType("numeric(18,2)")
            .IsRequired();

        builder.Property(l => l.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Property(l => l.UpdatedAt)
            .HasColumnName("updated_at")
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.HasIndex(l => l.UserId)
            .HasDatabaseName("ix_loans_user_id");

        builder.HasMany(l => l.PaymentSchedules)
            .WithOne(p => p.Loan)
            .HasForeignKey(p => p.LoanId)
            .OnDelete(DeleteBehavior.Cascade); 

        // seed
        var sampleLoanId = Guid.Parse("a3b84f92-12c4-4d81-8b1a-9f4a2153c390");
        var user01LoanId = Guid.Parse("b1c94f02-23d5-4e92-9c2b-af5b3264d401");
        var user02LoanId = Guid.Parse("c2d05f13-34e6-5fa3-ad3c-bf6c4375e512");

        builder.HasData(
            new Loan
            {
                Id = sampleLoanId,
                UserId = "USR-001",
                Amount = 1000.00m,
                Term = 3,
                InterestRate = 0.20m,
                LoanType = LoanType.Fixed,
                Status = LoanStatus.Active,
                MonthlyPayment = 344.41m,
                CreatedAt = DateTime.SpecifyKind(new DateTime(2026, 6, 24, 0, 0, 0), DateTimeKind.Utc),
                UpdatedAt = DateTime.SpecifyKind(new DateTime(2026, 6, 24, 0, 0, 0), DateTimeKind.Utc)
            },
            new Loan
            {
                Id = user01LoanId,
                UserId = "user-01",
                Amount = 2000.00m,
                Term = 6,
                InterestRate = 0.20m,
                LoanType = LoanType.Fixed,
                Status = LoanStatus.Active,
                MonthlyPayment = 351.42m,
                CreatedAt = DateTime.SpecifyKind(new DateTime(2026, 6, 25, 0, 0, 0), DateTimeKind.Utc),
                UpdatedAt = DateTime.SpecifyKind(new DateTime(2026, 6, 25, 0, 0, 0), DateTimeKind.Utc)
            },
            new Loan
            {
                Id = user02LoanId,
                UserId = "user-02",
                Amount = 1800.00m,
                Term = 6,
                InterestRate = 0.20m,
                LoanType = LoanType.Decreasing,
                Status = LoanStatus.Active,
                MonthlyPayment = 327.56m,
                CreatedAt = DateTime.SpecifyKind(new DateTime(2026, 6, 25, 0, 0, 0), DateTimeKind.Utc),
                UpdatedAt = DateTime.SpecifyKind(new DateTime(2026, 6, 25, 0, 0, 0), DateTimeKind.Utc)
            }
        );
    }
}