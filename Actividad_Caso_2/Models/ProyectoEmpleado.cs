using System;
using System.Collections.Generic;

namespace Actividad_Caso_2.Models;

public partial class ProyectoEmpleado
{
    public long ProyectoId { get; set; }

    public long EmpleadoId { get; set; }

    public string RolEnProyecto { get; set; } = null!;

    public DateOnly FechaAsignacion { get; set; }

    public bool Activo { get; set; }

    public virtual Empleado Empleado { get; set; } = null!;

    public virtual Proyecto Proyecto { get; set; } = null!;

    public virtual ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();
}
