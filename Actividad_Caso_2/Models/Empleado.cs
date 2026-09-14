using System;
using System.Collections.Generic;

namespace Actividad_Caso_2.Models;

public partial class Empleado
{
    public long EmpleadoId { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string DocumentoIdentidad { get; set; } = null!;

    public string CorreoCorporativo { get; set; } = null!;

    public string? Telefono { get; set; }

    public string Cargo { get; set; } = null!;

    public DateOnly FechaIngreso { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<GastoProyecto> GastoProyectos { get; set; } = new List<GastoProyecto>();

    public virtual ICollection<InformeProgreso> InformeProgresos { get; set; } = new List<InformeProgreso>();

    public virtual ICollection<InteraccionCliente> InteraccionClientes { get; set; } = new List<InteraccionCliente>();

    public virtual ICollection<ProyectoEmpleado> ProyectoEmpleados { get; set; } = new List<ProyectoEmpleado>();

    public virtual ICollection<Proyecto> Proyectos { get; set; } = new List<Proyecto>();
}
