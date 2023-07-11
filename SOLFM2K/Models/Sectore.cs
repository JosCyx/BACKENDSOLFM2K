using System;
using System.Collections.Generic;

namespace SOLFM2K.Models;

public partial class Sectore
{
    public int SecId { get; set; }

    public string SecDescripcion { get; set; } = null!;

    public string SecEstado { get; set; } = null!;
}
