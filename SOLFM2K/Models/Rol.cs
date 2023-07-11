using System;
using System.Collections.Generic;

namespace SOLFM2K.Models;

public partial class Rol
{
    public byte RoEmpresa { get; set; }

    public short RoCodigo { get; set; }

    public string? RoNombre { get; set; }

    public string? RoEstado { get; set; }

    public short? RoAplicacion { get; set; }
}
