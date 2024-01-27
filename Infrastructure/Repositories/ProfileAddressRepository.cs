
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

        public  async Task<bool> DeleteProfileAddressAsync(ProfileAddressEntity profileAddressEntity)
        {
            try
            {
                _context.Remove(profileAddressEntity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR  GetAllProfileAddressAsync::" + ex.Message);
            }
            return false;
        }
    

        public async Task<IEnumerable<ProfileAddressEntity>> GetAllProfileAddressByIdAsync(Expression<Func<ProfileAddressEntity, bool>> expression)

        {
           try
            {
                return await _context.Set<ProfileAddressEntity>().Where(expression).ToListAsync();
                   
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR  GetAllProfileAddressAsync::" + ex.Message);
            }
            return null!;
        }
    }
        }














        //    public async override Task<bool> DeleteAsync(ProfileAddressEntity entity)
        //    {
        //        try
        //        {
        //            if (entity != null)
        //            {
        //                _context.ProfileAddresses.Remove(entity);
        //                await _context.SaveChangesAsync();
        //                return true;
        //            }
        //            return false;
        //        }
        //        catch (Exception ex)
        //        {
        //            Debug.WriteLine("Error :: DeleteProfileAddressEntityAsync " + ex.Message);
        //            return false;
        //        }
        //    }

        //    public override Task<IEnumerable<ProfileAddressEntity>> GetAllAsync()
        //    {
        //        return base.GetAllAsync();
        //    }
        //}





    
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

