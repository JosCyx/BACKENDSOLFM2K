using System;
using System.Collections.Generic;

namespace SOLFM2K.Models;

public partial class DetalleFacturaPago
{
    public int DetFactId { get; set; }

    public int DetFactIdFactura { get; set; }

    public string DetFactNumOrdenCompra { get; set; } = null!;

    public string DetFactIdProducto { get; set; } = null!;

    public string DetFactDescpProducto { get; set; } = null!;

    public int DetFactCantProducto { get; set; }

    public double DetFactValorUnit { get; set; }

    public double DetFactDescuento { get; set; }

    public double DetFactTotal { get; set; }

}
