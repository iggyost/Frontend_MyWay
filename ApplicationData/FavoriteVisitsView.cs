using System;
using System.Collections.Generic;

namespace Frontend_MyWay.ApplicationData;

public partial class FavoriteVisitsView
{
    public int FavoriteId { get; set; }

    public int UserId { get; set; }

    public int VisitId { get; set; }

    public string Name { get; set; } = null!;

    public string DescriptionShort { get; set; } = null!;

    public string CoverImage { get; set; } = null!;

    public decimal Cost { get; set; }

    public decimal Rating { get; set; }
}
