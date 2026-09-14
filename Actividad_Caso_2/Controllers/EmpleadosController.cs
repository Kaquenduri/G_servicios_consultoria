using Actividad_Caso_2.Models;
using Actividad_Caso_2.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Actividad_Caso_2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmpleadosController : ControllerBase
{
    private readonly IEmpleadoService _service;

    public EmpleadosController(IEmpleadoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Empleado>>> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id:long}")]
    public async Task<ActionResult<Empleado>> GetById(long id)
    {
        var empleado = await _service.GetByIdAsync(id);
        return empleado is null
            ? NotFound(new { mensaje = "Empleado no encontrado." })
            : Ok(empleado);
    }

    [HttpPost]
    public async Task<ActionResult<Empleado>> Create(Empleado empleado)
    {
        try
        {
            var creado = await _service.CreateAsync(empleado);
            return CreatedAtAction(nameof(GetById), new { id = creado.EmpleadoId }, creado);
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
    public async Task<IActionResult> Update(long id, Empleado empleado)
    {
        try
        {
            return await _service.UpdateAsync(id, empleado)
                ? NoContent()
                : NotFound(new { mensaje = "Empleado no encontrado." });
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
                : NotFound(new { mensaje = "Empleado no encontrado." });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { mensaje = ex.Message });
        }
    }
}
