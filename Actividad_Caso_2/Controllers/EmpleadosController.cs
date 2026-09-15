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
    public async Task<ActionResult<Empleado>> GetById([FromRoute] long id)
    {
        var empleado = await _service.GetByIdAsync(id);
        return empleado is null
            ? NotFound(new { mensaje = "Empleado no encontrado." })
            : Ok(empleado);
    }

    [HttpPost]
    public async Task<ActionResult<Empleado>> Create([FromBody] Empleado empleado)
    {
        var creado = await _service.CreateAsync(empleado);
        return CreatedAtAction(nameof(GetById), new { id = creado.EmpleadoId }, creado);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update([FromRoute] long id, Empleado empleado)
    {
        return await _service.UpdateAsync(id, empleado)
            ? NoContent()
            : NotFound(new { mensaje = "Empleado no encontrado." });
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete([FromRoute] long id)
    {
        return await _service.DeleteAsync(id)
            ? NoContent()
            : NotFound(new { mensaje = "Empleado no encontrado." });
    }
}
