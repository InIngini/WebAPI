using System;
using System.Collections.Generic;

namespace WebAPI.DAL.Entities;

public partial class Block1
{
    public int IdCharacter { get; set; }

    public string Name { get; set; } = null!;

    public string Question1 { get; set; } = null!;

    public string Question2 { get; set; } = null!;

    public string Question3 { get; set; } = null!;

    public string Question4 { get; set; } = null!;

    public string Question5 { get; set; } = null!;

    public string Question6 { get; set; } = null!;

    public virtual Character IdCharacterNavigation { get; set; } = null!;
}
