using Actividad_Caso_2.Models;

namespace Actividad_Caso_2.Repositories.Interfaces;

public interface IEmpleadoRepository : IGenericRepository<Empleado>
{
    Task<bool> ExisteDocumentoAsync(
        string documento,
        long? excluirEmpleadoId = null);

    Task<bool> ExisteCorreoAsync(
        string correo,
        long? excluirEmpleadoId = null);
}