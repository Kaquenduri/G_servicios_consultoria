using Actividad_Caso_2.Models;
using Actividad_Caso_2.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Actividad_Caso_2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InformesProgresoController : ControllerBase
{
    private readonly IInformeProgresoService _service;

    public InformesProgresoController(IInformeProgresoService service)
    {
        _service = service;
    }

    [HttpGet("proyecto/{proyectoId:long}")]
    public async Task<ActionResult<IEnumerable<InformeProgreso>>> GetByProyecto(long proyectoId)
        => Ok(await _service.GetByProyectoAsync(proyectoId));

    [HttpGet("{id:long}")]
    public async Task<ActionResult<InformeProgreso>> GetById(long id)
    {
        var informe = await _service.GetByIdAsync(id);
        return informe is null
            ? NotFound(new { mensaje = "Informe de progreso no encontrado." })
            : Ok(informe);
    }

    [HttpPost]
    public async Task<ActionResult<InformeProgreso>> Create(InformeProgreso informe)
    {
        var creado = await _service.CreateAsync(informe);
        return CreatedAtAction(nameof(GetById), new { id = creado.InformeProgresoId }, creado);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, InformeProgreso informe)
    {
        return await _service.UpdateAsync(id, informe)
            ? NoContent()
            : NotFound(new { mensaje = "Informe de progreso no encontrado." });
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        return await _service.DeleteAsync(id)
            ? NoContent()
            : NotFound(new { mensaje = "Informe de progreso no encontrado." });
    }
}
