using Actividad_Caso_2.Data;
using Actividad_Caso_2.Models;
using Actividad_Caso_2.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Actividad_Caso_2.Repositories.Implementations;

public class HitoRepository
    : GenericRepository<Hito>, IHitoRepository
{
    public HitoRepository(GestionProyectosContext context)
        : base(context)
    {
    }

    public async Task<List<Hito>> GetByProyectoAsync(
        long proyectoId)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(h => h.ProyectoId == proyectoId)
            .OrderBy(h => h.FechaPlanificada)
            .ToListAsync();
    }
}