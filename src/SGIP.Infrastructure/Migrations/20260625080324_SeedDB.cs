using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SGIP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "loans",
                columns: new[] { "id", "amount", "created_at", "interest_rate", "loan_type", "monthly_payment", "status", "term", "updated_at", "user_id" },
                values: new object[,]
                {
                    { new Guid("b1c94f02-23d5-4e92-9c2b-af5b3264d401"), 2000.00m, new DateTime(2026, 6, 25, 0, 0, 0, 0, DateTimeKind.Utc), 0.20m, 0, 351.42m, 3, 6, new DateTime(2026, 6, 25, 0, 0, 0, 0, DateTimeKind.Utc), "user-01" },
                    { new Guid("c2d05f13-34e6-5fa3-ad3c-bf6c4375e512"), 1800.00m, new DateTime(2026, 6, 25, 0, 0, 0, 0, DateTimeKind.Utc), 0.20m, 1, 327.56m, 3, 6, new DateTime(2026, 6, 25, 0, 0, 0, 0, DateTimeKind.Utc), "user-02" }
                });

            migrationBuilder.InsertData(
                table: "payment_schedules",
                columns: new[] { "id", "due_date", "interest", "loan_id", "payment_number", "principal", "remaining_balance", "status", "total_payment" },
                values: new object[,]
                {
                    { new Guid("44444444-4444-4444-4444-444444444441"), new DateTime(2026, 7, 25, 0, 0, 0, 0, DateTimeKind.Utc), 30.62m, new Guid("b1c94f02-23d5-4e92-9c2b-af5b3264d401"), 1, 320.80m, 1679.20m, 0, 351.42m },
                    { new Guid("44444444-4444-4444-4444-444444444442"), new DateTime(2026, 8, 25, 0, 0, 0, 0, DateTimeKind.Utc), 25.71m, new Guid("b1c94f02-23d5-4e92-9c2b-af5b3264d401"), 2, 325.71m, 1353.49m, 0, 351.42m },
                    { new Guid("44444444-4444-4444-4444-444444444443"), new DateTime(2026, 9, 25, 0, 0, 0, 0, DateTimeKind.Utc), 20.73m, new Guid("b1c94f02-23d5-4e92-9c2b-af5b3264d401"), 3, 330.69m, 1022.80m, 0, 351.42m },
                    { new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2026, 10, 25, 0, 0, 0, 0, DateTimeKind.Utc), 15.66m, new Guid("b1c94f02-23d5-4e92-9c2b-af5b3264d401"), 4, 335.76m, 687.04m, 0, 351.42m },
                    { new Guid("44444444-4444-4444-4444-444444444445"), new DateTime(2026, 11, 25, 0, 0, 0, 0, DateTimeKind.Utc), 10.52m, new Guid("b1c94f02-23d5-4e92-9c2b-af5b3264d401"), 5, 340.90m, 346.14m, 0, 351.42m },
                    { new Guid("44444444-4444-4444-4444-444444444446"), new DateTime(2026, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), 5.30m, new Guid("b1c94f02-23d5-4e92-9c2b-af5b3264d401"), 6, 346.14m, 0.00m, 0, 351.44m },
                    { new Guid("55555555-5555-5555-5555-555555555551"), new DateTime(2026, 7, 25, 0, 0, 0, 0, DateTimeKind.Utc), 27.56m, new Guid("c2d05f13-34e6-5fa3-ad3c-bf6c4375e512"), 1, 300.00m, 1500.00m, 0, 327.56m },
                    { new Guid("55555555-5555-5555-5555-555555555552"), new DateTime(2026, 8, 25, 0, 0, 0, 0, DateTimeKind.Utc), 22.96m, new Guid("c2d05f13-34e6-5fa3-ad3c-bf6c4375e512"), 2, 300.00m, 1200.00m, 0, 322.96m },
                    { new Guid("55555555-5555-5555-5555-555555555553"), new DateTime(2026, 9, 25, 0, 0, 0, 0, DateTimeKind.Utc), 18.37m, new Guid("c2d05f13-34e6-5fa3-ad3c-bf6c4375e512"), 3, 300.00m, 900.00m, 0, 318.37m },
                    { new Guid("55555555-5555-5555-5555-555555555554"), new DateTime(2026, 10, 25, 0, 0, 0, 0, DateTimeKind.Utc), 13.78m, new Guid("c2d05f13-34e6-5fa3-ad3c-bf6c4375e512"), 4, 300.00m, 600.00m, 0, 313.78m },
                    { new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2026, 11, 25, 0, 0, 0, 0, DateTimeKind.Utc), 9.19m, new Guid("c2d05f13-34e6-5fa3-ad3c-bf6c4375e512"), 5, 300.00m, 300.00m, 0, 309.19m },
                    { new Guid("55555555-5555-5555-5555-555555555556"), new DateTime(2026, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), 4.59m, new Guid("c2d05f13-34e6-5fa3-ad3c-bf6c4375e512"), 6, 300.00m, 0.00m, 0, 304.59m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "payment_schedules",
                keyColumn: "id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444441"));

            migrationBuilder.DeleteData(
                table: "payment_schedules",
                keyColumn: "id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444442"));

            migrationBuilder.DeleteData(
                table: "payment_schedules",
                keyColumn: "id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444443"));

            migrationBuilder.DeleteData(
                table: "payment_schedules",
                keyColumn: "id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "payment_schedules",
                keyColumn: "id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444445"));

            migrationBuilder.DeleteData(
                table: "payment_schedules",
                keyColumn: "id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444446"));

            migrationBuilder.DeleteData(
                table: "payment_schedules",
                keyColumn: "id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555551"));

            migrationBuilder.DeleteData(
                table: "payment_schedules",
                keyColumn: "id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555552"));

            migrationBuilder.DeleteData(
                table: "payment_schedules",
                keyColumn: "id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555553"));

            migrationBuilder.DeleteData(
                table: "payment_schedules",
                keyColumn: "id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555554"));

            migrationBuilder.DeleteData(
                table: "payment_schedules",
                keyColumn: "id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"));

            migrationBuilder.DeleteData(
                table: "payment_schedules",
                keyColumn: "id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555556"));

            migrationBuilder.DeleteData(
                table: "loans",
                keyColumn: "id",
                keyValue: new Guid("b1c94f02-23d5-4e92-9c2b-af5b3264d401"));

            migrationBuilder.DeleteData(
                table: "loans",
                keyColumn: "id",
                keyValue: new Guid("c2d05f13-34e6-5fa3-ad3c-bf6c4375e512"));
        }
    }
}
