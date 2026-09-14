using Actividad_Caso_2.Data;
using Actividad_Caso_2.Models;
using Actividad_Caso_2.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Actividad_Caso_2.Repositories.Implementations;

public class ProyectoEmpleadoRepository
    : GenericRepository<ProyectoEmpleado>,
        IProyectoEmpleadoRepository
{
    public ProyectoEmpleadoRepository(
        GestionProyectosContext context)
        : base(context)
    {
    }

    public async Task<bool> ExisteAsignacionAsync(
        long proyectoId,
        long empleadoId)
    {
        return await _dbSet.AnyAsync(pe =>
            pe.ProyectoId == proyectoId &&
            pe.EmpleadoId == empleadoId);
    }

    public async Task<ProyectoEmpleado?> GetAsync(
        long proyectoId,
        long empleadoId)
    {
        return await _dbSet.FirstOrDefaultAsync(pe =>
            pe.ProyectoId == proyectoId &&
            pe.EmpleadoId == empleadoId);
    }

    public async Task<List<ProyectoEmpleado>> GetByProyectoAsync(
        long proyectoId)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(pe => pe.ProyectoId == proyectoId)
            .ToListAsync();
    }
}