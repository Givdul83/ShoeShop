using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ProductEntities;

public class ProductImage
{
    public int ProductId { get; set; }

    public virtual Product Product { get; set; } = null!;
    public int ImageId { get; set; }

    public virtual Image Image { get; set; } = null!;
}
