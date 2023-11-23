using System;
using System.Collections.Generic;

namespace SOLFM2K.Models;

public partial class CabSolOrdenCompra
{
    public int cabSolOCID { get; set; }

    public string? cabSolOCNumerico { get; set; }

    public int cabSolOCTipoSolicitud { get; set; }

    public int cabSolOCIdDept { get; set; }

    public int cabSolOCIdArea { get; set; } 

    public int cabSolOCNoSolicitud { get; set; }

    public string cabSolOCSolicitante { get; set; }

    public DateTime cabSolOCFecha { get; set; }

    public string cabSolOCAsunto { get; set; } = null!;

    public string? cabSolOCProcedimiento { get; set; } = null!;

    public string? cabSolOCObervaciones { get; set; } = null!;

    public string? cabSolOCAdjCot { get; set; } = null!;

    public int? cabSolOCNumCotizacion { get; set; }

    public string cabSolOCEstado { get; set; } = null!;

    public int cabSolOCEstadoTracking { get; set; }


    public DateTime cabSolOCPlazoEntrega { get; set; }

    public DateTime cabSolOCFechaMaxentrega { get; set; }

    public string? cabSolOCInspector { get; set; }

    public string? cabSolOCTelefInspector { get; set; } = null!;

    public string? cabSolOCProveedor { get; set; } = null!;

    public string? cabSolOCRUCProveedor { get; set; } = null!;

    public string cabSolOCIdEmisor { get; set; }

    public string cabSolOCApprovedBy { get; set; }

    public string cabSolOCFinancieroBy { get; set; }

    public string cabSolOCAprobPresup { get; set; }

    public string? cabSolOCMotivoDev { get; set; } = null!;

    public double cabSolOCValorAprobacion { get; set; }

    //public virtual TipoSolic CabOrdcIdCabeceraNavigation { get; set; } = null!;

    //public virtual Proveedor CabOrdcProveedorNavigation { get; set; } = null!;

    //public virtual ICollection<SolOrdenCompra> SolOrdenCompras { get; set; } = new List<SolOrdenCompra>();
}
