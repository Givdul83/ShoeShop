using Infrastructure.ProductContext;
using Infrastructure.ProductEntities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class ManufacturerRepository : ProductsBaseRepo<Manufacturer, ProductDbContext>
    {
        private readonly ProductDbContext _context;

        public ManufacturerRepository(ProductDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Manufacturer>> GetAllAsync()
        {
            try
            {
                var entities = await _context.Manufacturers.Include(p => p.Products).ThenInclude(pr => pr.Price).ToListAsync();
                if (entities != null)
                {
                    return entities;
                }

                return null!;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR GetAllManufacturersEntitiesAsync::" + ex.Message);
            }
            return null!;
        }

        public override async Task<Manufacturer> GetOneAsync(Expression<Func<Manufacturer, bool>> expression)
        {
            try 
            { 
            var entity = await _context.Set<Manufacturer>().Include(p => p.Products).ThenInclude(pr => pr.Price).FirstOrDefaultAsync(expression);
            if (entity != null)
            {
                return entity;
            }

            return null!;
        }
         catch (Exception ex)
            {
                Debug.WriteLine("ERROR GetAllManufacturersEntitiesAsync::" + ex.Message);
            }
            return null!;
    }
    }
}
