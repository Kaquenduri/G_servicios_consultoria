using Actividad_Caso_2.Models;

namespace Actividad_Caso_2.Services.Interfaces;

public interface IGastoProyectoService
{
    Task<List<GastoProyecto>> GetByProyectoAsync(
        long proyectoId);

    Task<decimal> GetTotalProyectoAsync(
        long proyectoId);

    Task<GastoProyecto> CreateAsync(
        GastoProyecto gasto);

    Task<bool> DeleteAsync(long id);
}