using System;
using System.Collections.Generic;

namespace Actividad_Caso_2.Models;

public partial class Proyecto
{
    public long ProyectoId { get; set; }

    public long ClienteId { get; set; }

    public long ResponsableId { get; set; }

    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Objetivos { get; set; } = null!;

    public DateOnly FechaInicio { get; set; }

    public DateOnly FechaFinPlanificada { get; set; }

    public DateOnly? FechaCierre { get; set; }

    public string Estado { get; set; } = null!;

    public decimal PresupuestoEstimado { get; set; }

    public DateTime FechaRegistro { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual ICollection<GastoProyecto> GastoProyectos { get; set; } = new List<GastoProyecto>();

    public virtual ICollection<Hito> Hitos { get; set; } = new List<Hito>();

    public virtual ICollection<InformeProgreso> InformeProgresos { get; set; } = new List<InformeProgreso>();

    public virtual ICollection<InteraccionCliente> InteraccionClientes { get; set; } = new List<InteraccionCliente>();

    public virtual ICollection<ProyectoEmpleado> ProyectoEmpleados { get; set; } = new List<ProyectoEmpleado>();

    public virtual Empleado Responsable { get; set; } = null!;
}
