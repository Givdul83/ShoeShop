
using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infrastructure.Services
{
    public class ProfileAddressService(AddressRepository addressRepository, ProfileRepository profileRepository, ProfileAddressRepository profileAddressRepository)
    {
        private readonly AddressRepository _addressRepository = addressRepository;
        private readonly ProfileRepository _profileRepository = profileRepository;
        private readonly ProfileAddressRepository _profileAddressRepository = profileAddressRepository;



        public async Task<ProfileAddressEntity> CreateProfileAddressAsync(int profileId, int addresId)
        {
            try
            {
                var profileAddress = await _profileAddressRepository.GetOneAsync(x => x.ProfileId == profileId && x.AddressId == addresId);
                if (profileAddress != null)
                {
                    return await _profileAddressRepository.UpdateAsync(x => x.ProfileId == profileId, profileAddress);
                }


                var newProfileAddressEntity = new ProfileAddressEntity
                {
                    AddressId = addresId,
                    ProfileId = profileId,
                };

                return await _profileAddressRepository.CreateAsync(newProfileAddressEntity);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(" ERROR CreateProfileAddressAsync " + ex.Message);
                return null!;
            }
        }


        public async Task<ProfileAddressEntity> UpdateProfileAddressAsync(int profileId, int addressId)
        {
            try
            {
                var profileAddressToUpdate = await _profileAddressRepository.GetOneAsync(x => x.ProfileId == profileId);
                if (profileAddressToUpdate != null)
                {

                    if (profileAddressToUpdate.AddressId == addressId)
                    {
                        return profileAddressToUpdate;
                    }
                    else
                    {
                        if (profileAddressToUpdate.AddressId != addressId)
                        {
                            profileAddressToUpdate.AddressId = addressId;
                            await _profileAddressRepository.UpdateAsync(x => x.ProfileId == profileAddressToUpdate.ProfileId, profileAddressToUpdate);
                            return profileAddressToUpdate;
                        }

                    }
                }
                return null!;


            }
            catch (Exception ex)
            {
                Debug.WriteLine(" ERROR UpdateProfileAddressAsync " + ex.Message);
                return null!;
            }
        }

        public async Task<bool> DeleteDuplicateProfileAddressesAsync(int profileId, int addressId)
        {
            try
            {
                var entities = await _profileAddressRepository.GetAllProfileAddressByIdAsync(x => x.ProfileId == profileId);

                if (entities != null && entities.Any())
                {
                    bool deletedAny = false;

                    foreach (var entity in entities)
                    {
                        if (entity.AddressId != addressId)
                        {
                            await _profileAddressRepository.DeleteProfileAddressAsync(entity);
                            deletedAny = true;
                        }
                    }
                    return deletedAny;
                }
                return false;
            }
            catch (Exception ex)
            {

                Debug.WriteLine(" ERROR DeleteDuplicateProfileAddressesAsync " + ex.Message);
                return false;
            }
        }
    }
}