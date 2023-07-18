using System;
using System.Collections.Generic;

namespace SOLFM2K.Models;

public partial class Departamento
{
    public int DepId { get; set; }

    public int DepIdNomina { get; set; }

    public string? DepDescp { get; set; } 

    public string? DepEstado { get; set; } 

    public int DepArea { get; set; }

    //public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
