using System;
using System.Collections.Generic;

namespace SOLFM2K.Models;

public partial class JwtConfig
{
    public string JwtIssuer { get; set; } = null!;

    public string JwtAudence { get; set; } = null!;

    public string JwtSecretKey { get; set; } = null!;
}
