using Actividad_Caso_2.Models;
using Actividad_Caso_2.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Actividad_Caso_2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HitosController : ControllerBase
{
    private readonly IHitoService _service;

    public HitosController(IHitoService service)
    {
        _service = service;
    }

    [HttpGet("proyecto/{proyectoId:long}")]
    public async Task<ActionResult<IEnumerable<Hito>>> GetByProyecto([FromRoute] long proyectoId)
        => Ok(await _service.GetByProyectoAsync(proyectoId));

    [HttpGet("{id:long}")]
    public async Task<ActionResult<Hito>> GetById([FromRoute] long id)
    {
        var hito = await _service.GetByIdAsync(id);
        return hito is null
            ? NotFound(new { mensaje = "Hito no encontrado." })
            : Ok(hito);
    }

    [HttpPost]
    public async Task<ActionResult<Hito>> Create([FromBody] Hito hito)
    {
        var creado = await _service.CreateAsync(hito);
        return CreatedAtAction(nameof(GetById), new { id = creado.HitoId }, creado);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update([FromRoute] long id, [FromBody] Hito hito)
    {
        return await _service.UpdateAsync(id, hito)
            ? NoContent()
            : NotFound(new { mensaje = "Hito no encontrado." });
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete([FromRoute] long id)
    {
        return await _service.DeleteAsync(id)
            ? NoContent()
            : NotFound(new { mensaje = "Hito no encontrado." });
    }
}
