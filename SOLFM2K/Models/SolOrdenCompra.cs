using System;
using System.Collections.Generic;

namespace SOLFM2K.Models;

public partial class SolOrdenCompra
{
    public int SolOrdIdSolicitud { get; set; }

    public int SolOrdIdCabecera { get; set; }

    public int SolOrdItem { get; set; }

    public string SolOrdDescripcion { get; set; } = null!;

    public string SolOrdPresupuesto { get; set; } = null!;

    public string SolOrdUnidad { get; set; } = null!;

    public int SolOrdCantidad { get; set; }

    public string SolOrdProcedimiento { get; set; } = null!;

    public string SolOrdAdjCotizacion { get; set; } = null!;

    public string SolOrdTelefono { get; set; } = null!;

    public string SolOrdInspector { get; set; } = null!;

    public string SolOrdEstado { get; set; } = null!;

    public string AudEvento { get; set; } = null!;

    public DateTime AudFecha { get; set; }

    public string AudUsuario { get; set; } = null!;

    public string AudObservacion { get; set; } = null!;

    public int AudVeces { get; set; }

    //public virtual CabSolOrdenCompra SolOrdIdCabeceraNavigation { get; set; } = null!;
}
