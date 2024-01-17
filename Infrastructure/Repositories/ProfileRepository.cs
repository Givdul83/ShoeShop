using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class ProfileRepository(CustomerDbContext context) : BaseRepo<ProfileEntity>(context)
{
    private readonly CustomerDbContext _context = context;

    public override IEnumerable<ProfileEntity> GetAll()
    {
        try
        {
            return _context.Profiles.Include(x => x.Customer.Email).ToList();
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR ::" + ex.Message);
        }
        return null!;
    }
            
        

    public override ProfileEntity GetOne(Expression<Func<ProfileEntity, bool>> predicate)
    {
        try
        {
            return _context.Profiles.Include(x => x.Customer.Email).FirstOrDefault(predicate, null);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR ::" + ex.Message);
        }
        return null!;
    }
}

