using System;
using System.Collections.Generic;

namespace SOLFM2K.Models;

public partial class Item
{
    public int ItmId { get; set; }

    public int ItmCantidad { get; set; }

    public string ItmDescripcion { get; set; } = null!;

    public int ItmValorUnitario { get; set; }

    public virtual ICollection<SolCotizacion> SolCotizacions { get; set; } = new List<SolCotizacion>();

    public virtual ICollection<SolOrdenCompra> SolOrdenCompras { get; set; } = new List<SolOrdenCompra>();
}
