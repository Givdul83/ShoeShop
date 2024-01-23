using Infrastructure.Contexts;
using Infrastructure.Entities;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class CustomerRepository(CustomerDbContext context) : BaseRepo<CustomerEntity,CustomerDbContext>(context)
{

    private readonly CustomerDbContext _context = context;

    public async Task DeleteAsync(CustomerEntity entity)
    {
        _context.Customers.Remove(entity);
        await _context.SaveChangesAsync();
        
    }
}
