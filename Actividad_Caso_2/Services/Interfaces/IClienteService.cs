using Actividad_Caso_2.Models;

namespace Actividad_Caso_2.Services.Interfaces;

public interface IClienteService
{
    Task<List<Cliente>> GetAllAsync();
    Task<Cliente?> GetByIdAsync(long id);
    Task<Cliente> CreateAsync(Cliente cliente);
    Task<bool> UpdateAsync(long id, Cliente cliente);
    Task<bool> DeleteAsync(long id);
}