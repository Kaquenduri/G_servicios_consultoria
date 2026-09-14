using System;
using System.Collections.Generic;

namespace Actividad_Caso_2.Models;

public partial class InteraccionCliente
{
    public long InteraccionClienteId { get; set; }

    public long ProyectoId { get; set; }

    public long EmpleadoId { get; set; }

    public string TipoInteraccion { get; set; } = null!;

    public DateTime FechaHora { get; set; }

    public string Asunto { get; set; } = null!;

    public string Detalle { get; set; } = null!;

    public string? ContactoCliente { get; set; }

    public virtual Empleado Empleado { get; set; } = null!;

    public virtual Proyecto Proyecto { get; set; } = null!;
}
