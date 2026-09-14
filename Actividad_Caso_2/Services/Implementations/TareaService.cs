using Actividad_Caso_2.Models;
using Actividad_Caso_2.Services.Interfaces;
using Actividad_Caso_2.Repositories.Interfaces;

namespace Actividad_Caso_2.Services.Implementations;

public class TareaService : ITareaService
{
    private readonly IUnitOfWork _unitOfWork;

    private static readonly string[] Estados =
    {
        "PENDIENTE",
        "EN_PROGRESO",
        "BLOQUEADA",
        "COMPLETADA",
        "CANCELADA"
    };

    private static readonly string[] Prioridades =
    {
        "BAJA",
        "MEDIA",
        "ALTA",
        "CRITICA"
    };

    public TareaService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<Tarea>> GetAllAsync()
    {
        return await _unitOfWork.Tareas.GetAllAsync();
    }

    public async Task<Tarea?> GetByIdAsync(long id)
    {
        return await _unitOfWork.Tareas.GetByIdAsync(id);
    }

    public async Task<List<Tarea>> GetByProyectoAsync(
        long proyectoId)
    {
        return await _unitOfWork.Tareas
            .GetByProyectoAsync(proyectoId);
    }

    public async Task<List<Tarea>> GetByEmpleadoAsync(
        long empleadoId)
    {
        return await _unitOfWork.Tareas
            .GetByEmpleadoAsync(empleadoId);
    }

    public async Task<Tarea> CreateAsync(Tarea tarea)
    {
        await ValidarAsync(tarea);

        await _unitOfWork.Tareas.AddAsync(tarea);

        await _unitOfWork.CompleteAsync();

        return tarea;
    }

    public async Task<bool> UpdateAsync(
        long id,
        Tarea tarea)
    {
        var actual =
            await _unitOfWork.Tareas.GetByIdAsync(id);

        if (actual == null)
            return false;

        await ValidarAsync(tarea);

        actual.ProyectoId = tarea.ProyectoId;
        actual.EmpleadoAsignadoId =
            tarea.EmpleadoAsignadoId;

        actual.Titulo = tarea.Titulo;
        actual.Descripcion = tarea.Descripcion;
        actual.FechaInicio = tarea.FechaInicio;
        actual.FechaLimite = tarea.FechaLimite;
        actual.FechaFinalizacion =
            tarea.FechaFinalizacion;
        actual.Estado = tarea.Estado;
        actual.Prioridad = tarea.Prioridad;
        actual.PorcentajeAvance =
            tarea.PorcentajeAvance;
        actual.HorasEstimadas = tarea.HorasEstimadas;
        actual.HorasReales = tarea.HorasReales;

        _unitOfWork.Tareas.Update(actual);

        await _unitOfWork.CompleteAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var tarea =
            await _unitOfWork.Tareas.GetByIdAsync(id);

        if (tarea == null)
            return false;

        _unitOfWork.Tareas.Remove(tarea);

        await _unitOfWork.CompleteAsync();

        return true;
    }

    private async Task ValidarAsync(Tarea tarea)
    {
        if (string.IsNullOrWhiteSpace(tarea.Titulo))
            throw new ArgumentException(
                "El título de la tarea es obligatorio.");

        if (tarea.FechaLimite < tarea.FechaInicio)
            throw new ArgumentException(
                "La fecha límite no puede ser anterior al inicio.");

        if (tarea.FechaFinalizacion.HasValue &&
            tarea.FechaFinalizacion.Value <
            tarea.FechaInicio)
        {
            throw new ArgumentException(
                "La fecha de finalización es inválida.");
        }

        if (tarea.PorcentajeAvance < 0 ||
            tarea.PorcentajeAvance > 100)
        {
            throw new ArgumentException(
                "El porcentaje debe estar entre 0 y 100.");
        }

        if (!Estados.Contains(tarea.Estado))
            throw new ArgumentException(
                "El estado de la tarea no es válido.");

        if (!Prioridades.Contains(tarea.Prioridad))
            throw new ArgumentException(
                "La prioridad no es válida.");

        if (tarea.HorasEstimadas.HasValue &&
            tarea.HorasEstimadas.Value < 0)
        {
            throw new ArgumentException(
                "Las horas estimadas no pueden ser negativas.");
        }

        if (tarea.HorasReales.HasValue &&
            tarea.HorasReales.Value < 0)
        {
            throw new ArgumentException(
                "Las horas reales no pueden ser negativas.");
        }

        if (!await _unitOfWork.ProyectoEmpleados
                .ExisteAsignacionAsync(
                    tarea.ProyectoId,
                    tarea.EmpleadoAsignadoId))
        {
            throw new InvalidOperationException(
                "El empleado debe pertenecer al proyecto antes de recibir una tarea.");
        }
    }
}
