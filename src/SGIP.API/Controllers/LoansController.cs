namespace SGIP.API.Controllers;

using Microsoft.AspNetCore.Mvc;
using SGIP.Application.DTOs;
using SGIP.Application.Interfaces.Services;

[ApiController]
[Route("api/loans")]
public class LoansController(ILoanService loanService) : ControllerBase
{
    /// <summary>
    /// Simula un préstamo basado en los parámetros indicados sin persistir datos.
    /// </summary>
    /// <param name="request">Parámetros de entrada para la simulación del préstamo.</param>
    /// <response code="200">Resultado de la simulación con el cronograma de pagos proyectado.</response>
    /// <response code="400">Parámetros de entrada inválidos.</response>
    [HttpPost("simulate")]
    public IActionResult Simulate([FromBody] LoanSimulationRequest request)
    {
        return Ok(loanService.SimulateLoan(request));
    }

    /// <summary>
    /// Registra una nueva solicitud de préstamo sujeta a validación de políticas de riesgo.
    /// </summary>
    /// <param name="request">Datos del préstamo a solicitar.</param>
    /// <param name="idempotencyKey">Clave única que garantiza la idempotencia del request.</param>
    /// <param name="ct">Token de cancelación.</param>
    /// <response code="201">Préstamo creado exitosamente con su cronograma de pagos.</response>
    /// <response code="400">Datos inválidos o políticas de riesgo no superadas.</response>
    [HttpPost]
    public async Task<IActionResult> RequestLoan(
        [FromBody] LoanApplicationRequest request,
        [FromHeader(Name = "X-Idempotency-Key")] string idempotencyKey,
        CancellationToken ct)
    {
        var result = await loanService.ApplyForLoanAsync(request, ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    /// <summary>
    /// Obtiene la lista de todos los préstamos, con filtro opcional por usuario.
    /// </summary>
    /// <param name="userId">Identificador del usuario para filtrar resultados (opcional).</param>
    /// <param name="ct">Token de cancelación.</param>
    /// <response code="200">Lista de préstamos encontrados.</response>
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<LoanResponse>>> GetAll([FromQuery] string? userId, CancellationToken ct)
        => Ok(await loanService.GetAllAsync(userId, ct));

    /// <summary>
    /// Obtiene un préstamo junto con su cronograma de pagos por ID.
    /// </summary>
    /// <param name="id">Identificador único del préstamo.</param>
    /// <param name="ct">Token de cancelación.</param>
    /// <response code="200">Préstamo con su cronograma de pagos.</response>
    /// <response code="404">Préstamo no encontrado.</response>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        => Ok(await loanService.GetLoanWithScheduleAsync(id, ct));

    /// <summary>
    /// Obtiene el cronograma de pagos de un préstamo específico.
    /// </summary>
    /// <param name="id">Identificador único del préstamo.</param>
    /// <param name="ct">Token de cancelación.</param>
    /// <response code="200">Cronograma de pagos del préstamo.</response>
    /// <response code="404">Préstamo no encontrado.</response>
    [HttpGet("{id:guid}/schedule")]
    public async Task<IActionResult> GetSchedule(Guid id, CancellationToken ct)
        => Ok((await loanService.GetLoanWithScheduleAsync(id, ct)).Schedule);

    /// <summary>
    /// Aprueba un préstamo que se encuentra en estado Pendiente y genera su desembolso.
    /// </summary>
    /// <param name="id">Identificador único del préstamo.</param>
    /// <param name="idempotencyKey">Clave única que garantiza la idempotencia del request.</param>
    /// <param name="ct">Token de cancelación.</param>
    /// <response code="200">Préstamo aprobado y transacción de desembolso registrada.</response>
    /// <response code="400">El préstamo no se encuentra en estado Pendiente.</response>
    /// <response code="404">Préstamo no encontrado.</response>
    [HttpPatch("{id:guid}/approve")]
    public async Task<IActionResult> Approve(
        Guid id,
        [FromHeader(Name = "X-Idempotency-Key")] string idempotencyKey,
        CancellationToken ct)
    {
        return  Ok(await loanService.ApproveLoanAsync(id, idempotencyKey, ct));

    }

    /// <summary>
    /// Rechaza un préstamo que se encuentra en estado Pendiente.
    /// </summary>
    /// <param name="id">Identificador único del préstamo.</param>
    /// <param name="ct">Token de cancelación.</param>
    /// <response code="200">Préstamo rechazado exitosamente.</response>
    /// <response code="400">El préstamo no se encuentra en estado Pendiente.</response>
    /// <response code="404">Préstamo no encontrado.</response>
    [HttpPatch("{id:guid}/reject")]
    public async Task<IActionResult> Reject(Guid id, CancellationToken ct)
    { 
     return  Ok(await loanService.RejectLoanAsync(id, ct));

    }
}
