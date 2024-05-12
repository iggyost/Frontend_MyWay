using System;
using System.Collections.Generic;

namespace Frontend_MyWay.ApplicationData;

public partial class Subcategory
{
    public int SubcategoryId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}
