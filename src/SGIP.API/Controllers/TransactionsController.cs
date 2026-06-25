namespace SGIP.API.Controllers;

using Microsoft.AspNetCore.Mvc;
using SGIP.Application.DTOs;
using SGIP.Application.Interfaces.Services;

[ApiController]
[Route("api/transactions")]
public class TransactionsController(ITransactionService transactionService) : ControllerBase
{
    /// <summary>
    /// Crea una nueva transacción financiera asociada opcionalmente a un préstamo.
    /// </summary>
    /// <param name="request">Datos de la transacción a registrar.</param>
    /// <param name="ct">Token de cancelación.</param>
    /// <response code="201">Transacción creada exitosamente.</response>
    /// <response code="400">Datos inválidos o clave de idempotencia duplicada.</response>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTransactionRequest request, CancellationToken ct)
    {
        var result = await transactionService.CreateAsync(request, ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    /// <summary>
    /// Obtiene la lista de todas las transacciones, con filtro opcional por préstamo.
    /// </summary>
    /// <param name="loanId">Identificador del préstamo para filtrar transacciones (opcional).</param>
    /// <param name="ct">Token de cancelación.</param>
    /// <response code="200">Lista de transacciones encontradas.</response>
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Guid? loanId, CancellationToken ct)
    { 
        return Ok(await transactionService.GetAllAsync(loanId, ct));
    }

    /// <summary>
    /// Obtiene una transacción por su identificador único.
    /// </summary>
    /// <param name="id">Identificador único de la transacción.</param>
    /// <param name="ct">Token de cancelación.</param>
    /// <response code="200">Datos de la transacción encontrada.</response>
    /// <response code="404">Transacción no encontrada.</response>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        return Ok(await transactionService.GetByIdAsync(id, ct));
    }
}
