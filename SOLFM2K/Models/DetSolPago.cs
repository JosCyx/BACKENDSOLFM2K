using System;
using System.Collections.Generic;

namespace SOLFM2K.Models;

public partial class DetSolPago
{
    public int DetPagoID { get; set; }
    public int DetPagoTipoSol { get; set; }
    public int DetPagoNoSol { get; set; }
    public int DetPagoIdDetalle { get; set; }
    public string DetPagoItemDesc { get; set; }
    public int DetPagoCantContratada { get; set; }
    public int DetPagoCantRecibida { get; set; }
    public double DetPagoValUnitario { get; set; }
    public double DetPagoSubtotal { get; set; }
}
