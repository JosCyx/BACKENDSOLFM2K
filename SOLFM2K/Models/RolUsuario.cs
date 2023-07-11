using System;
using System.Collections.Generic;

namespace SOLFM2K.Models;

public partial class RolUsuario
{
    public int RuId { get; set; }

    public int RuEmpresa { get; set; }

    public int RuRol { get; set; }

    public string RuLogin { get; set; } = null!;

    public string? RuEstado { get; set; }
}
