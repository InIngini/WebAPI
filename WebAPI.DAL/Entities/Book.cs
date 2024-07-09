using System;
using System.Collections.Generic;

namespace WebAPI.DAL.Entities;

public partial class Book
{
    public int IdBook { get; set; }

    public string NameBook { get; set; } = null!;

    public int? IdPicture { get; set; }

    public virtual ICollection<BelongToBook> BelongToBooks { get; set; } = new List<BelongToBook>();

    public virtual ICollection<Character> Characters { get; set; } = new List<Character>();

    public virtual Picture? IdPictureNavigation { get; set; }

    public virtual ICollection<Scheme> Schemes { get; set; } = new List<Scheme>();

    public virtual ICollection<Timeline> Timelines { get; set; } = new List<Timeline>();
}
