using Infrastructure.ProductContext;
using Infrastructure.ProductEntities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;


    public class ProductRepository : ProductsBaseRepo<Product, ProductDbContext>
    {
        private readonly ProductDbContext _context;

        public ProductRepository(ProductDbContext context) : base(context)
        {
            _context = context;
        }

    public override async Task<IEnumerable<Product>> GetAllAsync()
    {
        try
        {
            var entities = await _context.Products.Include(p => p.Price).Include(m => m.Manufacturer)
                .Include(i => i.Images).ToListAsync();
            {
                return entities;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR GetAllProductEntitiesAsync::" + ex.Message);
        }
        return null!;
    }

    public override async Task<Product> GetOneAsync(Expression<Func<Product, bool>> expression)
    {
        try
        {
            var entity =  await  _context.Set<Product>().Include(p => p.Price).Include(m => m.Manufacturer).Include(i => i.Images).FirstOrDefaultAsync(expression);
            if (entity != null)
            {
                return entity;
            }

            return null!;

        }
        catch (Exception ex) 
        {
            Debug.WriteLine("Error :: GetOneProductEntity "+ ex.Message);
            return null!;
        }
    }
}

