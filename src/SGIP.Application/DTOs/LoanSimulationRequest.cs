namespace SGIP.Application.DTOs;

using SGIP.Domain.Enums;
using System.ComponentModel.DataAnnotations;

public record LoanSimulationRequest(
    [Required] string UserId,
    [Range(500, 50000, ErrorMessage = "El monto debe estar entre 500 y 50000")] decimal Amount,
    [Range(6, 60, ErrorMessage = "El plazo debe ser entre 6 y 60 meses")] int Term,
    [Required] LoanType LoanType,
    [Range(0.01, double.MaxValue, ErrorMessage = "El ingreso mensual debe ser mayor a 0")] decimal MonthlyIncome
);