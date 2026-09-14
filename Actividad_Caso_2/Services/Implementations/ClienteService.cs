using Actividad_Caso_2.Models;
using Actividad_Caso_2.Services.Interfaces;
using Actividad_Caso_2.Repositories.Interfaces;

namespace Actividad_Caso_2.Services.Implementations;

public class ClienteService : IClienteService
{
    private readonly IUnitOfWork _unitOfWork;

    public ClienteService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<Cliente>> GetAllAsync()
    {
        return await _unitOfWork.Clientes.GetAllAsync();
    }

    public async Task<Cliente?> GetByIdAsync(long id)
    {
        return await _unitOfWork.Clientes.GetByIdAsync(id);
    }

    public async Task<Cliente> CreateAsync(Cliente cliente)
    {
        Validar(cliente);

        if (!string.IsNullOrWhiteSpace(cliente.Ruc) &&
            await _unitOfWork.Clientes
                .ExisteRucAsync(cliente.Ruc))
        {
            throw new InvalidOperationException(
                "Ya existe un cliente con ese RUC.");
        }

        await _unitOfWork.Clientes.AddAsync(cliente);
        await _unitOfWork.CompleteAsync();

        return cliente;
    }

    public async Task<bool> UpdateAsync(
        long id,
        Cliente cliente)
    {
        var actual =
            await _unitOfWork.Clientes.GetByIdAsync(id);

        if (actual == null)
            return false;

        Validar(cliente);

        if (!string.IsNullOrWhiteSpace(cliente.Ruc) &&
            await _unitOfWork.Clientes
                .ExisteRucAsync(cliente.Ruc, id))
        {
            throw new InvalidOperationException(
                "Ya existe un cliente con ese RUC.");
        }

        actual.RazonSocial = cliente.RazonSocial;
        actual.Ruc = cliente.Ruc;
        actual.NombreContacto = cliente.NombreContacto;
        actual.CorreoContacto = cliente.CorreoContacto;
        actual.TelefonoContacto = cliente.TelefonoContacto;
        actual.Direccion = cliente.Direccion;
        actual.Activo = cliente.Activo;

        _unitOfWork.Clientes.Update(actual);

        await _unitOfWork.CompleteAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var cliente =
            await _unitOfWork.Clientes.GetByIdAsync(id);

        if (cliente == null)
            return false;

        _unitOfWork.Clientes.Remove(cliente);

        await _unitOfWork.CompleteAsync();

        return true;
    }

    private static void Validar(Cliente cliente)
    {
        if (string.IsNullOrWhiteSpace(cliente.RazonSocial))
            throw new ArgumentException(
                "La razón social es obligatoria.");

        if (!string.IsNullOrWhiteSpace(cliente.Ruc))
        {
            if (cliente.Ruc.Length != 11 ||
                !cliente.Ruc.All(char.IsDigit))
            {
                throw new ArgumentException(
                    "El RUC debe contener exactamente 11 dígitos.");
            }
        }
    }
}
