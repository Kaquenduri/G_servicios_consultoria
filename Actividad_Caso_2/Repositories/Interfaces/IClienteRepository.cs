using Actividad_Caso_2.Models;

namespace Actividad_Caso_2.Repositories.Interfaces;

public interface IClienteRepository : IGenericRepository<Cliente>
{
    Task<bool> ExisteRucAsync(
        string ruc,
        long? excluirClienteId = null);
}