using System;
using System.Collections.Generic;

namespace SOLFM2K.Models;

public partial class Empleado
{
    public int EmpleadoId { get; set; }

    public int EmpleadoIdDpto { get; set; }

    public int EmpleadoCompania { get; set; }

    public int EmpleadoTipoId { get; set; }

    public string EmpleadoIdentificacion { get; set; } = null!;

    public string EmpleadoNombres { get; set; } = null!;

    public string EmpleadoApellidos { get; set; } = null!;

    public string EmpleadoSexo { get; set; } = null!;

    public string EmpleadoTelefono { get; set; } = null!;

    public string? EmpleadoCorreo { get; set; }

    public virtual ICollection<CabSolCotizacion> CabSolCotizacions { get; set; } = new List<CabSolCotizacion>();

    public virtual ICollection<CabSolOrdenCompra> CabSolOrdenCompras { get; set; } = new List<CabSolOrdenCompra>();

    public virtual ICollection<CabSolPago> CabSolPagos { get; set; } = new List<CabSolPago>();

    public virtual Departamento EmpleadoIdDptoNavigation { get; set; } = null!;

    public virtual TipoIdentificacion EmpleadoTipo { get; set; } = null!;
}
