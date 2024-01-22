
using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class AddressRepository(CustomerDbContext context) : BaseRepo<AddressEntity, CustomerDbContext>(context)
{
    private readonly CustomerDbContext _context = context;
}

