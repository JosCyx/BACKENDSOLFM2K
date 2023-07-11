using System;
using System.Collections.Generic;

namespace SOLFM2K.Models;

public partial class RolUsuario
{
    public byte RuEmpresa { get; set; }

    public short RuRol { get; set; }

    public string RuLogin { get; set; } = null!;

    public string? RuEstado { get; set; }
}
