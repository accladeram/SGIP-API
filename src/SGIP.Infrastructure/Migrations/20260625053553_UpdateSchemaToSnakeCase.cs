using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SGIP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSchemaToSnakeCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentSchedules_Loans_LoanId",
                table: "PaymentSchedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Loans",
                table: "Loans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentSchedules",
                table: "PaymentSchedules");

            migrationBuilder.RenameTable(
                name: "Transactions",
                newName: "transactions");

            migrationBuilder.RenameTable(
                name: "Loans",
                newName: "loans");

            migrationBuilder.RenameTable(
                name: "PaymentSchedules",
                newName: "payment_schedules");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "transactions",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "transactions",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "transactions",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "transactions",
                newName: "amount");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "transactions",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "LoanId",
                table: "transactions",
                newName: "loan_id");

            migrationBuilder.RenameColumn(
                name: "IdempotencyKey",
                table: "transactions",
                newName: "idempotency_key");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "transactions",
                newName: "created_at");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_IdempotencyKey",
                table: "transactions",
                newName: "ux_transactions_idempotency_key");

            migrationBuilder.RenameColumn(
                name: "Term",
                table: "loans",
                newName: "term");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "loans",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "loans",
                newName: "amount");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "loans",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "loans",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "loans",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "MonthlyPayment",
                table: "loans",
                newName: "monthly_payment");

            migrationBuilder.RenameColumn(
                name: "LoanType",
                table: "loans",
                newName: "loan_type");

            migrationBuilder.RenameColumn(
                name: "InterestRate",
                table: "loans",
                newName: "interest_rate");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "loans",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "payment_schedules",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Principal",
                table: "payment_schedules",
                newName: "principal");

            migrationBuilder.RenameColumn(
                name: "Interest",
                table: "payment_schedules",
                newName: "interest");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "payment_schedules",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "TotalPayment",
                table: "payment_schedules",
                newName: "total_payment");

            migrationBuilder.RenameColumn(
                name: "RemainingBalance",
                table: "payment_schedules",
                newName: "remaining_balance");

            migrationBuilder.RenameColumn(
                name: "PaymentNumber",
                table: "payment_schedules",
                newName: "payment_number");

            migrationBuilder.RenameColumn(
                name: "LoanId",
                table: "payment_schedules",
                newName: "loan_id");

            migrationBuilder.RenameColumn(
                name: "DueDate",
                table: "payment_schedules",
                newName: "due_date");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentSchedules_LoanId",
                table: "payment_schedules",
                newName: "ix_payment_schedules_loan_id");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "transactions",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "amount",
                table: "transactions",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<string>(
                name: "idempotency_key",
                table: "transactions",
                type: "character varying(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<decimal>(
                name: "amount",
                table: "loans",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<string>(
                name: "user_id",
                table: "loans",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<decimal>(
                name: "monthly_payment",
                table: "loans",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "interest_rate",
                table: "loans",
                type: "numeric(5,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "principal",
                table: "payment_schedules",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "interest",
                table: "payment_schedules",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "total_payment",
                table: "payment_schedules",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "remaining_balance",
                table: "payment_schedules",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddPrimaryKey(
                name: "PK_transactions",
                table: "transactions",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_loans",
                table: "loans",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_payment_schedules",
                table: "payment_schedules",
                column: "id");

            migrationBuilder.InsertData(
                table: "loans",
                columns: new[] { "id", "amount", "created_at", "interest_rate", "loan_type", "monthly_payment", "status", "term", "updated_at", "user_id" },
                values: new object[] { new Guid("a3b84f92-12c4-4d81-8b1a-9f4a2153c390"), 1000.00m, new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), 0.20m, 0, 344.41m, 3, 3, new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "USR-001" });

            migrationBuilder.InsertData(
                table: "payment_schedules",
                columns: new[] { "id", "due_date", "interest", "loan_id", "payment_number", "principal", "remaining_balance", "status", "total_payment" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 7, 24, 0, 0, 0, 0, DateTimeKind.Utc), 15.30m, new Guid("a3b84f92-12c4-4d81-8b1a-9f4a2153c390"), 1, 329.11m, 670.89m, 0, 344.41m },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2026, 8, 24, 0, 0, 0, 0, DateTimeKind.Utc), 10.26m, new Guid("a3b84f92-12c4-4d81-8b1a-9f4a2153c390"), 2, 334.15m, 336.74m, 0, 344.41m },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2026, 9, 24, 0, 0, 0, 0, DateTimeKind.Utc), 7.67m, new Guid("a3b84f92-12c4-4d81-8b1a-9f4a2153c390"), 3, 336.74m, 0.00m, 0, 344.41m }
                });

            migrationBuilder.CreateIndex(
                name: "ix_loans_user_id",
                table: "loans",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_payment_schedules_loans_loan_id",
                table: "payment_schedules",
                column: "loan_id",
                principalTable: "loans",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_payment_schedules_loans_loan_id",
                table: "payment_schedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_transactions",
                table: "transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_loans",
                table: "loans");

            migrationBuilder.DropIndex(
                name: "ix_loans_user_id",
                table: "loans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_payment_schedules",
                table: "payment_schedules");

            migrationBuilder.DeleteData(
                table: "payment_schedules",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "payment_schedules",
                keyColumn: "id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "payment_schedules",
                keyColumn: "id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "loans",
                keyColumn: "id",
                keyValue: new Guid("a3b84f92-12c4-4d81-8b1a-9f4a2153c390"));

            migrationBuilder.RenameTable(
                name: "transactions",
                newName: "Transactions");

            migrationBuilder.RenameTable(
                name: "loans",
                newName: "Loans");

            migrationBuilder.RenameTable(
                name: "payment_schedules",
                newName: "PaymentSchedules");

            migrationBuilder.RenameColumn(
                name: "type",
                table: "Transactions",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Transactions",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Transactions",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "amount",
                table: "Transactions",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Transactions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "loan_id",
                table: "Transactions",
                newName: "LoanId");

            migrationBuilder.RenameColumn(
                name: "idempotency_key",
                table: "Transactions",
                newName: "IdempotencyKey");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Transactions",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "ux_transactions_idempotency_key",
                table: "Transactions",
                newName: "IX_Transactions_IdempotencyKey");

            migrationBuilder.RenameColumn(
                name: "term",
                table: "Loans",
                newName: "Term");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Loans",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "amount",
                table: "Loans",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Loans",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Loans",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Loans",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "monthly_payment",
                table: "Loans",
                newName: "MonthlyPayment");

            migrationBuilder.RenameColumn(
                name: "loan_type",
                table: "Loans",
                newName: "LoanType");

            migrationBuilder.RenameColumn(
                name: "interest_rate",
                table: "Loans",
                newName: "InterestRate");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Loans",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "PaymentSchedules",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "principal",
                table: "PaymentSchedules",
                newName: "Principal");

            migrationBuilder.RenameColumn(
                name: "interest",
                table: "PaymentSchedules",
                newName: "Interest");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "PaymentSchedules",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "total_payment",
                table: "PaymentSchedules",
                newName: "TotalPayment");

            migrationBuilder.RenameColumn(
                name: "remaining_balance",
                table: "PaymentSchedules",
                newName: "RemainingBalance");

            migrationBuilder.RenameColumn(
                name: "payment_number",
                table: "PaymentSchedules",
                newName: "PaymentNumber");

            migrationBuilder.RenameColumn(
                name: "loan_id",
                table: "PaymentSchedules",
                newName: "LoanId");

            migrationBuilder.RenameColumn(
                name: "due_date",
                table: "PaymentSchedules",
                newName: "DueDate");

            migrationBuilder.RenameIndex(
                name: "ix_payment_schedules_loan_id",
                table: "PaymentSchedules",
                newName: "IX_PaymentSchedules_LoanId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Transactions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Transactions",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "IdempotencyKey",
                table: "Transactions",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Loans",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Loans",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<decimal>(
                name: "MonthlyPayment",
                table: "Loans",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "InterestRate",
                table: "Loans",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(5,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Principal",
                table: "PaymentSchedules",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Interest",
                table: "PaymentSchedules",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPayment",
                table: "PaymentSchedules",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "RemainingBalance",
                table: "PaymentSchedules",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Loans",
                table: "Loans",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentSchedules",
                table: "PaymentSchedules",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentSchedules_Loans_LoanId",
                table: "PaymentSchedules",
                column: "LoanId",
                principalTable: "Loans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
