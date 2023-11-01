using System;
using System.Collections.Generic;

namespace SOLFM2K.Models;

public partial class DestinoSolPago
{
    public int DestPagId { get; set; }

    public int DestPagTipoSol { get; set; }

    public int DestPagNoSol { get; set; }

    public int DestPagIdDetalle { get; set; }

    public string DestPagEmpleado { get; set; }

    public int DestPagSector { get; set; }

    public string DestPagObervacion { get; set; } = null!;

    public string DestPagEvidencia { get; set; } = null!;
}
