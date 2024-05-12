using System;
using System.Collections.Generic;

namespace Frontend_MyWay.ApplicationData;

public partial class Review
{
    public int ReviewId { get; set; }

    public int UserId { get; set; }

    public string? Text { get; set; }

    public int Mark { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual ICollection<VisitsReview> VisitsReviews { get; set; } = new List<VisitsReview>();
}
