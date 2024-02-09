using System;
using System.Collections.Generic;

namespace Infrastructure.ProductEntities;

public partial class Image
{
    public int Id { get; set; }

    public string ImageUrl { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
