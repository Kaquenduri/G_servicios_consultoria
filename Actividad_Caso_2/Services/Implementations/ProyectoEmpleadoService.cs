using Actividad_Caso_2.Models;
using Actividad_Caso_2.Services.Interfaces;
using Actividad_Caso_2.UnitOfWork;

namespace Actividad_Caso_2.Services.Implementations;

public class ProyectoEmpleadoService
    : IProyectoEmpleadoService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProyectoEmpleadoService(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<ProyectoEmpleado>>
        GetByProyectoAsync(long proyectoId)
    {
        return await _unitOfWork.ProyectoEmpleados
            .GetByProyectoAsync(proyectoId);
    }

    public async Task<ProyectoEmpleado> AsignarAsync(
        ProyectoEmpleado asignacion)
    {
        if (string.IsNullOrWhiteSpace(
                asignacion.RolEnProyecto))
        {
            throw new ArgumentException(
                "El rol dentro del proyecto es obligatorio.");
        }

        if (await _unitOfWork.Proyectos
                .GetByIdAsync(asignacion.ProyectoId) == null)
        {
            throw new KeyNotFoundException(
                "El proyecto no existe.");
        }

        if (await _unitOfWork.Empleados
                .GetByIdAsync(asignacion.EmpleadoId) == null)
        {
            throw new KeyNotFoundException(
                "El empleado no existe.");
        }

        if (await _unitOfWork.ProyectoEmpleados
                .ExisteAsignacionAsync(
                    asignacion.ProyectoId,
                    asignacion.EmpleadoId))
        {
            throw new InvalidOperationException(
                "El empleado ya pertenece al proyecto.");
        }

        await _unitOfWork.ProyectoEmpleados
            .AddAsync(asignacion);

        await _unitOfWork.CompleteAsync();

        return asignacion;
    }

    public async Task<bool> DesasignarAsync(
        long proyectoId,
        long empleadoId)
    {
        var asignacion =
            await _unitOfWork.ProyectoEmpleados
                .GetAsync(proyectoId, empleadoId);

        if (asignacion == null)
            return false;

        _unitOfWork.ProyectoEmpleados
            .Remove(asignacion);

        await _unitOfWork.CompleteAsync();

        return true;
    }
}