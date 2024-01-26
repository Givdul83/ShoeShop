using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class CustomerRepository(CustomerDbContext context) : BaseRepo<CustomerEntity, CustomerDbContext>(context)
{

    private readonly CustomerDbContext _context = context;

    public override async Task<IEnumerable<CustomerEntity>> GetAllAsync()
    {
        try
        {
            var entities = await _context.Customers.Include(p => p.Profile).ThenInclude(pa =>pa.ProfileAddresses)
                .ThenInclude(a =>a.Address)
                .Include(ct => ct.CustomerType).ToListAsync();
            {
                return entities;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR GetAllCustomerEntitiesAsync::" + ex.Message);
        }
        return null!;
    }

    public override async Task<CustomerEntity> GetOneAsync(Expression<Func<CustomerEntity, bool>> expression)
    {
        try
        {
            var entity = await _context.Set<CustomerEntity>().Include(p => p.Profile).ThenInclude(pa => pa.ProfileAddresses)
                .ThenInclude(a => a.Address)
                .Include(ct => ct.CustomerType).
                FirstOrDefaultAsync(expression);
            if (entity != null)
            {
                return entity;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR GetOneCustomerEntityAsync::" + ex.Message);
        }
        return null!;
    }
      
    
} 
