using System;
using System.Collections.Generic;

namespace SOLFM2K.Models;

public partial class CabSolOrdenCompra
{
    public int CabOrdcIdCabecera { get; set; }

    public DateTime CabOrdcFecha { get; set; }

    public int CabOrdcProveedor { get; set; }

    public string CabOrdcRuc { get; set; } = null!;

    public int CabOrdcSolicitante { get; set; }

    public string CabOrdcAreaSolicitante { get; set; } = null!;

    public string CabOrdcAsunto { get; set; } = null!;

    public virtual TipoSolic CabOrdcIdCabeceraNavigation { get; set; } = null!;

    public virtual Proveedor CabOrdcProveedorNavigation { get; set; } = null!;

    public virtual ICollection<SolOrdenCompra> SolOrdenCompras { get; set; } = new List<SolOrdenCompra>();
}
