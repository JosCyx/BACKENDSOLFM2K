using System;
using System.Collections.Generic;

namespace SOLFM2K.Models;

public partial class FacturaSolPago
{
    public int FactSpId { get; set; }

    public int FactSpTipoSol { get; set; }

    public int FactSpNoSol { get; set; }

    public int FactSpNoFactura { get; set; }

    public string FactSpNumFactura { get; set; } = null!;

    public DateTime FactSpFechaFactura { get; set; }

    public string FactSpRucProvFactura { get; set; } = null!;

    public string FactSpProvFactura { get; set; } = null!;

    public double FactSpMontoFactura { get; set; }

    public string FactSpNumOrdenCompra { get; set; } = null!;

    public int FactSpEstado { get; set; }

}
