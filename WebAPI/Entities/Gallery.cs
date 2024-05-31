using System;
using System.Collections.Generic;

namespace WebAPI;

public partial class Gallery
{
    public int IdPicture { get; set; }

    public int IdCharacter { get; set; }

    public virtual Character IdCharacterNavigation { get; set; } = null!;

    public virtual Picture IdPictureNavigation { get; set; } = null!;
}
