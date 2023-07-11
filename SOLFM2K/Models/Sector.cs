﻿using System;
using System.Collections.Generic;

namespace SOLFM2K.Models;

public partial class Sector
{
    public int SectId { get; set; }

    public string SectNombre { get; set; } = null!;

    public string SectDescripcion { get; set; } = null!;
}
