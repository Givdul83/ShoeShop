
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class ProfileAddressRepository(CustomerDbContext context) : BaseRepo<ProfileAddressEntity, CustomerDbContext>(context)
    {
        private readonly CustomerDbContext _context = context;

        public async Task DeleteAsync(ProfileAddressEntity entity)
        {
            _context.ProfileAddresses.Remove(entity);
            await _context.SaveChangesAsync();
                
        }
    }
}
