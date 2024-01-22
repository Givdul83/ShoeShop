using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class ProfileRepository(CustomerDbContext context) : BaseRepo<ProfileEntity, CustomerDbContext>(context)
{
    private readonly CustomerDbContext _context = context;
}
