using System;
using System.Collections.Generic;

namespace Actividad_Caso_2.Models;

public partial class Tarea
{
    public long TareaId { get; set; }

    public long ProyectoId { get; set; }

    public long EmpleadoAsignadoId { get; set; }

    public string Titulo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public DateOnly FechaInicio { get; set; }

    public DateOnly FechaLimite { get; set; }

    public DateOnly? FechaFinalizacion { get; set; }

    public string Estado { get; set; } = null!;

    public string Prioridad { get; set; } = null!;

    public short PorcentajeAvance { get; set; }

    public decimal? HorasEstimadas { get; set; }

    public decimal? HorasReales { get; set; }

    public virtual ProyectoEmpleado ProyectoEmpleado { get; set; } = null!;
}
