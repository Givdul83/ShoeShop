using System;
using System.Collections.Generic;

namespace Infrastructure.ProductEntities;

public partial class Product
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int ManufacturerId { get; set; }

    public int PriceId { get; set; }

    public virtual Manufacturer Manufacturer { get; set; } = null!;

    public virtual Price Price { get; set; } = null!;

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();
}
