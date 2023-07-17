using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SOLFM2K.Models;

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

    public virtual DbSet<Aplicacione> Aplicaciones { get; set; }

    public virtual DbSet<CabSolCotizacion> CabSolCotizacions { get; set; }

    public virtual DbSet<CabSolOrdenCompra> CabSolOrdenCompras { get; set; }

    public virtual DbSet<CabSolPago> CabSolPagos { get; set; }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<Documento> Documentos { get; set; }

    public virtual DbSet<EmplNivel> EmplNivels { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<Funcione> Funciones { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<NivelesRuteo> NivelesRuteos { get; set; }

    public virtual DbSet<Proveedor> Proveedors { get; set; }

    public virtual DbSet<Prueba> Pruebas { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<RolUsuario> RolUsuarios { get; set; }

    public virtual DbSet<RuteoArea> RuteoAreas { get; set; }

    public virtual DbSet<Sector> Sectors { get; set; }

    public virtual DbSet<Sectore> Sectores { get; set; }

    public virtual DbSet<SolCotizacion> SolCotizacions { get; set; }

    public virtual DbSet<SolOrdenCompra> SolOrdenCompras { get; set; }

    public virtual DbSet<SolPago> SolPagos { get; set; }

    public virtual DbSet<TipoIdentificacion> TipoIdentificacions { get; set; }

    public virtual DbSet<Transaccione> Transacciones { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<TipoSolicitud> TipoSolicituds { get; set; }


    public virtual DbSet<Ruteo> Ruteos { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:conn");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Area>(entity =>
        {
            entity.HasKey(e => e.AreaId);

            entity.ToTable("area");

            entity.Property(e => e.AreaId).HasColumnName("area_id");
            entity.Property(e => e.AreaEstado)
                .HasMaxLength(1)
                .HasColumnName("area_estado");
            entity.Property(e => e.AreaDescp)
                .HasMaxLength(50)
                .HasColumnName("area_decp");
        });

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

        modelBuilder.Entity<CabSolCotizacion>(entity =>
        {
            entity.HasKey(e => e.CabSolCotIdCabecera);

            entity.ToTable("cab_sol_cotizacion");

            entity.Property(e => e.CabSolCotIdCabecera)
                .ValueGeneratedNever()
                .HasColumnName("cab_sol_cot_id_cabecera");
            entity.Property(e => e.CabSolCotAreaSolicitante)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("cab_sol_cot_area_solicitante");
            entity.Property(e => e.CabSolCotAsunto)
                .HasMaxLength(500)
                .HasColumnName("cab_sol_cot_asunto");
            entity.Property(e => e.CabSolCotFecha)
                .HasColumnType("date")
                .HasColumnName("cab_sol_cot_fecha");
            entity.Property(e => e.CabSolCotSolicitante).HasColumnName("cab_sol_cot_solicitante");

            //entity.HasOne(d => d.CabSolCotSolicitanteNavigation).WithMany(p => p.CabSolCotizacions)
            //    .HasForeignKey(d => d.CabSolCotSolicitante)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_cab_sol_cotizacion_empleado");
        });

        modelBuilder.Entity<CabSolOrdenCompra>(entity =>
        {
            entity.HasKey(e => e.CabOrdcIdCabecera);

            entity.ToTable("cab_sol_orden_compra");

            entity.Property(e => e.CabOrdcIdCabecera)
                .ValueGeneratedNever()
                .HasColumnName("cab_ordc_id_cabecera");
            entity.Property(e => e.CabOrdcAreaSolicitante)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cab_ordc_area_solicitante");
            entity.Property(e => e.CabOrdcAsunto)
                .HasMaxLength(500)
                .HasColumnName("cab_ordc_asunto");
            entity.Property(e => e.CabOrdcFecha)
                .HasColumnType("date")
                .HasColumnName("cab_ordc_fecha");
            entity.Property(e => e.CabOrdcProveedor).HasColumnName("cab_ordc_proveedor");
            entity.Property(e => e.CabOrdcRuc)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("cab_ordc_ruc");
            entity.Property(e => e.CabOrdcSolicitante).HasColumnName("cab_ordc_solicitante");

            entity.HasOne(d => d.CabOrdcProveedorNavigation).WithMany(p => p.CabSolOrdenCompras)
                .HasForeignKey(d => d.CabOrdcProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cab_sol_orden_compra_proveedor");

            //entity.HasOne(d => d.CabOrdcSolicitanteNavigation).WithMany(p => p.CabSolOrdenCompras)
            //    .HasForeignKey(d => d.CabOrdcSolicitante)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_cab_sol_orden_compra_empleado");
        });

        modelBuilder.Entity<CabSolPago>(entity =>
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

            entity.HasOne(d => d.CabPagoProveedorNavigation).WithMany(p => p.CabSolPagos)
                .HasForeignKey(d => d.CabPagoProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cab_sol_pago_proveedor");

            //entity.HasOne(d => d.CabPagoSolicitanteNavigation).WithMany(p => p.CabSolPagos)
            //    .HasForeignKey(d => d.CabPagoSolicitante)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_cab_sol_pago_empleado");
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.DepId);

            entity.ToTable("departamento");

            entity.Property(e => e.DepArea).HasColumnName("dep_area");
            entity.Property(e => e.DepId)
                .ValueGeneratedNever()
                .HasColumnName("dep_id");
            entity.Property(e => e.DepDescp)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("dep_descp");
            entity.Property(e => e.DepEstado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("dep_estado");
        });

        modelBuilder.Entity<Documento>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("documentos");

            entity.Property(e => e.DocClave).HasColumnName("doc_clave");
            entity.Property(e => e.DocDescripcion)
                .HasMaxLength(50)
                .HasColumnName("doc_descripcion");
            entity.Property(e => e.DocEstado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("doc_estado");
        });

        modelBuilder.Entity<EmplNivel>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("empl_nivel");

            entity.Property(e => e.Area).HasColumnName("area");
            entity.Property(e => e.Empleado)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("empleado");
            entity.Property(e => e.Nivel).HasColumnName("nivel");
            entity.Property(e => e.TipoDeDocumento).HasColumnName("tipo_de_documento");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.ToTable("empleado");

            entity.Property(e => e.EmpleadoId)
                .ValueGeneratedNever()
                .HasColumnName("empleado_id");
            entity.Property(e => e.EmpleadoApellidos)
                .HasMaxLength(250)
                .HasColumnName("empleado_apellidos");
            entity.Property(e => e.EmpleadoCompania).HasColumnName("empleado_compania");
            entity.Property(e => e.EmpleadoCorreo)
                .HasMaxLength(100)
                .HasColumnName("empleado_correo");
            entity.Property(e => e.EmpleadoIdDpto).HasColumnName("empleado_id_dpto");
            entity.Property(e => e.EmpleadoIdentificacion)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("empleado_identificacion");
            entity.Property(e => e.EmpleadoNombres)
                .HasMaxLength(250)
                .HasColumnName("empleado_nombres");
            entity.Property(e => e.EmpleadoSexo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("empleado_sexo");
            entity.Property(e => e.EmpleadoTelefono)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("empleado_telefono");
            entity.Property(e => e.EmpleadoTipoId).HasColumnName("empleado_tipo_id");

            //entity.HasOne(d => d.EmpleadoIdDptoNavigation).WithMany(p => p.Empleados)
            //    .HasForeignKey(d => d.EmpleadoIdDpto)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_empleado_Departamento");

            //entity.HasOne(d => d.EmpleadoTipo).WithMany(p => p.Empleados)
            //    .HasForeignKey(d => d.EmpleadoTipoId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_empleado_tipo_identificacion");
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

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.ItmId);

            entity.ToTable("item");

            entity.Property(e => e.ItmId)
                .ValueGeneratedNever()
                .HasColumnName("itm_id");
            entity.Property(e => e.ItmCantidad).HasColumnName("itm_cantidad");
            entity.Property(e => e.ItmDescripcion)
                .HasMaxLength(500)
                .HasColumnName("itm_descripcion");
            entity.Property(e => e.ItmValorUnitario).HasColumnName("itm_valor_unitario");
        });

        modelBuilder.Entity<NivelesRuteo>(entity =>
        {
            entity.HasKey(e => e.CodRuteo);
            

            entity.Property(e => e.CodRuteo).HasColumnName("cod_ruteo");
            entity.Property(e => e.DescRuteo)
                .HasMaxLength(50)
                .HasColumnName("desc_ruteo");
            entity.Property(e => e.EstadoRuteo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("estado_ruteo");
            entity.Property(e => e.Nivel).HasColumnName("nivel");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.ProvId);

            entity.ToTable("proveedor");

            entity.Property(e => e.ProvId)
                .ValueGeneratedNever()
                .HasColumnName("prov_id");
            entity.Property(e => e.ProvCiudad)
                .HasMaxLength(100)
                .HasColumnName("prov_ciudad");
            entity.Property(e => e.ProvCorreo)
                .HasMaxLength(100)
                .HasColumnName("prov_correo");
            entity.Property(e => e.ProvEmail)
                .HasMaxLength(50)
                .HasColumnName("prov_email");
            entity.Property(e => e.ProvNombre)
                .HasMaxLength(100)
                .HasColumnName("prov_nombre");
            entity.Property(e => e.ProvPais)
                .HasMaxLength(100)
                .HasColumnName("prov_pais");
            entity.Property(e => e.ProvProvincia)
                .HasMaxLength(100)
                .HasColumnName("prov_provincia");
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
            entity.Property(e => e.RoAplicacion).HasColumnName("ro_aplicacion");
            entity.Property(e => e.RoEmpresa).HasColumnName("ro_empresa");
            entity.Property(e => e.RoEstado)
                .HasMaxLength(1)
                .HasColumnName("ro_estado");
            entity.Property(e => e.RoNombre)
                .HasMaxLength(40)
                .HasColumnName("ro_nombre");
        });

        modelBuilder.Entity<Ruteo>(entity =>
        {
            entity.HasKey(e => e.RutCodigo).HasName("PK_tb_rut");

            entity.ToTable("ruteo");

            entity.Property(e => e.RutCodigo).HasColumnName("rut_cod");
            entity.Property(e => e.RutArea).HasColumnName("rut_area");
            entity.Property(e => e.RutEstado)
                .HasMaxLength(1)
                .HasColumnName("rut_estado");
            entity.Property(e => e.RutNombre)
                .HasMaxLength(40)
                .HasColumnName("rut_nombre");
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

        modelBuilder.Entity<RuteoArea>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ruteo_area");

            entity.Property(e => e.CodDept).HasColumnName("cod_dept");
            entity.Property(e => e.CodRuteo).HasColumnName("cod_ruteo");
            entity.Property(e => e.CodTipoSolicitud).HasColumnName("cod_tipo_solicitud");
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("estado");
        });

        modelBuilder.Entity<Sector>(entity =>
        {
            entity.HasKey(e => e.SectId);

            entity.ToTable("sector");

            entity.Property(e => e.SectId).HasColumnName("sect_id");
            entity.Property(e => e.SectDescripcion)
                .HasMaxLength(500)
                .HasColumnName("sect_descripcion");
            entity.Property(e => e.SectNombre)
                .HasMaxLength(100)
                .HasColumnName("sect_nombre");
        });

        modelBuilder.Entity<Sectore>(entity =>
        {
            entity.HasKey(e => e.SecId);

            entity.ToTable("sectores");

            entity.Property(e => e.SecId)
                .ValueGeneratedNever()
                .HasColumnName("sec_id");
            entity.Property(e => e.SecDescripcion)
                .HasMaxLength(50)
                .HasColumnName("sec_descripcion");
            entity.Property(e => e.SecEstado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("sec_estado");
        });

        modelBuilder.Entity<SolCotizacion>(entity =>
        {
            entity.HasKey(e => e.SolCotIdSolicitud);

            entity.ToTable("sol_cotizacion");

            entity.Property(e => e.SolCotIdSolicitud)
                .ValueGeneratedNever()
                .HasColumnName("sol_cot_id_solicitud");
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
            entity.Property(e => e.AudVeces).HasColumnName("aud_veces");
            entity.Property(e => e.SolCotAdjCotizacion)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("sol_cot_adj_cotizacion");
            entity.Property(e => e.SolCotCantidad).HasColumnName("sol_cot_cantidad");
            entity.Property(e => e.SolCotDescripcion)
                .HasMaxLength(500)
                .HasColumnName("sol_cot_descripcion");
            entity.Property(e => e.SolCotEstado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sol_cot_estado");
            entity.Property(e => e.SolCotIdCabecera).HasColumnName("sol_cot_id_cabecera");
            entity.Property(e => e.SolCotItem).HasColumnName("sol_cot_item");
            entity.Property(e => e.SolCotObservaciones)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("sol_cot_observaciones");
            entity.Property(e => e.SolCotPresupuesto)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("sol_cot_presupuesto");
            entity.Property(e => e.SolCotProcedimiento)
                .HasMaxLength(500)
                .HasColumnName("sol_cot_procedimiento");
            entity.Property(e => e.SolCotUnidad)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("sol_cot_unidad");

            entity.HasOne(d => d.SolCotIdCabeceraNavigation).WithMany(p => p.SolCotizacions)
                .HasForeignKey(d => d.SolCotIdCabecera)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_sol_cotizacion_cab_sol_cotizacion");

            entity.HasOne(d => d.SolCotItemNavigation).WithMany(p => p.SolCotizacions)
                .HasForeignKey(d => d.SolCotItem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_sol_cotizacion_item");
        });

        modelBuilder.Entity<SolOrdenCompra>(entity =>
        {
            entity.HasKey(e => e.SolOrdIdSolicitud);

            entity.ToTable("sol_orden_compra");

            entity.Property(e => e.SolOrdIdSolicitud)
                .ValueGeneratedNever()
                .HasColumnName("sol_ord_id_solicitud");
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
            entity.Property(e => e.AudVeces).HasColumnName("aud_veces");
            entity.Property(e => e.SolOrdAdjCotizacion)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("sol_ord_adj_cotizacion");
            entity.Property(e => e.SolOrdCantidad).HasColumnName("sol_ord_cantidad");
            entity.Property(e => e.SolOrdDescripcion)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("sol_ord_descripcion");
            entity.Property(e => e.SolOrdEstado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sol_ord_estado");
            entity.Property(e => e.SolOrdIdCabecera).HasColumnName("sol_ord_id_cabecera");
            entity.Property(e => e.SolOrdInspector)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("sol_ord_inspector");
            entity.Property(e => e.SolOrdItem).HasColumnName("sol_ord_item");
            entity.Property(e => e.SolOrdPresupuesto)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("sol_ord_presupuesto");
            entity.Property(e => e.SolOrdProcedimiento)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("sol_ord_procedimiento");
            entity.Property(e => e.SolOrdTelefono)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("sol_ord_telefono");
            entity.Property(e => e.SolOrdUnidad)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("sol_ord_unidad");

            entity.HasOne(d => d.SolOrdIdCabeceraNavigation).WithMany(p => p.SolOrdenCompras)
                .HasForeignKey(d => d.SolOrdIdCabecera)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_sol_orden_compra_cab_sol_orden_compra");

            entity.HasOne(d => d.SolOrdItemNavigation).WithMany(p => p.SolOrdenCompras)
                .HasForeignKey(d => d.SolOrdItem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_sol_orden_compra_item");
        });

        modelBuilder.Entity<SolPago>(entity =>
        {
            entity.HasKey(e => e.SolPagoIdSolicitud);

            entity.ToTable("sol_pago");

            entity.Property(e => e.SolPagoIdSolicitud)
                .ValueGeneratedNever()
                .HasColumnName("sol_pago_id_solicitud");
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
            entity.Property(e => e.AudVeces).HasColumnName("aud_veces");
            entity.Property(e => e.SolPagoAbono)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("sol_pago_abono");
            entity.Property(e => e.SolPagoAplMulta)
                .HasMaxLength(500)
                .HasColumnName("sol_pago_apl_multa");
            entity.Property(e => e.SolPagoCantContratada).HasColumnName("sol_pago_cant_contratada");
            entity.Property(e => e.SolPagoCantRecibida).HasColumnName("sol_pago_cant_recibida");
            entity.Property(e => e.SolPagoCerrarOrden)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("sol_pago_cerrar_orden");
            entity.Property(e => e.SolPagoEstado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sol_pago_estado");
            entity.Property(e => e.SolPagoIdCabecera).HasColumnName("sol_pago_id_cabecera");
            entity.Property(e => e.SolPagoItem)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("sol_pago_item");
            entity.Property(e => e.SolPagoObservaciones)
                .HasMaxLength(500)
                .HasColumnName("sol_pago_observaciones");
            entity.Property(e => e.SolPagoPagoTotal).HasColumnName("sol_pago_pago_total");
            entity.Property(e => e.SolPagoReceptor)
                .HasMaxLength(100)
                .HasColumnName("sol_pago_receptor");
            entity.Property(e => e.SolPagoSubtotal).HasColumnName("sol_pago_subtotal");
            entity.Property(e => e.SolPagoTotal).HasColumnName("sol_pago_total");
            entity.Property(e => e.SolPagoValDescontar).HasColumnName("sol_pago_val_descontar");
            entity.Property(e => e.SolPagoValUnitario).HasColumnName("sol_pago_val_unitario");

            entity.HasOne(d => d.SolPagoIdCabeceraNavigation).WithMany(p => p.SolPagos)
                .HasForeignKey(d => d.SolPagoIdCabecera)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_sol_pago_cab_sol_pago");
        });

        modelBuilder.Entity<TipoIdentificacion>(entity =>
        {
            entity.HasKey(e => e.TipoDocId);

            entity.ToTable("tipo_identificacion");

            entity.Property(e => e.TipoDocId).HasColumnName("tipo_doc_id");
            entity.Property(e => e.TipoDocInicial)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("tipo_doc_inicial");
            entity.Property(e => e.TipoDocNombre)
                .HasMaxLength(50)
                .HasColumnName("tipo_doc_nombre");
        });

        modelBuilder.Entity<TipoSolicitud>(entity =>
        {
            entity.HasKey(e => e.TipoSolId);

            entity.ToTable("tipo_solic");

            entity.Property(e => e.TipoSolId).HasColumnName("tipo_sol_id");
            entity.Property(e => e.TipoSolInicial)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("tipo_sol_inicial");
            entity.Property(e => e.TipoSolNombre)
                .HasMaxLength(50)
                .HasColumnName("tipo_sol_nombre");
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

            entity.ToTable("usuario");

            entity.Property(e => e.UsId).HasColumnName("us_id");
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
            entity.Property(e => e.AudVeces).HasColumnName("aud_veces");
            entity.Property(e => e.UsBanUserData)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("us_ban_user_data");
            entity.Property(e => e.UsContrasenia).HasColumnName("us_contrasenia");
            entity.Property(e => e.UsEmpresa).HasColumnName("us_empresa");
            entity.Property(e => e.UsEstado)
                .HasMaxLength(1)
                .HasColumnName("us_estado");
            entity.Property(e => e.UsFechaCaduca)
                .HasColumnType("datetime")
                .HasColumnName("us_fecha_caduca");
            entity.Property(e => e.UsFechaInicio)
                .HasColumnType("datetime")
                .HasColumnName("us_fecha_inicio");
            entity.Property(e => e.UsLogin)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("us_login");
            entity.Property(e => e.UsNombre)
                .HasMaxLength(100)
                .HasColumnName("us_nombre");
            entity.Property(e => e.UsServicioC)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("us_ServicioC");
            entity.Property(e => e.UsTipoAcceso).HasColumnName("us_tipo_acceso");
            entity.Property(e => e.UsUserData)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("us_user_data");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public DbSet<SOLFM2K.Models.Area>? Area { get; set; }
}
