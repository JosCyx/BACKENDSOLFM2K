using System;
using System.Collections.Generic;

namespace SOLFM2K.Models;

public partial class EmplNivel
{
    public int EmpNivId { get; set; }

    public string EmpNivEmpelado { get; set; }

    public int EmpNivDeptAutorizado { get; set; }

    public int EmpNivRuteo { get; set; }

    public string EmpNivImp { get; set; }
}
