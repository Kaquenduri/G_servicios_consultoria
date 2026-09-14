using Actividad_Caso_2.Data;
using Actividad_Caso_2.Models;
using Actividad_Caso_2.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Actividad_Caso_2.Repositories.Implementations;

public class ClienteRepository
    : GenericRepository<Cliente>, IClienteRepository
{
    public ClienteRepository(GestionProyectosContext context)
        : base(context)
    {
    }

    public async Task<bool> ExisteRucAsync(
        string ruc,
        long? excluirClienteId = null)
    {
        return await _dbSet.AnyAsync(c =>
            c.Ruc == ruc &&
            (!excluirClienteId.HasValue ||
             c.ClienteId != excluirClienteId.Value));
    }
}