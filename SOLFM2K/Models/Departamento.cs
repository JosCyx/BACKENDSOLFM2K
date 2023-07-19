using System;
using System.Collections.Generic;

namespace SOLFM2K.Models;

public partial class Departamento
{
    public int DepId { get; set; }

    public int DepIdNomina { get; set; }

    public string DepDescp { get; set; } = null!;

    public string DepEstado { get; set; } = null!;

    public int? DepArea { get; set; }

    //public virtual Area? DepAreaNavigation { get; set; }
}
