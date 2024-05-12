using System;
using System.Collections.Generic;

namespace Frontend_MyWay.ApplicationData;

public partial class Visit
{
    public int VisitId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string DescriptionShort { get; set; } = null!;

    public string CoverImage { get; set; } = null!;

    public decimal Cost { get; set; }

    public decimal Rating { get; set; }

    public int LevelId { get; set; }

    public int CountryId { get; set; }

    public int CategoryId { get; set; }

    public decimal DistanceKm { get; set; }

    public TimeSpan? TimeHours { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    public virtual Level Level { get; set; } = null!;

    public virtual ICollection<VisitsReview> VisitsReviews { get; set; } = new List<VisitsReview>();

    public virtual ICollection<VisitsRoute> VisitsRoutes { get; set; } = new List<VisitsRoute>();
}
