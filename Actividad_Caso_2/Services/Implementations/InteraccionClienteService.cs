using Actividad_Caso_2.Models;
using Actividad_Caso_2.Services.Interfaces;
using Actividad_Caso_2.UnitOfWork;

namespace Actividad_Caso_2.Services.Implementations;

public class InteraccionClienteService
    : IInteraccionClienteService
{
    private readonly IUnitOfWork _unitOfWork;

    private static readonly string[] Tipos =
    {
        "CORREO",
        "REUNION",
        "LLAMADA",
        "MENSAJE",
        "OTRO"
    };

    public InteraccionClienteService(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<InteraccionCliente>>
        GetByProyectoAsync(long proyectoId)
    {
        return await _unitOfWork.InteraccionesCliente
            .GetByProyectoAsync(proyectoId);
    }

    public async Task<InteraccionCliente> CreateAsync(
        InteraccionCliente interaccion)
    {
        if (!Tipos.Contains(interaccion.TipoInteraccion))
            throw new ArgumentException(
                "El tipo de interacción no es válido.");

        if (string.IsNullOrWhiteSpace(
                interaccion.Asunto))
        {
            throw new ArgumentException(
                "El asunto es obligatorio.");
        }

        if (string.IsNullOrWhiteSpace(
                interaccion.Detalle))
        {
            throw new ArgumentException(
                "El detalle es obligatorio.");
        }

        if (await _unitOfWork.Proyectos
                .GetByIdAsync(interaccion.ProyectoId)
            == null)
        {
            throw new KeyNotFoundException(
                "El proyecto no existe.");
        }

        if (await _unitOfWork.Empleados
                .GetByIdAsync(interaccion.EmpleadoId)
            == null)
        {
            throw new KeyNotFoundException(
                "El empleado no existe.");
        }

        await _unitOfWork.InteraccionesCliente
            .AddAsync(interaccion);

        await _unitOfWork.CompleteAsync();

        return interaccion;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var interaccion =
            await _unitOfWork.InteraccionesCliente
                .GetByIdAsync(id);

        if (interaccion == null)
            return false;

        _unitOfWork.InteraccionesCliente
            .Remove(interaccion);

        await _unitOfWork.CompleteAsync();

        return true;
    }
}