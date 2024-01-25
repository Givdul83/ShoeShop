
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Diagnostics;
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
//        public async Task<bool> DeleteProfileAdressEntityAsync(ProfileAddressEntity entity)
//        {
//            try
//            {
//                var entityToDelete = await _context.Set<ProfileAddressEntity>().FirstOrDefaultAsync(x => x.ProfileId == entity.ProfileId && x.AddressId == entity.AddressId);
//                if (entityToDelete != null)
//                {
//                    _context.ProfileAddresses.Remove(entityToDelete);
//                    await _context.SaveChangesAsync();
//                    return true;
                      
//                }
//                return false;
//            }
//            catch (Exception ex)
//            {
//                Debug.WriteLine("Error :: DeleteProfileAddressEntityAsync " + ex.Message);
//                return false;
//            }
            
//        }

//        public async Task<ProfileAddressEntity> UpdateProfileAddressAsync( ProfileAddressEntity entity)
//        {

           
//            try
//            {
//                var entityToUpdate = await _context.Set<ProfileAddressEntity>().FirstOrDefaultAsync(x => x.ProfileId == entity.ProfileId);
//                if (entityToUpdate != null)
//                {
//                    if (entityToUpdate.AddressId != entity.AddressId)
//                    {
//                        var newProfileAddress = new ProfileAddressEntity();
//                        newProfileAddress.AddressId = entity.AddressId;
//                        await _context.ProfileAddresses.AddAsync(newProfileAddress);
//                        await _context.SaveChangesAsync();
//                    }
//                    else
//                        return entityToUpdate;

//                }
//                return null!;
//            }
//            catch (Exception ex)
//            {
//                Debug.WriteLine("ERROR Update ProfileAddressEntity " + ex.Message);
//                return null!;
//            }
//        }
//    }
//}
      
