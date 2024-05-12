using System;
using System.Collections.Generic;

namespace Frontend_MyWay.ApplicationData;

public partial class VisitsRoute
{
    public int VisitRouteId { get; set; }

    public int VisitId { get; set; }

    public string RoutePath { get; set; } = null!;

    public virtual Visit Visit { get; set; } = null!;
}
