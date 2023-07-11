using System;
using System.Collections.Generic;

namespace SOLFM2K.Models;

public partial class CabSolCotizacion
{
    public int CabSolCotIdCabecera { get; set; }

    public DateTime CabSolCotFecha { get; set; }

    public int CabSolCotSolicitante { get; set; }

    public string CabSolCotAreaSolicitante { get; set; } = null!;

    public string CabSolCotAsunto { get; set; } = null!;

    public virtual Empleado CabSolCotSolicitanteNavigation { get; set; } = null!;

    public virtual ICollection<SolCotizacion> SolCotizacions { get; set; } = new List<SolCotizacion>();
}
