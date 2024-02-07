using System;
using System.Collections.Generic;

namespace Infrastructure.ProductEntities;

public partial class Manufacturer
{
    public int Id { get; set; }

    public string? Manufacturer1 { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
