using System;
using System.Collections.Generic;

namespace Infrastructure.ProductEntities;

public partial class Price
{
    public int Id { get; set; }

    public decimal? Price1 { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
