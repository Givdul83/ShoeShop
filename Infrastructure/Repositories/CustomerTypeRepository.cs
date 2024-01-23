using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CustomerTypeRepository : BaseRepo<CustomerTypeEntity, CustomerDbContext>
{
    private readonly CustomerDbContext _context;


    public CustomerTypeRepository(CustomerDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<CustomerTypeEntity> CreateAsync(string typeOfCustomer)
    {
       var existingCustomerType = await _context.CustomersTypes.FirstOrDefaultAsync(x => x.TypeOfCustomer == typeOfCustomer);

        if (existingCustomerType != null) 
        {
            return existingCustomerType;
        }
        else
        {
            var newCustomerType = new CustomerTypeEntity
            {
                TypeOfCustomer = typeOfCustomer
            };
            await _context.AddAsync(newCustomerType);
            await _context.SaveChangesAsync();
            return newCustomerType;
        }
    }
}

