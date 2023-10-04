using System;
using System.Collections.Generic;

namespace U2Actividad1.Models.Entities;

public partial class Clase
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Especy> Especies { get; set; } = new List<Especy>();
}
