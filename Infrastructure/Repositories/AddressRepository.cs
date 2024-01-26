
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class AddressRepository : BaseRepo<AddressEntity, CustomerDbContext>
{
    private readonly CustomerDbContext _context;

    public AddressRepository(CustomerDbContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<AddressEntity>> GetAllAsync()
    {
        try
        {
            var entities = await _context.Addresses.Include(pa => pa.ProfileAddresses)
                .ThenInclude(p => p.Profile).ThenInclude(c => c.Customer)
                .ToListAsync();
               
            if (entities !=null) 
            {
                return entities;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR GetAllAddressesAsync::" + ex.Message);
        }
        return null!;
    }


    public override Task<AddressEntity> GetOneAsync(Expression<Func<AddressEntity, bool>> expression)
    {
        return base.GetOneAsync(expression);
    }
}

