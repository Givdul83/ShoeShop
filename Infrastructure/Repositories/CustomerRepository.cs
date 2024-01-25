using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class CustomerRepository(CustomerDbContext context) : BaseRepo<CustomerEntity,CustomerDbContext>(context)
{

    private readonly CustomerDbContext _context = context;

    //public async Task DeleteAsync(CustomerEntity entity)
    //{
    //    _context.Customers.Remove(entity);
    //    await _context.SaveChangesAsync();
        
    //}

    //public async Task<CustomerEntity> UpdateCustomerAsync(CustomerEntity entity)
    //{
    //    try
    //    {

    //        var entityToUpdate = await _context.Set<CustomerEntity>().FirstOrDefaultAsync(x => x.Email == entity.Email);
    //        if (entityToUpdate != null)
    //        {

    //           entityToUpdate.CustomerTypeId = entity.CustomerTypeId;

    //            await _context.SaveChangesAsync();

    //            return entityToUpdate;
    //        }
    //        return null!;
    //    }
    //    catch (Exception ex)
    //    {
    //        Debug.WriteLine("Error :: CustomerAsyncUpdate" + ex.Message);
    //        return null!;
    //    }
    //}
}
