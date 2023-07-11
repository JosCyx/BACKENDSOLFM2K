using System;
using System.Collections.Generic;

namespace SOLFM2K.Models;

public partial class Usuario
{
    public byte UsEmpresa { get; set; }

    public int UsId { get; set; }

    public string UsLogin { get; set; } = null!;

    public byte[] UsContrasenia { get; set; } = null!;

    public string UsNombre { get; set; } = null!;

    public string UsEstado { get; set; } = null!;

    public DateTime UsFechaInicio { get; set; }

    public DateTime UsFechaCaduca { get; set; }

    public string UsServicioC { get; set; } = null!;

    public string UsUserData { get; set; } = null!;

    public string UsBanUserData { get; set; } = null!;

    public int UsTipoAcceso { get; set; }

    public string AudEvento { get; set; } = null!;

    public DateTime AudFecha { get; set; }

    public string AudUsuario { get; set; } = null!;

    public string AudObservacion { get; set; } = null!;

    public int AudVeces { get; set; }
}
