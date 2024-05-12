using System;
using System.Collections.Generic;

namespace Frontend_MyWay.ApplicationData;

public partial class Category
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public string CoverImage { get; set; } = null!;

    public int SubcategoryId { get; set; }

    public virtual Subcategory Subcategory { get; set; } = null!;

    public virtual ICollection<Visit> Visits { get; set; } = new List<Visit>();
}
