using System;
using System.Collections.Generic;

namespace SOLFM2K.Models;

public partial class Empleado
{
    public int EmpleadoId { get; set; }

    public int EmpleadoCompania { get; set; }

    public int EmpleadoIdNomina { get; set; }

    public int EmpleadoIdDpto { get; set; }

    public int EmpleadoIdArea { get; set; }

    public string EmpleadoTipoId { get; set; } = null!;

    public string EmpleadoIdentificacion { get; set; } = null!;

    public string EmpleadoNombres { get; set; } = null!;

    public string EmpleadoApellidos { get; set; } = null!;

    public string EmpleadoSexo { get; set; } = null!;

    public string? EmpleadoTelefono { get; set; }

    public string? EmpleadoCorreo { get; set; }

    public string EmpleadoEstado { get; set; } = null!;
}
