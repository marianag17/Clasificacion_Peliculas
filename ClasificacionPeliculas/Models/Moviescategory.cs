using System;
using System.Collections.Generic;

namespace ClasificacionPeliculas.Models;

public partial class Moviescategory
{
    public int Movie_Id { get; set; }

    public int Category_Id { get; set; }

    public int? Id { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Movie Movie { get; set; } = null!;
}
