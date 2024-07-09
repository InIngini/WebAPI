using System;
using System.Collections.Generic;

namespace WebAPI.DAL.Entities;

public partial class Connection
{
    public int IdConnection { get; set; }

    public int TypeConnection { get; set; }

    public int IdCharacter1 { get; set; }

    public int IdCharacter2 { get; set; }

    public virtual Character IdCharacter1Navigation { get; set; } = null!;

    public virtual Character IdCharacter2Navigation { get; set; } = null!;

    public virtual ICollection<Scheme> IdSchemes { get; set; } = new List<Scheme>();
}
