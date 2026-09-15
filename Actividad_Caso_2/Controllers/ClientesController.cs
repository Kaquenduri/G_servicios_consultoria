using Actividad_Caso_2.Models;
using Actividad_Caso_2.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Actividad_Caso_2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly IClienteService _service;

    public ClientesController(IClienteService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cliente>>> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id:long}")]
    public async Task<ActionResult<Cliente>> GetById(long id)
    {
        var cliente = await _service.GetByIdAsync(id);
        return cliente is null
            ? NotFound(new { mensaje = "Cliente no encontrado." })
            : Ok(cliente);
    }

    [HttpPost]
    public async Task<ActionResult<Cliente>> Create(Cliente cliente)
    {
        var creado = await _service.CreateAsync(cliente);
        return CreatedAtAction(nameof(GetById), new { id = creado.ClienteId }, creado);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, Cliente cliente)
    {
        return await _service.UpdateAsync(id, cliente)
            ? NoContent()
            : NotFound(new { mensaje = "Cliente no encontrado." });
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        return await _service.DeleteAsync(id)
            ? NoContent()
            : NotFound(new { mensaje = "Cliente no encontrado." });
    }
}
