using Actividad_Caso_2.Data;
using Actividad_Caso_2.Models;
using Actividad_Caso_2.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Actividad_Caso_2.Repositories.Implementations;

public class TareaRepository
    : GenericRepository<Tarea>, ITareaRepository
{
    public TareaRepository(GestionProyectosContext context)
        : base(context)
    {
    }

    public async Task<List<Tarea>> GetByProyectoAsync(
        long proyectoId)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(t => t.ProyectoId == proyectoId)
            .OrderBy(t => t.FechaLimite)
            .ToListAsync();
    }

    public async Task<List<Tarea>> GetByEmpleadoAsync(
        long empleadoId)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(t => t.EmpleadoAsignadoId == empleadoId)
            .OrderBy(t => t.FechaLimite)
            .ToListAsync();
    }
}