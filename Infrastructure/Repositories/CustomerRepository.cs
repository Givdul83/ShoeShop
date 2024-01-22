using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class CustomerRepository(CustomerDbContext context) : BaseRepo<CustomerEntity,CustomerDbContext>(context)
{

    private readonly CustomerDbContext _context = context;
}
