using Microsoft.AspNetCore.Routing.Constraints;
using System;
using System.Collections.Generic;

namespace SOLFM2K.Models;

public partial class CabSolPago
{
    public int CabPagoID { get; set; }

    public string CabPagoNumerico { get; set; }
    public int CabPagoTipoSolicitud { get; set; }
    public int CabPagoNoSolicitud { get; set; }
    public int CabPagoIdDeptSolicitante { get; set; }
    public int CabPagoIdAreaSolicitante { get; set; }
    public string CabPagoSolicitante { get; set; }
    public string CabPagoNoOrdenCompra { get; set; }
    public DateTime CabPagoFechaEmision { get; set; }
    public DateTime CabPagoFechaEnvio { get; set; }
    public string CabPagoNumFactura { get; set; }
    public DateTime CabPagoFechaFactura { get; set; }
    public string CabPagoProveedor { get; set; }
    public string CabPagoRucProveedor { get; set; }
    public double Cabpagototal { get; set; }
    public string CabPagoObservaciones { get; set; } = null!;
    public string CabPagoAplicarMulta { get; set; } = null!;
    public double CabPagoValorMulta { get; set; }
    public double CabPagoValorTotalAut { get; set; }
    public string CabPagoReceptor { get; set; }
    public DateTime CabPagoFechaInspeccion { get; set; }
    public string CabPagoCancelacionOrden { get; set; } = null!;
    public string CabPagoEstado { get; set; }
    public int CabPagoEstadoTrack { get; set; }
    public string CabPagoIdEmisor { get; set; }
    public string CabPagoApprovedBy { get; set; }

}

