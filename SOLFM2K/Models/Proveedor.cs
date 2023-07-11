using System;
using System.Collections.Generic;

namespace SOLFM2K.Models;

public partial class Proveedor
{
    public int ProvId { get; set; }

    public string ProvNombre { get; set; } = null!;

    public string ProvEmail { get; set; } = null!;

    public string ProvCiudad { get; set; } = null!;

    public string ProvProvincia { get; set; } = null!;

    public string ProvPais { get; set; } = null!;

    public string ProvCorreo { get; set; } = null!;

    public virtual ICollection<CabSolOrdenCompra> CabSolOrdenCompras { get; set; } = new List<CabSolOrdenCompra>();

    public virtual ICollection<CabSolPago> CabSolPagos { get; set; } = new List<CabSolPago>();
}
