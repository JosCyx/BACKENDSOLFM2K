using System;
using System.Collections.Generic;

namespace SOLFM2K.Models;

public partial class SolPago
{
    public int SolPagoIdSolicitud { get; set; }

    public int SolPagoIdCabecera { get; set; }

    public string SolPagoItem { get; set; } = null!;

    public int SolPagoCantContratada { get; set; }

    public int SolPagoCantRecibida { get; set; }

    public int SolPagoValUnitario { get; set; }

    public int SolPagoSubtotal { get; set; }

    public int SolPagoTotal { get; set; }

    public string SolPagoObservaciones { get; set; } = null!;

    public string SolPagoAplMulta { get; set; } = null!;

    public int SolPagoValDescontar { get; set; }

    public int SolPagoPagoTotal { get; set; }

    public string SolPagoReceptor { get; set; } = null!;

    public string SolPagoAbono { get; set; } = null!;

    public string SolPagoCerrarOrden { get; set; } = null!;

    public string SolPagoEstado { get; set; } = null!;

    public string AudEvento { get; set; } = null!;

    public DateTime AudFecha { get; set; }

    public string AudUsuario { get; set; } = null!;

    public string AudObservacion { get; set; } = null!;

    public int AudVeces { get; set; }

    public virtual CabSolPago SolPagoIdCabeceraNavigation { get; set; } = null!;
}
