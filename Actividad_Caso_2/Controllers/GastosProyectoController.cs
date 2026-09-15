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
    public async Task<ActionResult<IEnumerable<GastoProyecto>>> GetByProyecto([FromRoute] long proyectoId)
        => Ok(await _service.GetByProyectoAsync(proyectoId));

    [HttpGet("proyecto/{proyectoId:long}/total")]
    public async Task<IActionResult> GetTotalProyecto([FromRoute] long proyectoId)
    {
        var total = await _service.GetTotalProyectoAsync(proyectoId);
        return Ok(new { proyectoId, totalGastos = total });
    }

    [HttpPost]
    public async Task<ActionResult<GastoProyecto>> Create([FromBody] GastoProyecto gasto)
    {
        var creado = await _service.CreateAsync(gasto);
        return StatusCode(StatusCodes.Status201Created, creado);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete([FromRoute] long id)
    {
        return await _service.DeleteAsync(id)
            ? NoContent()
            : NotFound(new { mensaje = "Gasto no encontrado." });
    }
}
