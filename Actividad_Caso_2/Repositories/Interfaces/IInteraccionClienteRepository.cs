using Actividad_Caso_2.Models;

namespace Actividad_Caso_2.Repositories.Interfaces;

public interface IInteraccionClienteRepository
    : IGenericRepository<InteraccionCliente>
{
    Task<List<InteraccionCliente>> GetByProyectoAsync(
        long proyectoId);
}