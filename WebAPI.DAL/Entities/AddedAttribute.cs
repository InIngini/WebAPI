using System;
using System.Collections.Generic;

namespace WebAPI.DAL.Entities;

public partial class AddedAttribute
{
    public int IdAttribute { get; set; }

    public int NumberBlock { get; set; }

    public string NameAttribute { get; set; } = null!;

    public string ContentAttribute { get; set; } = null!;

    public int IdCharacter { get; set; }

    public virtual Character IdCharacterNavigation { get; set; } = null!;
}
