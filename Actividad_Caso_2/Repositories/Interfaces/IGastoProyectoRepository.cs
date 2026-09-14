using Actividad_Caso_2.Models;

namespace Actividad_Caso_2.Repositories.Interfaces;

public interface IGastoProyectoRepository
    : IGenericRepository<GastoProyecto>
{
    Task<List<GastoProyecto>> GetByProyectoAsync(
        long proyectoId);

    Task<decimal> GetTotalProyectoAsync(
        long proyectoId);
}