using Actividad_Caso_2.Models;
using Actividad_Caso_2.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Actividad_Caso_2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProyectoEmpleadosController : ControllerBase
{
    private readonly IProyectoEmpleadoService _service;

    public ProyectoEmpleadosController(IProyectoEmpleadoService service)
    {
        _service = service;
    }

    [HttpGet("proyecto/{proyectoId:long}")]
    public async Task<ActionResult<IEnumerable<ProyectoEmpleado>>> GetByProyecto(long proyectoId)
        => Ok(await _service.GetByProyectoAsync(proyectoId));

    [HttpPost]
    public async Task<ActionResult<ProyectoEmpleado>> Asignar(ProyectoEmpleado asignacion)
    {
        try
        {
            var creada = await _service.AsignarAsync(asignacion);
            return CreatedAtAction(
                nameof(GetByProyecto),
                new { proyectoId = creada.ProyectoId },
                creada);
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

    [HttpDelete("proyecto/{proyectoId:long}/empleado/{empleadoId:long}")]
    public async Task<IActionResult> Desasignar(long proyectoId, long empleadoId)
    {
        try
        {
            return await _service.DesasignarAsync(proyectoId, empleadoId)
                ? NoContent()
                : NotFound(new { mensaje = "Asignación no encontrada." });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { mensaje = ex.Message });
        }
    }
}
