using System;
using System.Collections.Generic;

namespace Frontend_MyWay.ApplicationData;

public partial class Level
{
    public int LevelId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Visit> Visits { get; set; } = new List<Visit>();
}
