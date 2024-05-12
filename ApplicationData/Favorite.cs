using System;
using System.Collections.Generic;

namespace Frontend_MyWay.ApplicationData;

public partial class Favorite
{
    public int FavoriteId { get; set; }

    public int UserId { get; set; }

    public int VisitId { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual Visit Visit { get; set; } = null!;
}
