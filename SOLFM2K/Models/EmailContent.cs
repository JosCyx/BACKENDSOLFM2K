using System;
using System.Collections.Generic;

namespace SOLFM2K.Pruebas;

public partial class EmailContent
{
    public int EmailContId { get; set; }

    public int EmailContTipoAccion { get; set; }

    public string EmailContAsunto { get; set; } = null!;

    public string EmailContContenido { get; set; } = null!;
}
