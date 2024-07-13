using System;
using System.Collections.Generic;

namespace WebAPI.DAL.Entities;

public partial class Character
{
    public int IdCharacter { get; set; }

    public int IdBook { get; set; }

    public int? IdPicture { get; set; }

    public virtual ICollection<AddedAttribute> AddedAttributes { get; set; } = new List<AddedAttribute>();

    public virtual Answer? Answer { get; set; }

    public virtual ICollection<Connection> ConnectionIdCharacter1Navigations { get; set; } = new List<Connection>();

    public virtual ICollection<Connection> ConnectionIdCharacter2Navigations { get; set; } = new List<Connection>();

    public virtual ICollection<Gallery> Galleries { get; set; } = new List<Gallery>();

    public virtual Book IdBookNavigation { get; set; } = null!;

    public virtual Picture? IdPictureNavigation { get; set; }

    public virtual ICollection<Event> IdEvents { get; set; } = new List<Event>();
}
