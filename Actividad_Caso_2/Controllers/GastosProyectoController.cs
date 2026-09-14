using Actividad_Caso_2.Models;
using Actividad_Caso_2.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Actividad_Caso_2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GastosProyectoController : ControllerBase
{
    private readonly IGastoProyectoService _service;

    public GastosProyectoController(IGastoProyectoService service)
    {
        _service = service;
    }

    [HttpGet("proyecto/{proyectoId:long}")]
    public async Task<ActionResult<IEnumerable<GastoProyecto>>> GetByProyecto(long proyectoId)
        => Ok(await _service.GetByProyectoAsync(proyectoId));

    [HttpGet("proyecto/{proyectoId:long}/total")]
    public async Task<IActionResult> GetTotalProyecto(long proyectoId)
    {
        var total = await _service.GetTotalProyectoAsync(proyectoId);
        return Ok(new { proyectoId, totalGastos = total });
    }

    [HttpPost]
    public async Task<ActionResult<GastoProyecto>> Create(GastoProyecto gasto)
    {
        try
        {
            var creado = await _service.CreateAsync(gasto);
            return StatusCode(StatusCodes.Status201Created, creado);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { mensaje = ex.Message });
        }
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        try
        {
            return await _service.DeleteAsync(id)
                ? NoContent()
                : NotFound(new { mensaje = "Gasto no encontrado." });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { mensaje = ex.Message });
        }
    }
}
