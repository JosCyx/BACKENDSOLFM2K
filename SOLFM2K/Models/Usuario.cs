using System;
using System.Collections.Generic;

namespace SOLFM2K.Models;

public partial class Usuario
{
    public byte UsEmpresa { get; set; }

    public int UsId { get; set; }

    public string UsLogin { get; set; } = null!;

    public string UsContrasenia { get; set; } = null!;

    public string UsIdNomina { get; set; } 

    public string UsNombre { get; set; } = null!;

    public string UsEstado { get; set; } = null!;

    public DateTime UsFechaInicio { get; set; }

    public DateTime UsFechaCaduca { get; set; }

    public string? UsServicioC { get; set; }

    public string? UsUserData { get; set; }

    public string? UsBanUserData { get; set; }

    public int? UsTipoAcceso { get; set; }

    public string? AudEvento { get; set; }

    public DateTime? AudFecha { get; set; }

    public string? AudUsuario { get; set; }

    public string? AudObservacion { get; set; }

    public int? AudVeces { get; set; }
}
