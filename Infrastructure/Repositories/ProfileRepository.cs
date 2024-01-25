using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class ProfileRepository(CustomerDbContext context) : BaseRepo<ProfileEntity, CustomerDbContext>(context)
{
    private readonly CustomerDbContext _context = context;

    //public async Task DeleteAsync(ProfileEntity entity)
    //{
    //    _context.Profiles.Remove(entity);
    //    await _context.SaveChangesAsync();
    //}

    //public async Task<ProfileEntity> UpdateProfileAsync(ProfileEntity entity)
    //{
    //        try
    //        {
    //            var entityToUpdate = await _context.Set<ProfileEntity>().FirstOrDefaultAsync(x => x.CustomerId == entity.CustomerId);
    //            if (entityToUpdate != null)
    //            {
    //            entityToUpdate.FirstName= entity.FirstName;
    //            entityToUpdate.LastName= entity.LastName;

    //            await _context.SaveChangesAsync();
    //                return entityToUpdate;
    //            }
    //            return null!;
    //        }
    //        catch (Exception ex)
    //        {
    //            Debug.WriteLine("Error :: ProfileUpdate" + ex.Message);
    //            return null!;
    //        }
    //    }
    }
