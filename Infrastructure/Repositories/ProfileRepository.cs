using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class ProfileRepository(CustomerDbContext context) : BaseRepo<ProfileEntity, CustomerDbContext>(context)
{
    private readonly CustomerDbContext _context = context;

    public override async Task<IEnumerable<ProfileEntity>> GetAllAsync()
    {
        try
        {
            var entities = await _context.Profiles.Include(c => c.Customer)
                .ThenInclude(ct => ct.CustomerType)
                .Include(pa => pa.ProfileAddresses).ThenInclude(a => a.Address)
                .ToListAsync();
            {
                return entities;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR GetAllProfilesAsync::" + ex.Message);
        }
        return null!;
    }


    public override async Task<ProfileEntity> GetOneAsync(Expression<Func<ProfileEntity, bool>> expression)
    {
       try
        {
            var entity = await _context.Set<ProfileEntity>().Include(c => c.Customer)
                .ThenInclude(ct => ct.CustomerType)
                .Include(pa => pa.ProfileAddresses).ThenInclude(a=>a.Address).
                FirstOrDefaultAsync(expression);
            if (entity != null)
            {
                return entity;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR GetOneProfileAsync::" + ex.Message);
        }
            return null!;
          }
    }

