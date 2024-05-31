using System;
using System.Collections.Generic;

namespace WebAPI;

public partial class User
{
    public int IdUser { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<BelongToBook> BelongToBooks { get; set; } = new List<BelongToBook>();
}
