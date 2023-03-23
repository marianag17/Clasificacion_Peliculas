using System;
using System.Collections.Generic;

namespace ClasificacionPeliculas.Models;

public partial class Vote
{
    public int Id { get; set; }

    public int Pi_Id { get; set; }

    public int Movies_Id { get; set; }

    public DateTime RowCreationTime { get; set; }

    public virtual Movie Movies { get; set; } = null!;

    public virtual personal_information Pi { get; set; } = null!;
}
