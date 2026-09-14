using Actividad_Caso_2.Models;
using Actividad_Caso_2.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Actividad_Caso_2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProyectosController : ControllerBase
{
    private readonly IProyectoService _service;

    public ProyectosController(IProyectoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Proyecto>>> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id:long}")]
    public async Task<ActionResult<Proyecto>> GetById(long id)
    {
        var proyecto = await _service.GetByIdAsync(id);
        return proyecto is null
            ? NotFound(new { mensaje = "Proyecto no encontrado." })
            : Ok(proyecto);
    }

    [HttpGet("estado/{estado}")]
    public async Task<ActionResult<IEnumerable<Proyecto>>> GetByEstado(string estado)
    {
        try
        {
            return Ok(await _service.GetByEstadoAsync(estado));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpPost]
    public async Task<ActionResult<Proyecto>> Create(Proyecto proyecto)
    {
        try
        {
            var creado = await _service.CreateAsync(proyecto);
            return CreatedAtAction(nameof(GetById), new { id = creado.ProyectoId }, creado);
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
    public async Task<IActionResult> Update(long id, Proyecto proyecto)
    {
        try
        {
            return await _service.UpdateAsync(id, proyecto)
                ? NoContent()
                : NotFound(new { mensaje = "Proyecto no encontrado." });
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

    [HttpPatch("{id:long}/cerrar")]
    public async Task<IActionResult> Cerrar(long id)
    {
        try
        {
            return await _service.CerrarAsync(id)
                ? NoContent()
                : NotFound(new { mensaje = "Proyecto no encontrado." });
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
                : NotFound(new { mensaje = "Proyecto no encontrado." });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { mensaje = ex.Message });
        }
    }
}
