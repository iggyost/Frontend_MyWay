using System;
using System.Collections.Generic;

namespace Frontend_MyWay.ApplicationData;

public partial class VisitsView
{
    public int VisitId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string CoverImage { get; set; } = null!;

    public decimal Rating { get; set; }

    public string LevelName { get; set; } = null!;

    public decimal DistanceKm { get; set; }
    public TimeSpan? TimeHours { get; set; }
}
