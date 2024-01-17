using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class CustomerTypeRepository(CustomerDbContext context) : BaseRepo<CustomerTypeEntity>(context)
{
    private readonly CustomerDbContext _context = context;
}
