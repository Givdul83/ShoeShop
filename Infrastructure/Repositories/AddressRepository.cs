
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
        _context= context;
        }

    //public async Task<AddressEntity> CreateAddressAsync(string streetName, string postalCode, string city)
    //{
    //    try
    //    {
    //        var newAddressExist = await _context.Addresses.FirstOrDefaultAsync(x => x.StreetName == streetName && x.PostalCode == postalCode && x.City == city);
    //        if (newAddressExist != null)
    //        {
    //            return newAddressExist;
    //        }
    //        else
    //        {
    //            var newAddress = new AddressEntity
    //            {
    //                StreetName = streetName,
    //                PostalCode = postalCode,
    //                City = city
    //            };

    //            _context.Addresses.Add(newAddress);
    //            await _context.SaveChangesAsync();
    //            return newAddress;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Debug.WriteLine("ERROR :: CreateAddressEtity " + ex.Message);
    //        return null!;
    //    }
    //}

    //public async Task<AddressEntity> UpdateAddressAsync( AddressEntity entity)
    //{
    //    try
    //    {
    //        var entityToUpdate = await _context.Set<AddressEntity>().FirstOrDefaultAsync(x => x.StreetName == entity.StreetName && x.PostalCode == entity.PostalCode && x.City == entity.City);
    //        if (entityToUpdate != null)
    //        {
    //            await _context.SaveChangesAsync();
    //            return entityToUpdate;


    //        }
    //        else
    //        {
    //            var newAddress = new AddressEntity 
    //            {
    //                StreetName =  entity.StreetName,
    //                PostalCode = entity.PostalCode,
    //                City= entity.City };
    //            ;
    //            _context.Addresses.Add(newAddress);
    //            await _context.SaveChangesAsync();
    //            return newAddress;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Debug.WriteLine("ERROR :: UpdateAddressEnity " + ex.Message);
    //        return null!;
    //    }
    //}
}

