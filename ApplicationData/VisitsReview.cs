using System;
using System.Collections.Generic;

namespace Frontend_MyWay.ApplicationData;

public partial class VisitsReview
{
    public int VisitReviewId { get; set; }

    public int VisitId { get; set; }

    public int ReviewId { get; set; }

    public virtual Review Review { get; set; } = null!;

    public virtual Visit Visit { get; set; } = null!;
}
