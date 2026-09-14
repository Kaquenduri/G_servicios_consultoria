using Actividad_Caso_2.Models;
using Actividad_Caso_2.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Actividad_Caso_2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TareasController : ControllerBase
{
    private readonly ITareaService _service;

    public TareasController(ITareaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tarea>>> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id:long}")]
    public async Task<ActionResult<Tarea>> GetById(long id)
    {
        var tarea = await _service.GetByIdAsync(id);
        return tarea is null
            ? NotFound(new { mensaje = "Tarea no encontrada." })
            : Ok(tarea);
    }

    [HttpGet("proyecto/{proyectoId:long}")]
    public async Task<ActionResult<IEnumerable<Tarea>>> GetByProyecto(long proyectoId)
        => Ok(await _service.GetByProyectoAsync(proyectoId));

    [HttpGet("empleado/{empleadoId:long}")]
    public async Task<ActionResult<IEnumerable<Tarea>>> GetByEmpleado(long empleadoId)
        => Ok(await _service.GetByEmpleadoAsync(empleadoId));

    [HttpPost]
    public async Task<ActionResult<Tarea>> Create(Tarea tarea)
    {
        try
        {
            var creada = await _service.CreateAsync(tarea);
            return CreatedAtAction(nameof(GetById), new { id = creada.TareaId }, creada);
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

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, Tarea tarea)
    {
        try
        {
            return await _service.UpdateAsync(id, tarea)
                ? NoContent()
                : NotFound(new { mensaje = "Tarea no encontrada." });
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
                : NotFound(new { mensaje = "Tarea no encontrada." });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { mensaje = ex.Message });
        }
    }
}
