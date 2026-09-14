using Actividad_Caso_2.Data;
using Actividad_Caso_2.Models;
using Actividad_Caso_2.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Actividad_Caso_2.Repositories.Implementations;

public class GastoProyectoRepository
    : GenericRepository<GastoProyecto>,
        IGastoProyectoRepository
{
    public GastoProyectoRepository(
        GestionProyectosContext context)
        : base(context)
    {
    }

    public async Task<List<GastoProyecto>> GetByProyectoAsync(
        long proyectoId)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(g => g.ProyectoId == proyectoId)
            .OrderByDescending(g => g.FechaGasto)
            .ToListAsync();
    }

    public async Task<decimal> GetTotalProyectoAsync(
        long proyectoId)
    {
        return await _dbSet
            .Where(g => g.ProyectoId == proyectoId)
            .SumAsync(g => (decimal?)g.Monto) ?? 0;
    }
}