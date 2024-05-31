using System;
using System.Collections.Generic;

namespace WebAPI;

public partial class Character
{
    public int IdCharacter { get; set; }

    public int IdBook { get; set; }

    public int? IdPicture { get; set; }

    public virtual ICollection<AddedAttribute> AddedAttributes { get; set; } = new List<AddedAttribute>();

    public virtual Block1? Block1 { get; set; }

    public virtual Block2? Block2 { get; set; }

    public virtual Block3? Block3 { get; set; }

    public virtual Block4? Block4 { get; set; }

    public virtual ICollection<Connection> ConnectionIdCharacter1Navigations { get; set; } = new List<Connection>();

    public virtual ICollection<Connection> ConnectionIdCharacter2Navigations { get; set; } = new List<Connection>();

    public virtual ICollection<Gallery> Galleries { get; set; } = new List<Gallery>();

    public virtual Book IdBookNavigation { get; set; } = null!;

    public virtual Picture? IdPictureNavigation { get; set; }

    public virtual ICollection<Event> IdEvents { get; set; } = new List<Event>();
}
