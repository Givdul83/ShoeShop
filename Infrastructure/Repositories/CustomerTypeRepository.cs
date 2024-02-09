using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class CustomerTypeRepository : BaseRepo<CustomerTypeEntity, CustomerDbContext>
{
    private readonly CustomerDbContext _context;


    public CustomerTypeRepository(CustomerDbContext context) : base(context)
    {
        _context = context;
    }
}




