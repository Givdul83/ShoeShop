
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AddressRepository : BaseRepo<AddressEntity, CustomerDbContext>
{
    private readonly CustomerDbContext _context;

    public AddressRepository(CustomerDbContext context) : base(context)
        {
        _context= context;
        }

    public async Task<AddressEntity> CreateAsync(string streetName, string postalCode, string city)
    {
        var newAddressExist = await _context.Addresses.FirstOrDefaultAsync(x => x.StreetName== streetName && x.PostalCode== postalCode && x.City== city);
        if (newAddressExist != null) 
        {
            return newAddressExist;
        }
        else
        {
            var newAddress = new AddressEntity
            {
                StreetName = streetName,
                PostalCode = postalCode,
                City = city 
            };

            _context.Addresses.Add(newAddress);
            await _context.SaveChangesAsync();
            return newAddress;


        }    
    }
}

