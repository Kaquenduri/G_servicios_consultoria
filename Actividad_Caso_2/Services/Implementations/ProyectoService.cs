using Actividad_Caso_2.Models;
using Actividad_Caso_2.Services.Interfaces;
using Actividad_Caso_2.UnitOfWork;

namespace Actividad_Caso_2.Services.Implementations;

public class ProyectoService : IProyectoService
{
    private readonly IUnitOfWork _unitOfWork;

    private static readonly string[] EstadosPermitidos =
    {
        "PLANIFICADO",
        "EN_PROGRESO",
        "PAUSADO",
        "CERRADO",
        "CANCELADO"
    };

    public ProyectoService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<Proyecto>> GetAllAsync()
    {
        return await _unitOfWork.Proyectos.GetAllAsync();
    }

    public async Task<Proyecto?> GetByIdAsync(long id)
    {
        return await _unitOfWork.Proyectos.GetByIdAsync(id);
    }

    public async Task<List<Proyecto>> GetByEstadoAsync(
        string estado)
    {
        ValidarEstado(estado);

        return await _unitOfWork.Proyectos
            .GetByEstadoAsync(estado);
    }

    public async Task<Proyecto> CreateAsync(
        Proyecto proyecto)
    {
        Validar(proyecto);

        if (await _unitOfWork.Proyectos
                .ExisteCodigoAsync(proyecto.Codigo))
        {
            throw new InvalidOperationException(
                "Ya existe un proyecto con ese código.");
        }

        var cliente =
            await _unitOfWork.Clientes
                .GetByIdAsync(proyecto.ClienteId);

        if (cliente == null)
            throw new KeyNotFoundException(
                "El cliente no existe.");

        var responsable =
            await _unitOfWork.Empleados
                .GetByIdAsync(proyecto.ResponsableId);

        if (responsable == null)
            throw new KeyNotFoundException(
                "El responsable no existe.");

        await _unitOfWork.Proyectos.AddAsync(proyecto);

        await _unitOfWork.CompleteAsync();

        return proyecto;
    }

    public async Task<bool> UpdateAsync(
        long id,
        Proyecto proyecto)
    {
        var actual =
            await _unitOfWork.Proyectos.GetByIdAsync(id);

        if (actual == null)
            return false;

        Validar(proyecto);

        if (await _unitOfWork.Proyectos
                .ExisteCodigoAsync(proyecto.Codigo, id))
        {
            throw new InvalidOperationException(
                "Ya existe otro proyecto con ese código.");
        }

        if (await _unitOfWork.Clientes
                .GetByIdAsync(proyecto.ClienteId) == null)
        {
            throw new KeyNotFoundException(
                "El cliente no existe.");
        }

        if (await _unitOfWork.Empleados
                .GetByIdAsync(proyecto.ResponsableId) == null)
        {
            throw new KeyNotFoundException(
                "El responsable no existe.");
        }

        actual.ClienteId = proyecto.ClienteId;
        actual.ResponsableId = proyecto.ResponsableId;
        actual.Codigo = proyecto.Codigo;
        actual.Nombre = proyecto.Nombre;
        actual.Objetivos = proyecto.Objetivos;
        actual.FechaInicio = proyecto.FechaInicio;
        actual.FechaFinPlanificada =
            proyecto.FechaFinPlanificada;
        actual.FechaCierre = proyecto.FechaCierre;
        actual.Estado = proyecto.Estado;
        actual.PresupuestoEstimado =
            proyecto.PresupuestoEstimado;

        _unitOfWork.Proyectos.Update(actual);

        await _unitOfWork.CompleteAsync();

        return true;
    }

    public async Task<bool> CerrarAsync(long id)
    {
        var proyecto =
            await _unitOfWork.Proyectos.GetByIdAsync(id);

        if (proyecto == null)
            return false;

        if (proyecto.Estado == "CERRADO")
            throw new InvalidOperationException(
                "El proyecto ya se encuentra cerrado.");

        proyecto.Estado = "CERRADO";

        proyecto.FechaCierre =
            DateOnly.FromDateTime(DateTime.Today);

        _unitOfWork.Proyectos.Update(proyecto);

        await _unitOfWork.CompleteAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var proyecto =
            await _unitOfWork.Proyectos.GetByIdAsync(id);

        if (proyecto == null)
            return false;

        _unitOfWork.Proyectos.Remove(proyecto);

        await _unitOfWork.CompleteAsync();

        return true;
    }

    private static void Validar(Proyecto proyecto)
    {
        if (string.IsNullOrWhiteSpace(proyecto.Codigo))
            throw new ArgumentException(
                "El código es obligatorio.");

        if (string.IsNullOrWhiteSpace(proyecto.Nombre))
            throw new ArgumentException(
                "El nombre es obligatorio.");

        if (string.IsNullOrWhiteSpace(proyecto.Objetivos))
            throw new ArgumentException(
                "Los objetivos son obligatorios.");

        if (proyecto.FechaFinPlanificada <
            proyecto.FechaInicio)
        {
            throw new ArgumentException(
                "La fecha final debe ser igual o posterior a la fecha inicial.");
        }

        if (proyecto.FechaCierre.HasValue &&
            proyecto.FechaCierre.Value <
            proyecto.FechaInicio)
        {
            throw new ArgumentException(
                "La fecha de cierre no puede ser anterior al inicio.");
        }

        if (proyecto.PresupuestoEstimado < 0)
            throw new ArgumentException(
                "El presupuesto no puede ser negativo.");

        ValidarEstado(proyecto.Estado);
    }

    private static void ValidarEstado(string estado)
    {
        if (!EstadosPermitidos.Contains(estado))
        {
            throw new ArgumentException(
                "El estado del proyecto no es válido.");
        }
    }
}