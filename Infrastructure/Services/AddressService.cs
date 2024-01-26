
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

    public async Task<AddressDto> CreateAddressAsync(UserRegDto userRegDto)
    {
        try
        {
            if (!await _addressRepository.ExistAsync(x => x.StreetName == userRegDto.StreetName && x.City == userRegDto.City
            && x.PostalCode == userRegDto.PostalCode))
            {
                var newAddressEntity = await _addressRepository.CreateAsync(new AddressEntity
                {
                    StreetName = userRegDto.StreetName,
                    PostalCode = userRegDto.PostalCode,
                    City = userRegDto.City,
                });
                if (newAddressEntity != null)
                {
                    var createdAddress = await _addressRepository.GetOneAsync(x => x.Id == newAddressEntity.Id);
                    if (createdAddress != null)
                    {
                        var neawAddressDto = new AddressDto(createdAddress.StreetName, createdAddress.PostalCode,
                            createdAddress.City, createdAddress.Id);

                        return neawAddressDto;
                    }
                }
            }
            return null!;
        }

        catch (Exception ex)
        {
            Debug.WriteLine(" ERROR CreateAddressAsync " + ex.Message);
            return null!;
        }
    }

    public async Task<AddressDto> UpdateAddressAsync(UserRegDto userRegDto)
    {
        try
        {
            var addressToUpdate = await _addressRepository.GetOneAsync(x => x.StreetName == userRegDto.StreetName && x.City == userRegDto.City
                && x.PostalCode == userRegDto.PostalCode);

            if (addressToUpdate == null)
            {
                return await CreateAddressAsync(userRegDto);
               
            }
            else
            {
                addressToUpdate.StreetName = userRegDto.StreetName;
                addressToUpdate.City = userRegDto.City;
                addressToUpdate.PostalCode = userRegDto.PostalCode;

                var updatedAddress = await _addressRepository.UpdateAsync(x => x.Id == addressToUpdate.Id, addressToUpdate);
                return new AddressDto(updatedAddress.StreetName, updatedAddress.PostalCode,
                                      updatedAddress.City, updatedAddress.Id);

               
            }
            
        }
       
        catch (Exception ex)
        {
            Debug.WriteLine("Error UpdateAddressAsync " +ex.Message);
            return null !;
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

    public async Task<AddressDto> GetAddressByEmailAsync(string email)
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
                            var addressDto = new AddressDto(addressEntity.StreetName,addressEntity.PostalCode, addressEntity.City, addressEntity.Id);
                            return addressDto;
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
