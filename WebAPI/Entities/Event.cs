using System;
using System.Collections.Generic;

namespace WebAPI;

public partial class Event
{
    public int IdEvent { get; set; }

    public string Name { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string Time { get; set; } = null!;

    public virtual ICollection<Character> IdCharacters { get; set; } = new List<Character>();

    public virtual ICollection<Timeline> IdTimelines { get; set; } = new List<Timeline>();
}
