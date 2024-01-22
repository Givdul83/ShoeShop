
using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories
{
    public class ProfileAddressRepository(CustomerDbContext context) : BaseRepo<ProfileAddressEntity, CustomerDbContext>(context)
    {
        private readonly CustomerDbContext _context = context;
    }
}
