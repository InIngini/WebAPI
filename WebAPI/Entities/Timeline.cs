using System;
using System.Collections.Generic;

namespace WebAPI;

public partial class Timeline
{
    public int IdTimeline { get; set; }

    public string NameTimeline { get; set; } = null!;

    public int IdBook { get; set; }

    public virtual Book IdBookNavigation { get; set; } = null!;

    public virtual ICollection<Event> IdEvents { get; set; } = new List<Event>();
}
