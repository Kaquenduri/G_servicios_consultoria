using Actividad_Caso_2.Data;
using Actividad_Caso_2.Models;
using Actividad_Caso_2.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Actividad_Caso_2.Repositories.Implementations;

public class InteraccionClienteRepository
    : GenericRepository<InteraccionCliente>,
        IInteraccionClienteRepository
{
    public InteraccionClienteRepository(
        GestionProyectosContext context)
        : base(context)
    {
    }

    public async Task<List<InteraccionCliente>>
        GetByProyectoAsync(long proyectoId)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(i => i.ProyectoId == proyectoId)
            .OrderByDescending(i => i.FechaHora)
            .ToListAsync();
    }
}