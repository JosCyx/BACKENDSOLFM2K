using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SOLFM2K.Models;

public partial class SolicitudContext : DbContext
{
    public SolicitudContext()
    {
    }

    public SolicitudContext(DbContextOptions<SolicitudContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Presupuesto> Presupuestos { get; set; }
    public virtual DbSet<Aplicacione> Aplicaciones { get; set; }

    public virtual DbSet<Area> Areas { get; set; }

    public virtual DbSet<CabSolCotizacion> CabSolCotizacions { get; set; }

    public virtual DbSet<CabSolOrdenCompra> CabSolOrdenCompras { get; set; }

    public virtual DbSet<CabSolPago> CabSolPagos { get; set; }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<DetSolCotizacion> DetSolCotizacions { get; set; }

    public virtual DbSet<DetSolPago> DetSolPagos { get; set; }

    public virtual DbSet<Documento> Documentos { get; set; }

    public virtual DbSet<EmplNivel> EmpleadoNivel { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<Funcione> Funciones { get; set; }

    public virtual DbSet<ItemSector> ItemSectores { get; set; }   

    public virtual DbSet<NivelesRuteo> NivelesRuteos { get; set; }

    public virtual DbSet<Proveedor> Proveedors { get; set; }

    public virtual DbSet<Prueba> Pruebas { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<RolUsuario> RolUsuarios { get; set; }

    public virtual DbSet<RolTransaccion> RolTransaccions { get; set; }

    public virtual DbSet<Ruteo> Ruteos { get; set; }

    public virtual DbSet<RuteoArea> RuteoAreas { get; set; }
    
    public virtual DbSet<Sector> Sectores { get; set; }

    public virtual DbSet<SolTracking> SolTrackings { get; set; }

    public virtual DbSet<TipoSolic> TipoSolics { get; set; }

    public virtual DbSet<Transaccione> Transacciones { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<ProveedorTemplate> ProveedorTemplates { get; set; }

    public virtual DbSet<CotizacionProveedor> CotizacionProveedors { get; set; }

    public virtual DbSet<ParamsConf> ParamsConfs { get; set; }

    public virtual DbSet<JwtConfig> JwtConfigs { get; set; }

    public virtual DbSet<DestinoSolPago> DestinoSolPagos { get; set; }


    //public virtual DbSet<SolicitudTemplate> SolicitudTemplates { get; set; }


    //extraer cadena de conexion de las variables de entorno y usarla en el metodo OnConfiguring

    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Obtén la cadena de conexión de la variable de entorno
        string connectionString = Environment.GetEnvironmentVariable("DB_CONN");

        // Verifica si la cadena de conexión no es nula o vacía
        if (!string.IsNullOrEmpty(connectionString))
        {
            // Utiliza la cadena de conexión en la configuración del contexto
            optionsBuilder.UseSqlServer(connectionString);
        }
    }*/


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:conn");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<Aplicacione>(entity =>
        {
            entity.HasKey(e => e.ApCodigo);

            entity.ToTable("aplicaciones");

            entity.Property(e => e.ApCodigo).HasColumnName("ap_codigo");
            entity.Property(e => e.ApEmpresa).HasColumnName("ap_empresa");
            entity.Property(e => e.ApEstado)
                .HasMaxLength(1)
                .HasColumnName("ap_estado");
            entity.Property(e => e.ApFuncion).HasColumnName("ap_funcion");
            entity.Property(e => e.ApNemonico)
                .HasMaxLength(6)
                .HasColumnName("ap_nemonico");
            entity.Property(e => e.ApNombre)
                .HasMaxLength(40)
                .HasColumnName("ap_nombre");
            entity.Property(e => e.ApVersion)
                .HasMaxLength(150)
                .HasColumnName("ap_version");
        });

        modelBuilder.Entity<Area>(entity =>
        {
            entity.HasKey(e => e.AreaId).HasName("PK_area");
            entity.ToTable("area");

            entity.Property(e => e.AreaId).HasColumnName("area_id");
            entity.Property(e => e.AreaDecp)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("area_decp");
            entity.Property(e => e.AreaEstado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("area_estado");
            entity.Property(e => e.AreaIdNomina).HasColumnName("area_id_nomina");
            entity.Property(e => e.AreaNemonico)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("area_nemonico");
        });

        modelBuilder.Entity<CabSolCotizacion>(entity =>
        {
            entity.HasKey(e => e.CabSolCotID);

            entity.ToTable("cab_sol_cotizacion");

            entity.Property(e => e.CabSolCotNumerico)
                .HasMaxLength(50)
                .HasColumnName("cab_sol_cot_numerico");

            entity.Property(e => e.CabSolCotID)
                .HasColumnName("cab_sol_cot_ID");

            entity.Property(e => e.CabSolCotTipoSolicitud)
                .HasColumnName("cab_sol_cot_tipo_solicitud");

            entity.Property(e => e.CabSolCotIdDept)
                .HasColumnName("cab_sol_cot_id_dep_solicitante");

            entity.Property(e => e.CabSolCotIdArea)
                .HasColumnName("cab_sol_cot_id_area_solicitante");

            entity.Property(e => e.CabSolCotNoSolicitud)
                .HasColumnName("cab_sol_cot_no_solicitud");

            entity.Property(e => e.CabSolCotSolicitante)
                .HasColumnName("cab_sol_cot_solicitante").HasMaxLength(6);

            entity.Property(e => e.CabSolCotFecha)
                .HasColumnType("date")
                .HasColumnName("cab_sol_cot_fecha");

            entity.Property(e => e.CabSolCotAsunto)
                .HasMaxLength(500)
                .HasColumnName("cab_sol_cot_asunto");

            entity.Property(e => e.CabSolCotProcedimiento)
                .HasMaxLength(500)
                .HasColumnName("cab_sol_cot_procedimiento");

            entity.Property(e => e.CabSolCotObervaciones)
                .HasMaxLength(500)
                .HasColumnName("cab_sol_cot_observaciones");

            entity.Property(e => e.CabSolCotAdjCot)
                .HasMaxLength(2)
                .HasColumnName("cab_sol_cot_adj_cotizacion");

            entity.Property(e => e.CabSolCotNumCotizacion)
                .HasColumnName("cab_sol_cot_num_cotizaciones");

            entity.Property(e => e.CabSolCotEstado)
                .HasMaxLength(5)
                .HasColumnName("cab_sol_cot_estado");

            entity.Property(e => e.CabSolCotEstadoTracking)
                .HasColumnName("cab_sol_cot_estado_track");

            entity.Property(e => e.CabSolCotPlazoEntrega)
                .HasColumnType("date")
                .HasColumnName("cab_sol_cot_plazo_entrega");

            entity.Property(e => e.CabSolCotFechaMaxentrega)
                .HasColumnType("date")
                .HasColumnName("cab_sol_cot_fechamax_entrega");

            entity.Property(e => e.CabSolCotInspector)
                .HasColumnName("cab_sol_cot_inspector");

            entity.Property(e => e.CabSolCotTelefInspector)
                .HasMaxLength(15)
                .HasColumnName("cab_sol_cot_telef_inspector");

            entity.Property(e => e.CabSolCotAprobPresup)
                .HasMaxLength(5)
                .HasColumnName("cab_sol_cot_aprob_presup");

            entity.Property(e => e.CabSolCotMtovioDev)
                .HasMaxLength(5)
                .HasColumnName("cab_sol_cot_motivo_devolucion");

            entity.Property(e => e.CabSolCotIdEmisor)
                .HasColumnName("cab_sol_cot_generate_by");

            entity.Property(e => e.CabSolCotApprovedBy)
                .HasColumnName("cab_sol_cot_approvedBy");

            entity.Property(e => e.CabSolCotFinancieroBy)
                .HasColumnName("cab_sol_cot__financieroBy");


            /*entity.HasOne(d => d.CabSolCotIdCabeceraNavigation).WithOne(p => p.CabSolCotizacion)
                .HasForeignKey<CabSolCotizacion>(d => d.CabSolCotIdCabecera)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cab_sol_cotizacion_tipo_solic");*/
        });

        modelBuilder.Entity<CabSolOrdenCompra>(entity =>
        {
            entity.HasKey(e => e.cabSolOCID);

            entity.ToTable("cab_sol_orden_compra");

            entity.Property(e => e.cabSolOCNumerico)
                .HasMaxLength(50)
                .HasColumnName("cab_ordc_numerico");

            entity.Property(e => e.cabSolOCID)
                .HasColumnName("cab_ordc_ID");

            entity.Property(e => e.cabSolOCTipoSolicitud)
                .HasColumnName("cab_ordc_tipo_solicitud");

            entity.Property(e => e.cabSolOCIdDept)
                .HasColumnName("cab_ordc_id_dep_solicitante");

            entity.Property(e => e.cabSolOCIdArea)
                .HasColumnName("cab_ordc_id_area_solicitante");

            entity.Property(e => e.cabSolOCNoSolicitud)
                .HasColumnName("cab_ordc_no_solicitud");

            entity.Property(e => e.cabSolOCSolicitante)
                .HasColumnName("cab_ordc_solicitante").HasMaxLength(6);

            entity.Property(e => e.cabSolOCFecha)
                .HasColumnType("date")
                .HasColumnName("cab_ordc_fecha");

            entity.Property(e => e.cabSolOCAsunto)
                .HasMaxLength(500)
                .HasColumnName("cab_ordc_asunto");

            entity.Property(e => e.cabSolOCProcedimiento)
                .HasMaxLength(500)
                .HasColumnName("cab_ordc_procedimiento");

            entity.Property(e => e.cabSolOCObervaciones)
                .HasMaxLength(500)
                .HasColumnName("cab_ordc_observaciones");

            entity.Property(e => e.cabSolOCAdjCot)
                .HasMaxLength(2)
                .HasColumnName("cab_ordc_adj_cotizacion");

            entity.Property(e => e.cabSolOCNumCotizacion)
                .HasColumnName("cab_ordc_num_cotizaciones");

            entity.Property(e => e.cabSolOCEstado)
                .HasMaxLength(5)
                .HasColumnName("cab_ordc_estado");

            entity.Property(e => e.cabSolOCEstadoTracking)
                .HasColumnName("cab_ordc_estado_track");

            entity.Property(e => e.cabSolOCPlazoEntrega)
                .HasColumnType("date")
                .HasColumnName("cab_ordc_plazo_entrega");

            entity.Property(e => e.cabSolOCFechaMaxentrega)
                .HasColumnType("date")
                .HasColumnName("cab_ordc_fechamax_entrega");

            entity.Property(e => e.cabSolOCInspector)
                .HasColumnName("cab_ordc_inspector");

            entity.Property(e => e.cabSolOCTelefInspector)
                .HasMaxLength(15)
                .HasColumnName("cab_ordc_telef_inspector");

            entity.Property(e => e.cabSolOCProveedor)
                .HasMaxLength(50)
                .HasColumnName("cab_ordc_proveedor");

            entity.Property(e => e.cabSolOCRUCProveedor)
                .HasMaxLength(25)
                .HasColumnName("cab_ordc_ruc_proveedor");

            entity.Property(e => e.cabSolOCIdEmisor)
                .HasColumnName("cab_ordc_generate_by");

            entity.Property(e => e.cabSolOCApprovedBy)
                .HasColumnName("cab_ordc_approvedBy");

            entity.Property(e => e.cabSolOCFinancieroBy)
                .HasColumnName("cab_ordc_financieroBy");
        });

        modelBuilder.Entity<CabSolPago>(entity =>
        {
            entity.HasKey(e => e.CabPagoID);

            entity.ToTable("cab_sol_pago");

            entity.Property(e => e.CabPagoID)
                .HasColumnName("cab_pago_ID");

            entity.Property(e => e.CabPagoNumerico)
                .IsRequired()
                .HasColumnName("cab_pago_numerico")
                .HasMaxLength(50);

            entity.Property(e => e.CabPagoTipoSolicitud)
                .HasColumnName("cab_pago_tipo_solicitud");

            entity.Property(e => e.CabPagoNoSolicitud)
                .HasColumnName("cab_pago_no_solicitud");

            entity.Property(e => e.CabPagoIdDeptSolicitante)
               .HasColumnName("cab_pago_dep_solicitante");

            entity.Property(e => e.CabPagoIdAreaSolicitante)
                .HasColumnName("cab_pago_area_solicitante"); 

            entity.Property(e => e.CabPagoSolicitante)
                .HasColumnName("cab_pago_solicitante").HasMaxLength(6);

            entity.Property(e => e.CabPagoNoOrdenCompra)
                .IsRequired()
                .HasColumnName("cab_pago_no_orden_compra")
                .HasMaxLength(15);

            entity.Property(e => e.CabPagoFechaEmision)
                .HasColumnName("cab_pago_fecha_emision")
                .HasColumnType("date");

            entity.Property(e => e.CabPagoFechaEnvio)
                .HasColumnName("cab_pago_fecha_envio")
                .HasColumnType("date");

            entity.Property(e => e.CabPagoNumFactura)
                .IsRequired()
                .HasColumnName("cab_pago_num_factura")
                .HasMaxLength(25);

            entity.Property(e => e.CabPagoFechaFactura)
                .HasColumnName("cab_pago_fecha_factura")
                .HasColumnType("date");

            entity.Property(e => e.CabPagoProveedor)
                .HasColumnName("cab_pago_proveedor")
                 .HasMaxLength(50);

            entity.Property(e => e.CabPagoRucProveedor)
                .IsRequired()
                .HasColumnName("cab_pago_ruc_proveedor")
                .HasMaxLength(25);

            entity.Property(e => e.Cabpagototal)
                .HasColumnName("cab_pago_total");
      
            entity.Property(e => e.CabPagoObservaciones)
                .HasColumnName("cab_pago_observaciones")
                .HasMaxLength(500);

            entity.Property(e => e.CabPagoAplicarMulta)
                .HasColumnName("cab_pago_aplicar_multa")
                .HasMaxLength(500);

            entity.Property(e => e.CabPagoValorMulta)
                .HasColumnName("cab_pago_valor_multa");

            entity.Property(e => e.CabPagoValorTotalAut)
                .HasColumnName("cab_pago_valor_total_aut");

            entity.Property(e => e.CabPagoReceptor)
                .HasColumnName("cab_pago_receptor");

            entity.Property(e => e.CabPagoFechaInspeccion)
                .HasColumnName("cab_pago_fecha_inspeccion")
                .HasColumnType("date");

            entity.Property(e => e.CabPagoCancelacionOrden)
                .HasColumnName("cab_pago_cancelacion_orden")
                .HasMaxLength(5);

            entity.Property(e => e.CabPagoEstado)
                .IsRequired()
                .HasColumnName("cab_pago_estado")
                .HasMaxLength(5);

            entity.Property(e => e.CabPagoEstadoTrack)
                .HasColumnName("cab_pago_estado_track");

            entity.Property(e => e.CabPagoIdEmisor)
                .HasColumnName("cab_pago_generate_by");

            entity.Property(e => e.CabPagoApprovedBy)
                .HasColumnName("cab_pago_approvedBy");

            entity.Property(e => e.CabPagoFinancieroBy)
                .HasColumnName("cab_pago_financieroBy");
        });

        /* modelBuilder.Entity<CabSolPago>(entity =>
         {
             entity.HasKey(e => e.CabPagoIdCabecera);

             entity.ToTable("cab_sol_pago");

             entity.Property(e => e.CabPagoIdCabecera)
                 .ValueGeneratedNever()
                 .HasColumnName("cab_pago_id_cabecera");
             entity.Property(e => e.CabPagoFechaFactura)
                 .HasColumnType("date")
                 .HasColumnName("cab_pago_fecha_factura");
             entity.Property(e => e.CabPagoFechaSolPago)
                 .HasColumnType("date")
                 .HasColumnName("cab_pago_fecha_sol_pago");
             entity.Property(e => e.CabPagoIdOrdenCompra).HasColumnName("cab_pago_id_orden_compra");
             entity.Property(e => e.CabPagoNumFactura)
                 .HasMaxLength(20)
                 .IsUnicode(false)
                 .HasColumnName("cab_pago_num_factura");
             entity.Property(e => e.CabPagoProveedor).HasColumnName("cab_pago_proveedor");
             entity.Property(e => e.CabPagoRuc)
                 .HasMaxLength(13)
                 .IsUnicode(false)
                 .HasColumnName("cab_pago_ruc");
             entity.Property(e => e.CabPagoSolicitante).HasColumnName("cab_pago_solicitante");

             entity.HasOne(d => d.CabPagoIdCabeceraNavigation).WithOne(p => p.CabSolPago)
                 .HasForeignKey<CabSolPago>(d => d.CabPagoIdCabecera)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK_cab_sol_pago_tipo_solic");

        entity.HasOne(d => d.CabPagoProveedorNavigation).WithMany(p => p.CabSolPagos)
            .HasForeignKey(d => d.CabPagoProveedor)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_cab_sol_pago_proveedor");
    });*/

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.DepId).HasName("PK_Departamento");

            entity.ToTable("departamento");

            entity.Property(e => e.DepId).HasColumnName("dep_id");
            entity.Property(e => e.DepArea).HasColumnName("dep_area");
            entity.Property(e => e.DepDescp)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("dep_descp");
            entity.Property(e => e.DepEstado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("dep_estado");
            entity.Property(e => e.DepIdNomina).HasColumnName("dep_id_nomina");

            //entity.HasOne(d => d.DepAreaNavigation).WithMany(p => p.Departamentos)
            //    .HasForeignKey(d => d.DepArea)
            //    .HasConstraintName("FK_departamento_area");
        });

        modelBuilder.Entity<DetSolCotizacion>(entity =>
        {
            entity.HasKey(e => e.SolCotID);

            entity.ToTable("det_sol_cotizacion");

            entity.Property(e => e.SolCotID)
                .HasColumnName("sol_cot_ID");

            entity.Property(e => e.SolCotTipoSol)
                .HasColumnName("sol_cot_id_tipo_sol");

            entity.Property(e => e.SolCotNoSol)
                .HasColumnName("sol_cot_id_no_sol");

            entity.Property(e => e.SolCotIdDetalle)
                .HasColumnName("sol_cot_id_detalle");

            entity.Property(e => e.SolCotDescripcion)
                .HasMaxLength(500)
                .HasColumnName("sol_cot_descripcion");

            entity.Property(e => e.SolCotUnidad)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("sol_cot_unidad");

            entity.Property(e => e.SolCotCantidadTotal)
                .HasColumnName("sol_cot_cantidad_total");

            entity.Property(e => e.SolCotPresupuesto)
                .HasMaxLength(100)
                .HasColumnName("sol_cot_presupuesto");

            //entity.Property(e => e.AudEvento)
            //    .HasMaxLength(50)
            //    .HasColumnName("sol_cot_aud_evento");

            //entity.Property(e => e.AudFecha)
            //    .HasColumnType("date")
            //    .HasColumnName("sol_cot_aud_fecha");

            //entity.Property(e => e.AudObservacion)
            //    .HasMaxLength(250)
            //    .HasColumnName("sol_cot_aud_observacion");

            //entity.Property(e => e.AudUsuario)
            //    .HasMaxLength(50)
            //    .HasColumnName("sol_cot_aud_usuario");

            //entity.Property(e => e.AudVeces)
            //    .HasColumnName("sol_cot_aud_veces");



            /*entity.HasOne(d => d.SolCotIdCabeceraNavigation).WithMany(p => p.SolCotizacions)
                .HasForeignKey(d => d.SolCotIdCabecera)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_sol_cotizacion_cab_sol_cotizacion");*/

            /*entity.HasOne(d => d.SolCotItemNavigation).WithMany(p => p.SolCotizacions)
                .HasForeignKey(d => d.SolCotItem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_sol_cotizacion_item");*/
        });

        //editar a detsolpago
        modelBuilder.Entity<DetSolPago>(entity =>
        {
            entity.HasKey(e => e.DetPagoID);

            entity.ToTable("det_sol_pago"); // Reemplaza "nombre_de_la_tabla" con el nombre real de tu tabla en la base de datos.

            entity.Property(e => e.DetPagoID)
                .HasColumnName("det_pago_ID")
                .IsRequired(); // Esto indica que la columna es requerida.

            entity.Property(e => e.DetPagoItemDesc)
                .HasColumnName("det_pago_item_desc")
                .HasMaxLength(255); // Puedes ajustar el tamaño máximo según tus necesidades.

            entity.Property(e => e.DetPagoCantContratada)
                .HasColumnName("det_pago_cant_contratada");

            entity.Property(e => e.DetPagoCantRecibida)
                .HasColumnName("det_pago_cant_recibida");

            entity.Property(e => e.DetPagoValUnitario)
                .HasColumnName("det_pago_val_unitario");

            entity.Property(e => e.DetPagoSubtotal)
                .HasColumnName("det_pago_subtotal");

            entity.Property(e => e.DetPagoTipoSol)
                .HasColumnName("det_pago_tipo_solicitud");

            entity.Property(e => e.DetPagoNoSol)
                .HasColumnName("det_pago_no_solicitud");

            entity.Property(e => e.DetPagoIdDetalle)
                .HasColumnName("det_pago_id_detalle");
        });

        modelBuilder.Entity<Documento>(entity =>
        {
            entity
                .ToTable("documentos");
            entity.HasKey(e => e.DocId).HasName("PK_documentos");
            entity.Property(e => e.DocEstado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("doc_estado");
            entity.Property(e => e.DocId)
                .ValueGeneratedOnAdd()
                .HasColumnName("doc_id");
            entity.Property(e => e.DocNoSolicitud).HasColumnName("doc_no_solicitud");
            entity.Property(e => e.DocTipoSolicitud).HasColumnName("doc_tipo_solicitud");
            entity.Property(e => e.DocUrl)
                .HasMaxLength(300)
                .HasColumnName("doc_URL");

            entity.Property(e => e.DocNombre)
                .HasMaxLength(100)
                .HasColumnName("doc_nombre");

        });

        modelBuilder.Entity<EmplNivel>(entity =>
        {
            entity.ToTable("empleado_nivel");
            entity.HasKey(e => e.EmpNivId).HasName("PK_niv_gerencia");

            entity.Property(e => e.EmpNivId).HasColumnName("emp_niv_id");
            entity.Property(e => e.EmpNivEmpelado)
                .HasMaxLength(6)
                .IsFixedLength()
                .HasColumnName("emp_niv_id_nomina");
            entity.Property(e => e.EmpNivDeptAutorizado)
                .HasColumnName("emp_niv_dept_aut");
            entity.Property(e => e.EmpNivRuteo)
                .HasColumnName("emp_niv_ruteo");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.ToTable("empleados");

            entity.HasKey(e => e.EmpleadoId)
                .HasName("PK_empleados");

            entity.Property(e => e.EmpleadoId).HasColumnName("empleado_id");
            entity.Property(e => e.EmpleadoCompania).HasColumnName("empleado_compania");
            entity.Property(e => e.EmpleadoIdNomina).HasColumnName("empleado_id_nomina").HasMaxLength(6);
            entity.Property(e => e.EmpleadoIdDpto).HasColumnName("empleado_id_dpto");
            entity.Property(e => e.EmpleadoIdArea).HasColumnName("empleado_id_area");
            entity.Property(e => e.EmpleadoDpto).HasColumnName("empleado_departamento").HasMaxLength(100);
            entity.Property(e => e.EmpleadoArea).HasColumnName("empleado_area").HasMaxLength(100);
            entity.Property(e => e.EmpleadoIdentificacion)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("empleado_identificacion");
            entity.Property(e => e.EmpleadoNombres)
                .HasMaxLength(100)
                .HasColumnName("empleado_nombres");
            entity.Property(e => e.EmpleadoApellidos)
                .HasMaxLength(100)
                .HasColumnName("empleado_apellidos");
            entity.Property(e => e.EmpleadoTelefono)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("empleado_telefono");

            entity.Property(e => e.EmpleadoCorreo)
                .HasMaxLength(100)
                .HasColumnName("empleado_correo");

            entity.Property(e => e.EmpleadoEstado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("empleado_estado");

            entity.Property(e => e.EmpleadoCargo).HasColumnName("empleado_cargo").HasMaxLength(100);

            
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.EmCodigo);

            entity.ToTable("empresa");

            entity.Property(e => e.EmCodigo).HasColumnName("em_codigo");
            entity.Property(e => e.EmAmbiente)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("em_ambiente");
            entity.Property(e => e.EmApruebaOg)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("em_apruebaOG");
            entity.Property(e => e.EmAutorizaCupo)
                .HasMaxLength(20)
                .HasColumnName("em_AutorizaCupo");
            entity.Property(e => e.EmAutorizacion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("em_autorizacion");
            entity.Property(e => e.EmBase)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("em_Base");
            entity.Property(e => e.EmClaveFirma)
                .HasMaxLength(50)
                .HasColumnName("em_ClaveFirma");
            entity.Property(e => e.EmClaveNoRepetir).HasColumnName("em_Clave_NoRepetir");
            entity.Property(e => e.EmCliente).HasColumnName("em_Cliente");
            entity.Property(e => e.EmComanda)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("em_comanda");
            entity.Property(e => e.EmCtaAjusctvs)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("em_cta_ajusctvs");
            entity.Property(e => e.EmCtaAjusctvsD)
                .HasMaxLength(25)
                .HasColumnName("em_cta_ajusctvsD");
            entity.Property(e => e.EmCtaBono)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("em_cta_bono");
            entity.Property(e => e.EmCtaCpp)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("em_cta_cpp");
            entity.Property(e => e.EmCtaExpTrans)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("em_cta_exp_trans");
            entity.Property(e => e.EmCtaFalPrecio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("em_cta_Fal_Precio");
            entity.Property(e => e.EmCtaLiq)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("em_cta_liq");
            entity.Property(e => e.EmCtaPedidosTran)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("em_ctaPedidosTran");
            entity.Property(e => e.EmCtaSobranteLiquidacion)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("em_cta_Sobrante_liquidacion");
            entity.Property(e => e.EmCtactocto)
                .HasMaxLength(25)
                .HasColumnName("em_ctactocto");
            entity.Property(e => e.EmCtadescomp)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("em_ctadescomp");
            entity.Property(e => e.EmCtaxpfruta)
                .HasMaxLength(25)
                .HasColumnName("em_ctaxpfruta");
            entity.Property(e => e.EmCuentaUtilidad)
                .HasMaxLength(20)
                .HasColumnName("em_cuentaUtilidad");
            entity.Property(e => e.EmDiasExtCaducado).HasColumnName("em_dias_ext_caducado");
            entity.Property(e => e.EmDireccion)
                .HasMaxLength(130)
                .IsUnicode(false)
                .HasColumnName("em_direccion");
            entity.Property(e => e.EmEmail)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("em_email");
            entity.Property(e => e.EmEmision)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("em_emision");
            entity.Property(e => e.EmEstado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("em_estado");
            entity.Property(e => e.EmFax)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("em_fax");
            entity.Property(e => e.EmFechacad)
                .HasColumnType("datetime")
                .HasColumnName("em_fechacad");
            entity.Property(e => e.EmFechaini)
                .HasColumnType("datetime")
                .HasColumnName("em_fechaini");
            entity.Property(e => e.EmFechareg)
                .HasColumnType("datetime")
                .HasColumnName("em_fechareg");
            entity.Property(e => e.EmFruta)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("em_fruta");
            entity.Property(e => e.EmGeneraCodP)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("em_GeneraCodP");
            entity.Property(e => e.EmGerente)
                .HasMaxLength(50)
                .HasColumnName("em_gerente");
            entity.Property(e => e.EmGrabaFox)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("em_grabaFox");
            entity.Property(e => e.EmImagen)
                .HasColumnType("image")
                .HasColumnName("em_imagen");
            entity.Property(e => e.EmImpcomanda)
                .HasMaxLength(200)
                .HasColumnName("em_Impcomanda");
            entity.Property(e => e.EmLlevaContabilidad).HasColumnName("em_LlevaContabilidad");
            entity.Property(e => e.EmManApertCaja)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("em_ManApertCaja");
            entity.Property(e => e.EmMaquina)
                .HasMaxLength(50)
                .HasColumnName("em_maquina");
            entity.Property(e => e.EmNombre)
                .HasMaxLength(50)
                .HasColumnName("em_nombre");
            entity.Property(e => e.EmNotificacion).HasColumnName("em_notificacion");
            entity.Property(e => e.EmNumContribuyente)
                .HasMaxLength(5)
                .HasColumnName("em_NumContribuyente");
            entity.Property(e => e.EmNumEqui).HasColumnName("em_num_equi");
            entity.Property(e => e.EmPorseguro).HasColumnName("em_porseguro");
            entity.Property(e => e.EmRazonSocial)
                .HasMaxLength(350)
                .IsUnicode(false)
                .HasColumnName("em_razonSocial");
            entity.Property(e => e.EmResolucont).HasColumnName("em_resolucont");
            entity.Property(e => e.EmRuc)
                .HasMaxLength(13)
                .HasColumnName("em_ruc");
            entity.Property(e => e.EmSa)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("em_sa");
            entity.Property(e => e.EmServerData)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("em_server_data");
            entity.Property(e => e.EmServidor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("em_Servidor");
            entity.Property(e => e.EmTelInt)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("em_tel_int");
            entity.Property(e => e.EmTelefonos)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("em_telefonos");
            entity.Property(e => e.EmTiempoCaduca).HasColumnName("em_tiempoCaduca");
            entity.Property(e => e.EmTipoNegocio).HasColumnName("em_tipo_negocio");
            entity.Property(e => e.EmTipoValPtoemi).HasColumnName("em_Tipo_valPtoemi");
            entity.Property(e => e.EmUsuario)
                .HasMaxLength(50)
                .HasColumnName("em_usuario");
        });

        modelBuilder.Entity<Funcione>(entity =>
        {
            entity.HasKey(e => e.FuCodigo);

            entity.ToTable("funciones");

            entity.Property(e => e.FuCodigo).HasColumnName("fu_codigo");
            entity.Property(e => e.FuAplicacion).HasColumnName("fu_aplicacion");
            entity.Property(e => e.FuEmpresa).HasColumnName("fu_empresa");
            entity.Property(e => e.FuEstado)
                .HasMaxLength(1)
                .HasColumnName("fu_estado");
            entity.Property(e => e.FuNombre)
                .HasMaxLength(40)
                .HasColumnName("fu_nombre");
            entity.Property(e => e.FuTransaccion).HasColumnName("fu_transaccion");
        });

        modelBuilder.Entity<ItemSector>(entity =>
        {

            entity.HasKey(e => e.ItmID).HasName("PK_item_sector");

            entity.ToTable("item_sector");
            entity.Property(e => e.ItmID).HasColumnName("itm_sect_ID");

            entity.Property(e => e.ItmTipoSol).HasColumnName("itm_sect_tipo_solicitud");

            entity.Property(e => e.ItmNumSol).HasColumnName("itm_sect_no_solicitud");

            entity.Property(e => e.ItmIdDetalle).HasColumnName("itm_sect_id_detalle");

            entity.Property(e => e.ItmIdItem).HasColumnName("itm_sect_id_item");

            entity.Property(e => e.ItmCantidad).HasColumnName("itm_sect_cantidad");

            entity.Property(e => e.ItmSector).HasColumnName("itm_sect_sector");

        });

        modelBuilder.Entity<NivelesRuteo>(entity =>
        {
            entity.HasKey(e => e.CodRuteo).HasName("PK_niveles_ruteo");

            entity.ToTable("nivel_ruteo");

            entity.Property(e => e.CodRuteo).HasColumnName("ruteo_id");
            entity.Property(e => e.DescRuteo)
                .HasMaxLength(50)
                .HasColumnName("ruteo_desc");
            entity.Property(e => e.EstadoRuteo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ruteo_estado");
            entity.Property(e => e.Nivel).HasColumnName("ruteo_nivel");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.ProvId);

            entity.ToTable("proveedor");

            entity.Property(e => e.ProvId)
                .HasColumnName("prov_id");
            entity.Property(e => e.ProvRuc)
                .HasMaxLength(20)
                .HasColumnName("prov_ruc");
            entity.Property(e => e.ProvNombre)
                .HasMaxLength(100)
                .HasColumnName("prov_nombre");
            entity.Property(e => e.ProvAlias)
                .HasMaxLength(100)
                .HasColumnName("prov_alias");
            entity.Property(e => e.ProvTelefono)
                .HasMaxLength(15)
                .HasColumnName("prov_telefono");
            entity.Property(e => e.ProvCorreo)
                .HasMaxLength(50)
                .HasColumnName("prov_correo");

            entity.Property(e => e.ProvCiudad)
                .HasMaxLength(50)
                .HasColumnName("prov_ciudad");
            entity.Property(e => e.ProvProvincia)
                .HasMaxLength(50)
                .HasColumnName("prov_provincia");
            entity.Property(e => e.ProvPais)
                .HasMaxLength(50)
                .HasColumnName("prov_pais");
            
        });
        modelBuilder.Entity<ProveedorTemplate>(entity =>
        {
           

            entity.ToTable("proveedorTmp");
            entity.HasNoKey();

            entity.Property(e => e.prov_alias)
                .HasMaxLength(100)
                .HasColumnName("prvtmp_alias");
            entity.Property(e => e.prov_nombre)
                .HasMaxLength(100)
                .HasColumnName("prvtmp_nombre");
            entity.Property(e => e.prov_ruc)
                .HasMaxLength(20)
                .HasColumnName("prvtmp_ruc");
            entity.Property(e => e.prov_telefono)
                .HasMaxLength(15)
                .HasColumnName("prvtmp_telefono");
          

            

        });

        modelBuilder.Entity<Prueba>(entity =>
        {
            entity.ToTable("prueba");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .HasColumnName("apellido");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.RoCodigo).HasName("PK_tb_rol");

            entity.ToTable("rol");

            entity.Property(e => e.RoCodigo).HasColumnName("ro_codigo");
            entity.Property(e => e.RoEmpresa).HasColumnName("ro_empresa");
            entity.Property(e => e.RoEstado)
                .HasMaxLength(1)
                .HasColumnName("ro_estado");
            entity.Property(e => e.RoNombre)
                .HasMaxLength(40)
                .HasColumnName("ro_nombre");
            entity.Property(e => e.RoNivelRt).HasColumnName("ro_nivel_rt");

            /*entity.HasOne(d => d.RoAplicacionNavigation).WithMany(p => p.Rols)
                .HasForeignKey(d => d.RoAplicacion)
                .HasConstraintName("FK_rol_aplicaciones");*/
        });

        modelBuilder.Entity<RolUsuario>(entity =>
        {
            entity.HasKey(e => e.RuId).HasName("PK_tb_rol_usuario");

            entity.ToTable("rol_usuario");

            entity.Property(e => e.RuId).HasColumnName("ru_id");
            entity.Property(e => e.RuEmpresa).HasColumnName("ru_empresa");
            entity.Property(e => e.RuEstado)
                .HasMaxLength(1)
                .HasColumnName("ru_estado");
            entity.Property(e => e.RuLogin)
                .HasMaxLength(20)
                .HasColumnName("ru_login");
            entity.Property(e => e.RuRol).HasColumnName("ru_rol");
        });

        modelBuilder.Entity<RolTransaccion>(entity =>
        {
            entity.ToTable("rol_transaccion");

            entity.HasKey(e => e.RtId).HasName("PK_tb_rol_transaccion");

            entity.Property(e => e.RtId)
                  .HasColumnName("rt_id");
            entity.Property(e => e.RtEmpresa)
                  .HasColumnName("rt_empresa");
            entity.Property(e => e.RtRol)
                  .HasColumnName("rt_rol");
            entity.Property(e => e.RtTransaccion)
                    .HasColumnName("rt_transaccion");
            entity.Property(e => e.RtEstado)
                  .HasMaxLength(1)
                  .HasColumnName("rt_estado");
        });

        modelBuilder.Entity<Ruteo>(entity =>
        {
            entity.HasKey(e => e.RutCod);

            entity.ToTable("ruteo");

            entity.Property(e => e.RutCod).HasColumnName("rut_cod");
            entity.Property(e => e.RutArea).HasColumnName("rut_area");
            entity.Property(e => e.RutEstado)
                .HasMaxLength(1)
                .HasColumnName("rut_estado");
            entity.Property(e => e.RutNombre)
                .HasMaxLength(100)
                .HasColumnName("rut_nombre");
        });

        modelBuilder.Entity<RuteoArea>(entity =>
        {
            entity.ToTable("ruteo_area");
            entity.HasKey(e => e.RutareaId);

            entity.Property(e => e.RutareaId)
                .HasColumnName("rutar_id");
            entity.Property(e => e.RutareaTipoSol)
                .HasColumnName("rutar_tipo_sol");
            entity.Property(e => e.RutareaArea)
                .HasColumnName("rutar_area");
            entity.Property(e => e.RutareaNivel)
                .HasColumnName("rutar_nivel");
            
        });

        
        modelBuilder.Entity<Sector>(entity =>
        {
            entity.HasKey(e => e.SectId);

            entity.ToTable("sectores");

            entity.Property(e => e.SectId)
                .HasColumnName("sect_ID");

            entity.Property(e => e.SectIdNomina)
                .HasColumnName("sect_Id_Nomina");

            entity.Property(e => e.SectNombre)
                .HasMaxLength(50)
                .HasColumnName("sect_Nombre");
        });


        modelBuilder.Entity<SolTracking>(entity =>
        {

            entity.HasKey(e => e.SolTrId).HasName("PK_sol_tracking");

            entity.ToTable("sol_tracking");

            entity.Property(e => e.SolTrId)
                .HasColumnName("sol_trck_id");

            entity.Property(e => e.SolTrTipoSol)
                .HasColumnName("sol_trck_tipo_solicitud");

            entity.Property(e => e.SolTrNumSol)
                .HasColumnName("sol_trck_no_solicitud");

            entity.Property(e => e.SolTrNivel)
                .HasColumnName("sol_trck_nivel");

            entity.Property(e => e.SolTrIdEmisor)
                .HasColumnName("sol_trck_id_nomina_emisor").HasMaxLength(6);
        });


        modelBuilder.Entity<TipoSolic>(entity =>
        {
            entity.HasKey(e => e.TipoSolId);

            entity.ToTable("tipo_solic");

            entity.Property(e => e.TipoSolId).HasColumnName("tipo_sol_id");
            entity.Property(e => e.TipoSolInicial)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("tipo_sol_inicial");
            entity.Property(e => e.TipoSolNombre)
                .HasMaxLength(50)
                .HasColumnName("tipo_sol_nombre");
            entity.Property(e => e.TipoSolEstado)
                .HasMaxLength(1)
                .HasColumnName("tipo_sol_estado");
        });

        modelBuilder.Entity<Transaccione>(entity =>
        {
            entity.HasKey(e => e.TrCodigo);

            entity.ToTable("transacciones");

            entity.Property(e => e.TrCodigo).HasColumnName("tr_codigo");
            entity.Property(e => e.TrAplicacion).HasColumnName("tr_aplicacion");
            entity.Property(e => e.TrEmpresa).HasColumnName("tr_empresa");
            entity.Property(e => e.TrEstado)
                .HasMaxLength(1)
                .HasColumnName("tr_estado");
            entity.Property(e => e.TrFuncion).HasColumnName("tr_funcion");
            entity.Property(e => e.TrNombre)
                .HasMaxLength(40)
                .HasColumnName("tr_nombre");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsId);

            entity.ToTable("usuarios");

            entity.Property(e => e.UsId)
                .HasColumnName("us_ID");
            entity.Property(e => e.AudEvento)
                .HasMaxLength(50)
                .HasColumnName("aud_evento");
            entity.Property(e => e.AudFecha)
                .HasColumnType("date")
                .HasColumnName("aud_fecha");
            entity.Property(e => e.AudObservacion)
                .HasMaxLength(250)
                .HasColumnName("aud_observacion");
            entity.Property(e => e.AudUsuario)
                .HasMaxLength(50)
                .HasColumnName("aud_usuario");
            entity.Property(e => e.AudVeces)
                .HasColumnName("aud_veces");
            entity.Property(e => e.UsBanUserData)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("us_ban_user_data");
            entity.Property(e => e.UsContrasenia)
                .HasMaxLength(100)
                .HasColumnName("us_contrasenia");
            entity.Property(e => e.UsEmpresa)
                .HasColumnName("us_empresa");
            entity.Property(e => e.UsEstado)
                .HasMaxLength(1)
                .HasColumnName("us_estado");
            entity.Property(e => e.UsFechaCaduca)
                .HasColumnType("date")
                .HasColumnName("us_fecha_caduca");
            entity.Property(e => e.UsFechaInicio)
                .HasColumnType("date")
                .HasColumnName("us_fecha_inicio");
            entity.Property(e => e.UsLogin)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("us_login");
            entity.Property(e => e.UsIdNomina)
                .HasColumnName("us_id_nomina").HasMaxLength(6);
            entity.Property(e => e.UsNombre)
                .HasMaxLength(100)
                .HasColumnName("us_nombre");
            entity.Property(e => e.UsServicioC)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("us_ServicioC");
            entity.Property(e => e.UsTipoAcceso)
                .HasColumnName("us_tipo_acceso");
            entity.Property(e => e.UsUserData)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("us_user_data");
        });

        modelBuilder.Entity<CotizacionProveedor>(entity =>
        {
            entity.HasKey(e => e.CotProvId).HasName("PK_proveedor");

            entity.ToTable("cotizacion_proveedor");

            entity.Property(e => e.CotProvId).HasColumnName("cot_prov_id");
            entity.Property(e => e.CotProvCorreo)
                .HasMaxLength(50)
                .HasColumnName("cot_prov_correo");
            entity.Property(e => e.CotProvDireccion)
                .HasMaxLength(50)
                .HasColumnName("cot_prov_direccion");
            entity.Property(e => e.CotProvNoSolicitud).HasColumnName("cot_prov_no_solicitud");
            entity.Property(e => e.CotProvNombre)
                .HasMaxLength(100)
                .HasColumnName("cot_prov_nombre");
            entity.Property(e => e.CotProvRuc)
                .HasMaxLength(20)
                .HasColumnName("cot_prov_ruc");
            entity.Property(e => e.CotProvTelefono)
                .HasMaxLength(15)
                .HasColumnName("cot_prov_telefono");
            entity.Property(e => e.CotProvTipoSolicitud).HasColumnName("cot_prov_tipo_solicitud");
            entity.Property(e => e.CotProvVerify).HasColumnName("cot_prov_verify");
        });

        modelBuilder.Entity<ParamsConf>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_params_conf");

            entity
                .ToTable("params_conf");

            entity.Property(e => e.Id).HasColumnName("param_ID");
            entity.Property(e => e.Pass)
                .HasMaxLength(80)
                .HasColumnName("param_pass");
            entity.Property(e => e.Status)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("param_status");
            entity.Property(e => e.Content)
                .HasMaxLength(50)
                .HasColumnName("param_content");
            entity.Property(e => e.Identify)
                .HasMaxLength(50)
                .HasColumnName("param_identify");
        });

        modelBuilder.Entity<JwtConfig>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("jwt_config");

            entity.Property(e => e.JwtAudence)
                .HasMaxLength(100)
                .HasColumnName("jwt_audence");
            entity.Property(e => e.JwtIssuer)
                .HasMaxLength(100)
                .HasColumnName("jwt_issuer");
            entity.Property(e => e.JwtSecretKey)
                .HasMaxLength(100)
                .HasColumnName("jwt_secretKey");
        });

