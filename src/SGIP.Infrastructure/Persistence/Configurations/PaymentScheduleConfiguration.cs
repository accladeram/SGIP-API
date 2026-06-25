using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGIP.Domain.Entities;
using SGIP.Domain.Enums;

namespace SGIP.Infrastructure.Persistence.Configurations;

public class PaymentScheduleConfiguration : IEntityTypeConfiguration<PaymentSchedule>
{
    public void Configure(EntityTypeBuilder<PaymentSchedule> builder)
    {
        builder.ToTable("payment_schedules");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasColumnName("id");

        builder.Property(p => p.LoanId)
            .HasColumnName("loan_id")
            .IsRequired();

        builder.Property(p => p.PaymentNumber)
            .HasColumnName("payment_number")
            .IsRequired();

        builder.Property(p => p.DueDate)
            .HasColumnName("due_date")
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Property(p => p.TotalPayment)
            .HasColumnName("total_payment")
            .HasColumnType("numeric(18,2)")
            .IsRequired();

        builder.Property(p => p.Principal)
            .HasColumnName("principal")
            .HasColumnType("numeric(18,2)")
            .IsRequired();

        builder.Property(p => p.Interest)
            .HasColumnName("interest")
            .HasColumnType("numeric(18,2)")
            .IsRequired();

        builder.Property(p => p.RemainingBalance)
            .HasColumnName("remaining_balance")
            .HasColumnType("numeric(18,2)")
            .IsRequired();

        builder.Property(p => p.Status)
            .HasColumnName("status")
            .HasConversion<int>()
            .IsRequired();

        builder.HasIndex(p => p.LoanId)
            .HasDatabaseName("ix_payment_schedules_loan_id");

        // seed
        var sampleLoanId = Guid.Parse("a3b84f92-12c4-4d81-8b1a-9f4a2153c390");
        var user01LoanId = Guid.Parse("b1c94f02-23d5-4e92-9c2b-af5b3264d401");
        var user02LoanId = Guid.Parse("c2d05f13-34e6-5fa3-ad3c-bf6c4375e512");

        builder.HasData(
            // USR-001 (existing)
            new PaymentSchedule { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), LoanId = sampleLoanId, PaymentNumber = 1, DueDate = DateTime.SpecifyKind(new DateTime(2026, 7, 24, 0, 0, 0), DateTimeKind.Utc), TotalPayment = 344.41m, Principal = 329.11m, Interest = 15.30m, RemainingBalance = 670.89m, Status = ScheduleStatus.Pending },
            new PaymentSchedule { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), LoanId = sampleLoanId, PaymentNumber = 2, DueDate = DateTime.SpecifyKind(new DateTime(2026, 8, 24, 0, 0, 0), DateTimeKind.Utc), TotalPayment = 344.41m, Principal = 334.15m, Interest = 10.26m, RemainingBalance = 336.74m, Status = ScheduleStatus.Pending },
            new PaymentSchedule { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), LoanId = sampleLoanId, PaymentNumber = 3, DueDate = DateTime.SpecifyKind(new DateTime(2026, 9, 24, 0, 0, 0), DateTimeKind.Utc), TotalPayment = 344.41m, Principal = 336.74m, Interest = 7.67m, RemainingBalance = 0.00m, Status = ScheduleStatus.Pending },

            // user-01: Fixed (Francés) — $2000, 6 meses, TEM≈1.5309%
            new PaymentSchedule { Id = Guid.Parse("44444444-4444-4444-4444-444444444441"), LoanId = user01LoanId, PaymentNumber = 1, DueDate = DateTime.SpecifyKind(new DateTime(2026, 7, 25, 0, 0, 0), DateTimeKind.Utc), TotalPayment = 351.42m, Principal = 320.80m, Interest = 30.62m, RemainingBalance = 1679.20m, Status = ScheduleStatus.Pending },
            new PaymentSchedule { Id = Guid.Parse("44444444-4444-4444-4444-444444444442"), LoanId = user01LoanId, PaymentNumber = 2, DueDate = DateTime.SpecifyKind(new DateTime(2026, 8, 25, 0, 0, 0), DateTimeKind.Utc), TotalPayment = 351.42m, Principal = 325.71m, Interest = 25.71m, RemainingBalance = 1353.49m, Status = ScheduleStatus.Pending },
            new PaymentSchedule { Id = Guid.Parse("44444444-4444-4444-4444-444444444443"), LoanId = user01LoanId, PaymentNumber = 3, DueDate = DateTime.SpecifyKind(new DateTime(2026, 9, 25, 0, 0, 0), DateTimeKind.Utc), TotalPayment = 351.42m, Principal = 330.69m, Interest = 20.73m, RemainingBalance = 1022.80m, Status = ScheduleStatus.Pending },
            new PaymentSchedule { Id = Guid.Parse("44444444-4444-4444-4444-444444444444"), LoanId = user01LoanId, PaymentNumber = 4, DueDate = DateTime.SpecifyKind(new DateTime(2026, 10, 25, 0, 0, 0), DateTimeKind.Utc), TotalPayment = 351.42m, Principal = 335.76m, Interest = 15.66m, RemainingBalance = 687.04m, Status = ScheduleStatus.Pending },
            new PaymentSchedule { Id = Guid.Parse("44444444-4444-4444-4444-444444444445"), LoanId = user01LoanId, PaymentNumber = 5, DueDate = DateTime.SpecifyKind(new DateTime(2026, 11, 25, 0, 0, 0), DateTimeKind.Utc), TotalPayment = 351.42m, Principal = 340.90m, Interest = 10.52m, RemainingBalance = 346.14m, Status = ScheduleStatus.Pending },
            new PaymentSchedule { Id = Guid.Parse("44444444-4444-4444-4444-444444444446"), LoanId = user01LoanId, PaymentNumber = 6, DueDate = DateTime.SpecifyKind(new DateTime(2026, 12, 25, 0, 0, 0), DateTimeKind.Utc), TotalPayment = 351.44m, Principal = 346.14m, Interest = 5.30m, RemainingBalance = 0.00m, Status = ScheduleStatus.Pending },

            // user-02: Decreasing (Alemán) — $1800, 6 meses, TEM≈1.5309%
            new PaymentSchedule { Id = Guid.Parse("55555555-5555-5555-5555-555555555551"), LoanId = user02LoanId, PaymentNumber = 1, DueDate = DateTime.SpecifyKind(new DateTime(2026, 7, 25, 0, 0, 0), DateTimeKind.Utc), TotalPayment = 327.56m, Principal = 300.00m, Interest = 27.56m, RemainingBalance = 1500.00m, Status = ScheduleStatus.Pending },
            new PaymentSchedule { Id = Guid.Parse("55555555-5555-5555-5555-555555555552"), LoanId = user02LoanId, PaymentNumber = 2, DueDate = DateTime.SpecifyKind(new DateTime(2026, 8, 25, 0, 0, 0), DateTimeKind.Utc), TotalPayment = 322.96m, Principal = 300.00m, Interest = 22.96m, RemainingBalance = 1200.00m, Status = ScheduleStatus.Pending },
            new PaymentSchedule { Id = Guid.Parse("55555555-5555-5555-5555-555555555553"), LoanId = user02LoanId, PaymentNumber = 3, DueDate = DateTime.SpecifyKind(new DateTime(2026, 9, 25, 0, 0, 0), DateTimeKind.Utc), TotalPayment = 318.37m, Principal = 300.00m, Interest = 18.37m, RemainingBalance = 900.00m, Status = ScheduleStatus.Pending },
            new PaymentSchedule { Id = Guid.Parse("55555555-5555-5555-5555-555555555554"), LoanId = user02LoanId, PaymentNumber = 4, DueDate = DateTime.SpecifyKind(new DateTime(2026, 10, 25, 0, 0, 0), DateTimeKind.Utc), TotalPayment = 313.78m, Principal = 300.00m, Interest = 13.78m, RemainingBalance = 600.00m, Status = ScheduleStatus.Pending },
            new PaymentSchedule { Id = Guid.Parse("55555555-5555-5555-5555-555555555555"), LoanId = user02LoanId, PaymentNumber = 5, DueDate = DateTime.SpecifyKind(new DateTime(2026, 11, 25, 0, 0, 0), DateTimeKind.Utc), TotalPayment = 309.19m, Principal = 300.00m, Interest = 9.19m, RemainingBalance = 300.00m, Status = ScheduleStatus.Pending },
            new PaymentSchedule { Id = Guid.Parse("55555555-5555-5555-5555-555555555556"), LoanId = user02LoanId, PaymentNumber = 6, DueDate = DateTime.SpecifyKind(new DateTime(2026, 12, 25, 0, 0, 0), DateTimeKind.Utc), TotalPayment = 304.59m, Principal = 300.00m, Interest = 4.59m, RemainingBalance = 0.00m, Status = ScheduleStatus.Pending }
        );
    }
}