using System;
using System.Collections.Generic;

namespace SOLFM2K.Models;

public partial class ParamsConf
{
    public int Id { get; set; }

    public string Identify { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string Pass { get; set; } = null!;

    public string Status { get; set; } = null!;
}
