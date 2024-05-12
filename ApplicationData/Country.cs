using System;
using System.Collections.Generic;

namespace Frontend_MyWay.ApplicationData;

public partial class Country
{
    public int CountryId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string CoverImage { get; set; } = null!;

    public virtual ICollection<Visit> Visits { get; set; } = new List<Visit>();
}
