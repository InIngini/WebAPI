using System;
using System.Collections.Generic;

namespace WebAPI.DAL.Entities;

public partial class Scheme
{
    public int IdScheme { get; set; }

    public string NameScheme { get; set; } = null!;

    public int IdBook { get; set; }

    public virtual Book IdBookNavigation { get; set; } = null!;

    public virtual ICollection<Connection> IdConnections { get; set; } = new List<Connection>();
}
