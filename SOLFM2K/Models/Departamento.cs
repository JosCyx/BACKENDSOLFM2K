using System;
using System.Collections.Generic;

namespace SOLFM2K.Models;

public partial class Departamento
{
    public int CodDep { get; set; }

    public string DescDep { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
