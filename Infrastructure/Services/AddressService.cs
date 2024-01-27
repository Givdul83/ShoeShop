
using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Services;

public class AddressService(AddressRepository addressRepository, CustomerRepository customerRepository, ProfileRepository profileRepository, ProfileAddressRepository profileAddressRepository)
{
    private readonly AddressRepository _addressRepository = addressRepository;
    private readonly CustomerRepository _customerRepository = customerRepository;
    private readonly ProfileRepository _profileRepository = profileRepository;
    private readonly ProfileAddressRepository _profileAddressRepository = profileAddressRepository;

    public async Task<AddressEntity> CreateAddressAsync(UserRegDto userRegDto)
    {
        try
        {
            var addressEntity = await _addressRepository.GetOneAsync(x => x.StreetName == userRegDto.StreetName && x.City == userRegDto.City
            && x.PostalCode == userRegDto.PostalCode);
            if (addressEntity == null)
            {
                var newAddressEntity = await _addressRepository.CreateAsync(new AddressEntity
                {
                    StreetName = userRegDto.StreetName,
                    PostalCode = userRegDto.PostalCode,
                    City = userRegDto.City,
                });
                if (newAddressEntity != null)
                {

                    return newAddressEntity;
                }
            }

            else
                if (addressEntity != null)
            {
                return addressEntity;
            }

            return null!;
        }


        catch (Exception ex)
        {
            Debug.WriteLine(" ERROR CreateAddressAsync " + ex.Message);
            return null!;
        }
    }

    public async Task<AddressEntity> UpdateAddressAsync(UserRegDto userRegDto)
    {
        try
        {
            var addressToUpdate = await _addressRepository.GetOneAsync(x => x.StreetName == userRegDto.StreetName && x.City == userRegDto.City
                && x.PostalCode == userRegDto.PostalCode);

            if (addressToUpdate == null)
            {
                var newAddress =  await CreateAddressAsync(userRegDto);

                Console.WriteLine("Address Updated");
                return newAddress;


            }
            else
            {
                

                var updatedAddress = await _addressRepository.UpdateAsync(x => x.Id == addressToUpdate.Id, addressToUpdate);
               
                Console.WriteLine("No changes to Address Detected");
                return updatedAddress;
            }

        }

        catch (Exception ex)
        {
            Debug.WriteLine("Error UpdateAddressAsync " + ex.Message);
            return null!;
        }
    }

    public async Task<IEnumerable<AddressDto>> GetAllAddressesAsync()
    {
        try
        {
            var addressEntities = await _addressRepository.GetAllAsync();
            if (addressEntities != null)
            {
                var list = new List<AddressDto>();
                foreach (var address in addressEntities)
                    list.Add(new AddressDto(address.StreetName, address.PostalCode, address.City, address.Id));

                return list;
            }
            else { return null!; }
        }

        catch (Exception ex)
        {
            Debug.WriteLine("Error :: GetAllAddressesAsync " + ex.Message);
            return null!;
        }
    }

    public async Task<AddressEntity> GetAddressByEmailAsync(string email)
    {
        try
        {
            var customerEntity = await _customerRepository.GetOneAsync(x => x.Email == email);
            if (customerEntity != null)
            {
                var profileEntity = await _profileRepository.GetOneAsync(x => x.CustomerId == customerEntity.Id);

                if (profileEntity != null)
                {
                    var profileAddressEntity = await _profileAddressRepository.GetOneAsync(pa => pa.ProfileId == profileEntity.Id);
                    if(profileAddressEntity!= null!)
                    {
                        var addressEntity = await _addressRepository.GetOneAsync(a => a.Id == profileAddressEntity.AddressId);
                        if (addressEntity != null)
                        {
                            return addressEntity;
                        }
                    }

                }
                return null!;
            }
            else return null!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error :: GetProfileByEmailAsync " + ex.Message);
            return null!;
        }
    }
    public async Task<bool> DeleteAddressAsync(Expression<Func<AddressEntity, bool>> expression)
    {
        try
        {
            var result = await _addressRepository.DeleteAsync(expression);
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: DeleteAddressTypeAsync " + ex.Message);
            return false!;

        }
    }

}
