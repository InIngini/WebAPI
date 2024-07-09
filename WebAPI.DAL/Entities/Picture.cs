using System;
using System.Collections.Generic;

namespace WebAPI.DAL.Entities;

public partial class Picture
{
    public int IdPicture { get; set; }

    public byte[] Picture1 { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();

    public virtual ICollection<Character> Characters { get; set; } = new List<Character>();

    public virtual Gallery? Gallery { get; set; }
}