        modelBuilder.Entity<DestinoSolPago>(entity =>
        {
            entity.HasKey(e => e.DestPagId);

            entity.ToTable("destino_sol_pago");

            entity.Property(e => e.DestPagId).HasColumnName("dest_pag_id");
            entity.Property(e => e.DestPagEmpleado).HasColumnName("dest_pag_empleado");
            entity.Property(e => e.DestPagEvidencia)
                .HasMaxLength(200)
                .HasColumnName("dest_pag_evidencia");
            entity.Property(e => e.DestPagIdDetalle).HasColumnName("dest_pag_id_detalle");
            entity.Property(e => e.DestPagNoSol).HasColumnName("dest_pag_no_sol");
            entity.Property(e => e.DestPagObervacion)
                .HasMaxLength(500)
                .HasColumnName("dest_pag_obervacion");
            entity.Property(e => e.DestPagSector).HasColumnName("dest_pag_sector");
            entity.Property(e => e.DestPagTipoSol).HasColumnName("dest_pag_tipo_sol");
        });

        modelBuilder.Entity<Presupuesto>(entity =>
        {
            entity
                .ToTable("presupuesto");

            entity.HasKey(e => e.PrespId).HasName("PK_presupuesto");

            entity.Property(e => e.PrespId)
                .ValueGeneratedOnAdd()
                .HasColumnName("presp_id");
            entity.Property(e => e.PrespNombre)
                .HasMaxLength(100)
                .HasColumnName("presp_nombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
