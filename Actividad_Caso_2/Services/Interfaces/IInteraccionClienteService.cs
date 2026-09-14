using Actividad_Caso_2.Models;

namespace Actividad_Caso_2.Services.Interfaces;

public interface IInteraccionClienteService
{
    Task<List<InteraccionCliente>> GetByProyectoAsync(
        long proyectoId);

    Task<InteraccionCliente> CreateAsync(
        InteraccionCliente interaccion);

    Task<bool> DeleteAsync(long id);
}