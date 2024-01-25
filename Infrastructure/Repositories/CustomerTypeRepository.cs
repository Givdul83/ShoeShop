using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class CustomerTypeRepository : BaseRepo<CustomerTypeEntity, CustomerDbContext>
{
    private readonly CustomerDbContext _context;


    public CustomerTypeRepository(CustomerDbContext context) : base(context)
    {
        _context = context;
    }
}

//    public async Task<CustomerTypeEntity> CreateCustomerTypeAsync(string typeOfCustomer)
//    {
//       var existingCustomerType = await _context.CustomersTypes.FirstOrDefaultAsync(x => x.TypeOfCustomer == typeOfCustomer);

//        if (existingCustomerType != null) 
//        {
//            return existingCustomerType;
//        }
//        else
//        {
//            var newCustomerType = new CustomerTypeEntity
//            {
//                TypeOfCustomer = typeOfCustomer
//            };
//            await _context.AddAsync(newCustomerType);
//            await _context.SaveChangesAsync();
//            return newCustomerType;
//        }
//    }

//    public async Task<CustomerTypeEntity> UpdateCustomerTypeAsync(CustomerTypeEntity entity)
//    {
//        try
//        {
//            var alreadyExist = await _context.CustomersTypes.FirstOrDefaultAsync(x => x.TypeOfCustomer == entity.TypeOfCustomer);

//            if (alreadyExist != null)
//            {
//                if (alreadyExist.Id == entity.Id)
//                {

//                    return alreadyExist;
//                }
//                return alreadyExist;
//            }
//            else
//            {
//                var newCustomerType = await _context.Set<CustomerTypeEntity>().FirstOrDefaultAsync(x => x.Id == entity.Id);
//                if(newCustomerType != null)
//                {
//                    newCustomerType.TypeOfCustomer = entity.TypeOfCustomer;
//                    await _context.SaveChangesAsync();
//                    return newCustomerType;
//                }
//                else
//                {
//                    Debug.WriteLine("no customerType found to update");
//                }
//            }
//        }
//        catch (Exception ex)
//        {
//            Debug.WriteLine("ERROR UpdateCustomerType::" + ex.Message);
//        }
//        return null!;
//    }
//}


