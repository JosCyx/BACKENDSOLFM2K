using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace SOLFM2K.Models;

public partial class Area
{
    public int AreaId { get; set; }
    
    public int AreaIdNomina { get; set; }

    public string AreaDecp { get; set; } = null!;

    public string AreaEstado { get; set; } = null!;

    public string AreaNemonico { get; set; } = null!;

    //public virtual ICollection<Departamento> Departamentos { get; set; } = new List<Departamento>();
}
