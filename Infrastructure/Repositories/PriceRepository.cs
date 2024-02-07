using Infrastructure.ProductContext;
using Infrastructure.ProductEntities;


namespace Infrastructure.Repositories;

public class PriceRepository : ProductsBaseRepo<Price, ProductDbContext>
{
    private readonly ProductDbContext _context;

    public PriceRepository(ProductDbContext context) : base(context)
    {
        _context = context;
    }
}
