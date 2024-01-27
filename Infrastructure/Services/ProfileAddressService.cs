
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
                    return profileAddress;
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

                var profileAddresses = await _profileAddressRepository.GetAllProfileAddressByIdAsync(x => x.ProfileId == profileId);


                foreach (var profileAddress in profileAddresses)
                {
                    if (profileAddress.AddressId == addressId)
                    {

                        var isUpdated = await _profileAddressRepository.CreateAsync(profileAddress);
                        return isUpdated;
                    }
                    else
                    {

                        await _profileAddressRepository.DeleteProfileAddressAsync(profileAddress);
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

    }
}