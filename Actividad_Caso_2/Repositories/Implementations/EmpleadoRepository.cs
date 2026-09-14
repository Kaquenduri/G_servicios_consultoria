using Actividad_Caso_2.Data;
using Actividad_Caso_2.Models;
using Actividad_Caso_2.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Actividad_Caso_2.Repositories.Implementations;

public class EmpleadoRepository
    : GenericRepository<Empleado>, IEmpleadoRepository
{
    public EmpleadoRepository(GestionProyectosContext context)
        : base(context)
    {
    }

    public async Task<bool> ExisteDocumentoAsync(
        string documento,
        long? excluirEmpleadoId = null)
    {
        return await _dbSet.AnyAsync(e =>
            e.DocumentoIdentidad == documento &&
            (!excluirEmpleadoId.HasValue ||
             e.EmpleadoId != excluirEmpleadoId.Value));
    }

    public async Task<bool> ExisteCorreoAsync(
        string correo,
        long? excluirEmpleadoId = null)
    {
        return await _dbSet.AnyAsync(e =>
            e.CorreoCorporativo == correo &&
            (!excluirEmpleadoId.HasValue ||
             e.EmpleadoId != excluirEmpleadoId.Value));
    }
}