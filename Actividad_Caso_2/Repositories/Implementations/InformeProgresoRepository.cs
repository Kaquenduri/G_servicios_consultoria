using Actividad_Caso_2.Data;
using Actividad_Caso_2.Models;
using Actividad_Caso_2.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Actividad_Caso_2.Repositories.Implementations;

public class InformeProgresoRepository
    : GenericRepository<InformeProgreso>,
        IInformeProgresoRepository
{
    public InformeProgresoRepository(
        GestionProyectosContext context)
        : base(context)
    {
    }

    public async Task<List<InformeProgreso>>
        GetByProyectoAsync(long proyectoId)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(i => i.ProyectoId == proyectoId)
            .OrderByDescending(i => i.FechaInforme)
            .ToListAsync();
    }
}