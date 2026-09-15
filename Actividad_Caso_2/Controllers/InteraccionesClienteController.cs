using Actividad_Caso_2.Models;
using Actividad_Caso_2.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Actividad_Caso_2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InteraccionesClienteController : ControllerBase
{
    private readonly IInteraccionClienteService _service;

    public InteraccionesClienteController(IInteraccionClienteService service)
    {
        _service = service;
    }

    [HttpGet("proyecto/{proyectoId:long}")]
    public async Task<ActionResult<IEnumerable<InteraccionCliente>>> GetByProyecto([FromRoute] long proyectoId)
        => Ok(await _service.GetByProyectoAsync(proyectoId));

    [HttpPost]
    public async Task<ActionResult<InteraccionCliente>> Create([FromBody] InteraccionCliente interaccion)
    {
        var creada = await _service.CreateAsync(interaccion);
        return StatusCode(StatusCodes.Status201Created, creada);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete([FromRoute] long id)
    {
        return await _service.DeleteAsync(id)
            ? NoContent()
            : NotFound(new { mensaje = "Interacción no encontrada." });
    }
}
