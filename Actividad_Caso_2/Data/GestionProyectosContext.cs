using System;
using System.Collections.Generic;
using Actividad_Caso_2.Models;
using Microsoft.EntityFrameworkCore;

namespace Actividad_Caso_2.Data;

public partial class GestionProyectosContext : DbContext
{
    public GestionProyectosContext(DbContextOptions<GestionProyectosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<GastoProyecto> GastoProyectos { get; set; }

    public virtual DbSet<Hito> Hitos { get; set; }

    public virtual DbSet<InformeProgreso> InformeProgresos { get; set; }

    public virtual DbSet<InteraccionCliente> InteraccionClientes { get; set; }

    public virtual DbSet<Proyecto> Proyectos { get; set; }

    public virtual DbSet<ProyectoEmpleado> ProyectoEmpleados { get; set; }

    public virtual DbSet<Tarea> Tareas { get; set; }

    public virtual DbSet<VwAvanceProyecto> VwAvanceProyectos { get; set; }

    public virtual DbSet<VwResumenPresupuestoProyecto> VwResumenPresupuestoProyectos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.ClienteId).HasName("pk_cliente");

            entity.ToTable("cliente", "gestion");

            entity.HasIndex(e => e.Ruc, "uq_cliente_ruc").IsUnique();

            entity.Property(e => e.ClienteId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("cliente_id");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.CorreoContacto)
                .HasMaxLength(150)
                .HasColumnName("correo_contacto");
            entity.Property(e => e.Direccion)
                .HasMaxLength(250)
                .HasColumnName("direccion");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.NombreContacto)
                .HasMaxLength(120)
                .HasColumnName("nombre_contacto");
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(150)
                .HasColumnName("razon_social");
            entity.Property(e => e.Ruc)
                .HasMaxLength(11)
                .HasColumnName("ruc");
            entity.Property(e => e.TelefonoContacto)
                .HasMaxLength(30)
                .HasColumnName("telefono_contacto");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.EmpleadoId).HasName("pk_empleado");

            entity.ToTable("empleado", "gestion");

            entity.HasIndex(e => e.CorreoCorporativo, "uq_empleado_correo").IsUnique();

            entity.HasIndex(e => e.DocumentoIdentidad, "uq_empleado_documento").IsUnique();

            entity.Property(e => e.EmpleadoId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("empleado_id");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(100)
                .HasColumnName("apellidos");
            entity.Property(e => e.Cargo)
                .HasMaxLength(100)
                .HasColumnName("cargo");
            entity.Property(e => e.CorreoCorporativo)
                .HasMaxLength(150)
                .HasColumnName("correo_corporativo");
            entity.Property(e => e.DocumentoIdentidad)
                .HasMaxLength(20)
                .HasColumnName("documento_identidad");
            entity.Property(e => e.FechaIngreso).HasColumnName("fecha_ingreso");
            entity.Property(e => e.Nombres)
                .HasMaxLength(100)
                .HasColumnName("nombres");
            entity.Property(e => e.Telefono)
                .HasMaxLength(30)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<GastoProyecto>(entity =>
        {
            entity.HasKey(e => e.GastoProyectoId).HasName("pk_gasto_proyecto");

            entity.ToTable("gasto_proyecto", "gestion");

            entity.HasIndex(e => new { e.ProyectoId, e.FechaGasto }, "ix_gasto_proyecto_fecha");

            entity.Property(e => e.GastoProyectoId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("gasto_proyecto_id");
            entity.Property(e => e.Categoria)
                .HasMaxLength(80)
                .HasColumnName("categoria");
            entity.Property(e => e.Concepto)
                .HasMaxLength(250)
                .HasColumnName("concepto");
            entity.Property(e => e.FechaGasto).HasColumnName("fecha_gasto");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.Monto)
                .HasPrecision(18, 2)
                .HasColumnName("monto");
            entity.Property(e => e.NumeroComprobante)
                .HasMaxLength(50)
                .HasColumnName("numero_comprobante");
            entity.Property(e => e.Observacion)
                .HasMaxLength(500)
                .HasColumnName("observacion");
            entity.Property(e => e.ProyectoId).HasColumnName("proyecto_id");
            entity.Property(e => e.RegistradoPorEmpleadoId).HasColumnName("registrado_por_empleado_id");

            entity.HasOne(d => d.Proyecto).WithMany(p => p.GastoProyectos)
                .HasForeignKey(d => d.ProyectoId)
                .HasConstraintName("fk_gasto_proyecto_proyecto");

            entity.HasOne(d => d.RegistradoPorEmpleado).WithMany(p => p.GastoProyectos)
                .HasForeignKey(d => d.RegistradoPorEmpleadoId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_gasto_proyecto_empleado");
        });

        modelBuilder.Entity<Hito>(entity =>
        {
            entity.HasKey(e => e.HitoId).HasName("pk_hito");

            entity.ToTable("hito", "gestion");

            entity.HasIndex(e => new { e.ProyectoId, e.Estado }, "ix_hito_proyecto_estado");

            entity.Property(e => e.HitoId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("hito_id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'PENDIENTE'::character varying")
                .HasColumnName("estado");
            entity.Property(e => e.FechaAlcanzada).HasColumnName("fecha_alcanzada");
            entity.Property(e => e.FechaPlanificada).HasColumnName("fecha_planificada");
            entity.Property(e => e.Nombre)
                .HasMaxLength(180)
                .HasColumnName("nombre");
            entity.Property(e => e.ProyectoId).HasColumnName("proyecto_id");

            entity.HasOne(d => d.Proyecto).WithMany(p => p.Hitos)
                .HasForeignKey(d => d.ProyectoId)
                .HasConstraintName("fk_hito_proyecto");
        });

        modelBuilder.Entity<InformeProgreso>(entity =>
        {
            entity.HasKey(e => e.InformeProgresoId).HasName("pk_informe_progreso");

            entity.ToTable("informe_progreso", "gestion");

            entity.HasIndex(e => new { e.ProyectoId, e.FechaInforme }, "ix_informe_proyecto_fecha").IsDescending(false, true);

            entity.Property(e => e.InformeProgresoId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("informe_progreso_id");
            entity.Property(e => e.ElaboradoPorEmpleadoId).HasColumnName("elaborado_por_empleado_id");
            entity.Property(e => e.FechaInforme)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("fecha_informe");
            entity.Property(e => e.PeriodoDesde).HasColumnName("periodo_desde");
            entity.Property(e => e.PeriodoHasta).HasColumnName("periodo_hasta");
            entity.Property(e => e.PorcentajeAvance).HasColumnName("porcentaje_avance");
            entity.Property(e => e.ProximosPasos).HasColumnName("proximos_pasos");
            entity.Property(e => e.ProyectoId).HasColumnName("proyecto_id");
            entity.Property(e => e.Resumen).HasColumnName("resumen");
            entity.Property(e => e.Riesgos).HasColumnName("riesgos");

            entity.HasOne(d => d.ElaboradoPorEmpleado).WithMany(p => p.InformeProgresos)
                .HasForeignKey(d => d.ElaboradoPorEmpleadoId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_informe_progreso_empleado");

            entity.HasOne(d => d.Proyecto).WithMany(p => p.InformeProgresos)
                .HasForeignKey(d => d.ProyectoId)
                .HasConstraintName("fk_informe_progreso_proyecto");
        });

        modelBuilder.Entity<InteraccionCliente>(entity =>
        {
            entity.HasKey(e => e.InteraccionClienteId).HasName("pk_interaccion_cliente");

            entity.ToTable("interaccion_cliente", "gestion");

            entity.HasIndex(e => new { e.ProyectoId, e.FechaHora }, "ix_interaccion_proyecto_fecha").IsDescending(false, true);

            entity.Property(e => e.InteraccionClienteId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("interaccion_cliente_id");
            entity.Property(e => e.Asunto)
                .HasMaxLength(180)
                .HasColumnName("asunto");
            entity.Property(e => e.ContactoCliente)
                .HasMaxLength(120)
                .HasColumnName("contacto_cliente");
            entity.Property(e => e.Detalle).HasColumnName("detalle");
            entity.Property(e => e.EmpleadoId).HasColumnName("empleado_id");
            entity.Property(e => e.FechaHora)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("fecha_hora");
            entity.Property(e => e.ProyectoId).HasColumnName("proyecto_id");
            entity.Property(e => e.TipoInteraccion)
                .HasMaxLength(20)
                .HasColumnName("tipo_interaccion");

            entity.HasOne(d => d.Empleado).WithMany(p => p.InteraccionClientes)
                .HasForeignKey(d => d.EmpleadoId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_interaccion_cliente_empleado");

            entity.HasOne(d => d.Proyecto).WithMany(p => p.InteraccionClientes)
                .HasForeignKey(d => d.ProyectoId)
                .HasConstraintName("fk_interaccion_cliente_proyecto");
        });

        modelBuilder.Entity<Proyecto>(entity =>
        {
            entity.HasKey(e => e.ProyectoId).HasName("pk_proyecto");

            entity.ToTable("proyecto", "gestion");

            entity.HasIndex(e => e.ClienteId, "ix_proyecto_cliente_id");

            entity.HasIndex(e => e.Estado, "ix_proyecto_estado");

            entity.HasIndex(e => e.ResponsableId, "ix_proyecto_responsable_id");

            entity.HasIndex(e => e.Codigo, "uq_proyecto_codigo").IsUnique();

            entity.Property(e => e.ProyectoId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("proyecto_id");
            entity.Property(e => e.ClienteId).HasColumnName("cliente_id");
            entity.Property(e => e.Codigo)
                .HasMaxLength(20)
                .HasColumnName("codigo");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'PLANIFICADO'::character varying")
                .HasColumnName("estado");
            entity.Property(e => e.FechaCierre).HasColumnName("fecha_cierre");
            entity.Property(e => e.FechaFinPlanificada).HasColumnName("fecha_fin_planificada");
            entity.Property(e => e.FechaInicio).HasColumnName("fecha_inicio");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(180)
                .HasColumnName("nombre");
            entity.Property(e => e.Objetivos).HasColumnName("objetivos");
            entity.Property(e => e.PresupuestoEstimado)
                .HasPrecision(18, 2)
                .HasColumnName("presupuesto_estimado");
            entity.Property(e => e.ResponsableId).HasColumnName("responsable_id");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Proyectos)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_proyecto_cliente");

            entity.HasOne(d => d.Responsable).WithMany(p => p.Proyectos)
                .HasForeignKey(d => d.ResponsableId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_proyecto_responsable");
        });

        modelBuilder.Entity<ProyectoEmpleado>(entity =>
        {
            entity.HasKey(e => new { e.ProyectoId, e.EmpleadoId }).HasName("pk_proyecto_empleado");

            entity.ToTable("proyecto_empleado", "gestion");

            entity.HasIndex(e => e.EmpleadoId, "ix_proyecto_empleado_empleado_id");

            entity.Property(e => e.ProyectoId).HasColumnName("proyecto_id");
            entity.Property(e => e.EmpleadoId).HasColumnName("empleado_id");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.FechaAsignacion)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("fecha_asignacion");
            entity.Property(e => e.RolEnProyecto)
                .HasMaxLength(100)
                .HasColumnName("rol_en_proyecto");

            entity.HasOne(d => d.Empleado).WithMany(p => p.ProyectoEmpleados)
                .HasForeignKey(d => d.EmpleadoId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_proyecto_empleado_empleado");

            entity.HasOne(d => d.Proyecto).WithMany(p => p.ProyectoEmpleados)
                .HasForeignKey(d => d.ProyectoId)
                .HasConstraintName("fk_proyecto_empleado_proyecto");
        });

        modelBuilder.Entity<Tarea>(entity =>
        {
            entity.HasKey(e => e.TareaId).HasName("pk_tarea");

            entity.ToTable("tarea", "gestion");

            entity.HasIndex(e => new { e.EmpleadoAsignadoId, e.Estado }, "ix_tarea_empleado_estado");

            entity.HasIndex(e => e.FechaLimite, "ix_tarea_fecha_limite");

            entity.HasIndex(e => new { e.ProyectoId, e.Estado }, "ix_tarea_proyecto_estado");

            entity.Property(e => e.TareaId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("tarea_id");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.EmpleadoAsignadoId).HasColumnName("empleado_asignado_id");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'PENDIENTE'::character varying")
                .HasColumnName("estado");
            entity.Property(e => e.FechaFinalizacion).HasColumnName("fecha_finalizacion");
            entity.Property(e => e.FechaInicio).HasColumnName("fecha_inicio");
            entity.Property(e => e.FechaLimite).HasColumnName("fecha_limite");
            entity.Property(e => e.HorasEstimadas)
                .HasPrecision(8, 2)
                .HasColumnName("horas_estimadas");
            entity.Property(e => e.HorasReales)
                .HasPrecision(8, 2)
                .HasColumnName("horas_reales");
            entity.Property(e => e.PorcentajeAvance).HasColumnName("porcentaje_avance");
            entity.Property(e => e.Prioridad)
                .HasMaxLength(10)
                .HasDefaultValueSql("'MEDIA'::character varying")
                .HasColumnName("prioridad");
            entity.Property(e => e.ProyectoId).HasColumnName("proyecto_id");
            entity.Property(e => e.Titulo)
                .HasMaxLength(180)
                .HasColumnName("titulo");

            entity.HasOne(d => d.ProyectoEmpleado).WithMany(p => p.Tareas)
                .HasForeignKey(d => new { d.ProyectoId, d.EmpleadoAsignadoId })
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_tarea_proyecto_empleado");
        });

        modelBuilder.Entity<VwAvanceProyecto>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_avance_proyecto", "gestion");

            entity.Property(e => e.Codigo)
                .HasMaxLength(20)
                .HasColumnName("codigo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(180)
                .HasColumnName("nombre");
            entity.Property(e => e.PromedioAvanceTareas).HasColumnName("promedio_avance_tareas");
            entity.Property(e => e.ProyectoId).HasColumnName("proyecto_id");
            entity.Property(e => e.TareasCompletadas).HasColumnName("tareas_completadas");
            entity.Property(e => e.TotalTareas).HasColumnName("total_tareas");
        });

        modelBuilder.Entity<VwResumenPresupuestoProyecto>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_resumen_presupuesto_proyecto", "gestion");

            entity.Property(e => e.Codigo)
                .HasMaxLength(20)
                .HasColumnName("codigo");
            entity.Property(e => e.GastoReal)
                .HasPrecision(18, 2)
                .HasColumnName("gasto_real");
            entity.Property(e => e.Nombre)
                .HasMaxLength(180)
                .HasColumnName("nombre");
            entity.Property(e => e.PorcentajeConsumido).HasColumnName("porcentaje_consumido");
            entity.Property(e => e.PresupuestoEstimado)
                .HasPrecision(18, 2)
                .HasColumnName("presupuesto_estimado");
            entity.Property(e => e.ProyectoId).HasColumnName("proyecto_id");
            entity.Property(e => e.SaldoPresupuesto)
                .HasPrecision(18, 2)
                .HasColumnName("saldo_presupuesto");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
