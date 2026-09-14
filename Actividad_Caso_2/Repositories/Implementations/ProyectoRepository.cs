using Actividad_Caso_2.Data;
using Actividad_Caso_2.Models;
using Actividad_Caso_2.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Actividad_Caso_2.Repositories.Implementations;

public class ProyectoRepository
    : GenericRepository<Proyecto>, IProyectoRepository
{
    public ProyectoRepository(GestionProyectosContext context)
        : base(context)
    {
    }

    public async Task<bool> ExisteCodigoAsync(
        string codigo,
        long? excluirProyectoId = null)
    {
        return await _dbSet.AnyAsync(p =>
            p.Codigo == codigo &&
            (!excluirProyectoId.HasValue ||
             p.ProyectoId != excluirProyectoId.Value));
    }

    public async Task<List<Proyecto>> GetByEstadoAsync(
        string estado)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(p => p.Estado == estado)
            .ToListAsync();
    }
}