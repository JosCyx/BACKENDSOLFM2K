using System;
using System.Collections.Generic;

namespace SOLFM2K.Models;

public partial class CabSolPago
{
    public int CabPagoIdCabecera { get; set; }

    public int CabPagoIdOrdenCompra { get; set; }

    public DateTime CabPagoFechaFactura { get; set; }

    public string CabPagoNumFactura { get; set; } = null!;

    public int CabPagoProveedor { get; set; }

    public DateTime CabPagoFechaSolPago { get; set; }

    public string CabPagoRuc { get; set; } = null!;

    public int CabPagoSolicitante { get; set; }

    //public virtual TipoSolic CabPagoIdCabeceraNavigation { get; set; } = null!;

    //public virtual Proveedor CabPagoProveedorNavigation { get; set; } = null!;

    //public virtual ICollection<SolPago> SolPagos { get; set; } = new List<SolPago>();
}
