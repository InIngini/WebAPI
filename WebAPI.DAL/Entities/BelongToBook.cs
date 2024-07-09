using System;
using System.Collections.Generic;

namespace WebAPI.DAL.Entities;

public partial class BelongToBook
{
    public int IdUser { get; set; }

    public int IdBook { get; set; }

    public int TypeBelong { get; set; }

    public virtual Book IdBookNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
