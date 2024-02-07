

using Infrastructure.ProductContext;
using Infrastructure.ProductEntities;

namespace Infrastructure.Repositories;

public class ImageRepository : ProductsBaseRepo<Image, ProductDbContext>
{
    private readonly ProductDbContext _context;

    public ImageRepository(ProductDbContext context) : base(context)
    {
        _context = context;
    }
}
