using System;
using System.Collections.Generic;

namespace Actividad_Caso_2.Models;

public partial class Cliente
{
    public long ClienteId { get; set; }

    public string RazonSocial { get; set; } = null!;

    public string? Ruc { get; set; }

    public string? NombreContacto { get; set; }

    public string? CorreoContacto { get; set; }

    public string? TelefonoContacto { get; set; }

    public string? Direccion { get; set; }

    public bool Activo { get; set; }

    public DateTime FechaRegistro { get; set; }

    public virtual ICollection<Proyecto> Proyectos { get; set; } = new List<Proyecto>();
}
