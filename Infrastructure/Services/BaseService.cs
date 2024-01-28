
using Infrastructure.Contexts;
using Infrastructure.Dtos;
using Infrastructure.Entities;

using Infrastructure.Repositories;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infrastructure.Services;

public class BaseService(AddressRepository addressRepository, ProfileRepository profileRepository, CustomerRepository customerRepository, CustomerTypeRepository customerTypeRepository,
    ProfileAddressRepository profileAddressRepository, AddressService addressService, CustomerTypeService customerTypeService, CustomerService customerService, ProfileService profileService, ProfileAddressService profileAddressService)
{

    private readonly AddressRepository _addressRepository = addressRepository;
    private readonly ProfileRepository _profileRepository = profileRepository;
    private readonly CustomerRepository _customerRepository = customerRepository;
    private readonly CustomerTypeRepository _customerTypeRepository = customerTypeRepository;
    private readonly ProfileAddressRepository _profileAddressRepository = profileAddressRepository;
    private readonly AddressService _addressService = addressService;
    private readonly CustomerTypeService _customerTypeService = customerTypeService;
    private readonly CustomerService _customerService = customerService;
    private readonly ProfileService _profileService = profileService;
    private readonly ProfileAddressService _profileAddressService = profileAddressService;







    public async Task<bool> CreateUserAsync(UserRegDto dtoReg)
    {
        try
        {
            var address = await _addressService.CreateAddressAsync(dtoReg);
            var customerType = await _customerTypeService.CreateCustomerType(dtoReg.TypeOfCustomer);
            if (customerType != null && address != null)
            {

                var customer = await _customerService.CreateNewCustomer(dtoReg.Email, customerType.Id);
                if (customer != null)
                {
                    var profile = await _profileService.CreateNewPofile(customer.Id, dtoReg.FirstName, dtoReg.LastName);
                    if (profile != null)
                    {
                        var profileAddress = await _profileAddressService.CreateProfileAddressAsync(profile.Id, address.Id);
                    }
                }

            }
            return true;
        }

        catch (Exception ex)
        {
            Debug.WriteLine("Error CreateUserAsync:: " + ex.Message);
            return false;
        }
    }

    //public async Task<UserDto> GetUserDtoAsync(string email)
    //{
    //    try
    //    {
    //        var foundCustomer = await _customerRepository.GetOneAsync(x => x.Email == email);
    //        var foundCustomerType = await _customerTypeRepository.GetOneAsync(x => x.Id == foundCustomer.CustomerTypeId);
    //        var foundProfile = await _profileRepository.GetOneAsync(x => x.CustomerId == foundCustomer.Id);
    //        var foundProfileAddress = await _profileAddressRepository.GetOneAsync(x => x.ProfileId == foundProfile.Id);
    //        var foundAddress = await _addressRepository.GetOneAsync(x => x.Id == foundProfileAddress.AddressId);
    //        var foundDto = UserDto.FromEntities(foundCustomer, foundProfile, foundAddress, foundCustomerType);

    //        return foundDto;
    //    }

    //    catch (Exception ex)
    //    {
    //        Debug.WriteLine("Error GetUserDtoAsync:: " + ex.Message);
    //        return null!;
    //    }
    //    {

    //    }
    //}


    public async Task<bool> ControlUserExistAsync(string email)
    {
        try
        {
            if (await _customerRepository.ExistAsync(x => x.Email == email))
            {
                Console.WriteLine($"User with {email} is already created", "ControlUSerExist");

                return true;
            }

            return false;
        }

        catch (Exception ex)
        {
            Debug.WriteLine("Error ControllUserExist:: " + ex.Message);
            return false;
        }
    }

    public async Task<bool> DeleteUserAsync(string email)
    {
        try
        {

            if (!await ControlUserExistAsync(email))
            {

                Console.WriteLine("User not found");
                return false;
            }
            var customerToDelete = await _customerRepository.GetOneAsync(x => x.Email == email);
            if (customerToDelete != null)
            {
                var profileToDelete = await _profileRepository.GetOneAsync(x => x.CustomerId == customerToDelete.Id);
                if (profileToDelete != null)
                {

                    await _customerRepository.DeleteAsync(x => x.Email == customerToDelete.Email);
                    await _profileRepository.DeleteAsync(x => x.Id == profileToDelete.Id);

                    Console.WriteLine("UserDeleted");
                    return true;
                }
            }
            return false;
        }

        catch (Exception ex)
        {
            Debug.WriteLine("Error DeleteUserAsync:: " + ex.Message);
            return false;
        }
    }

    public async Task<UserDto> FindUserAsync(string email)
    {
        try
        {
            var foundCustomer = await _customerRepository.GetOneAsync(x => x.Email == email);
            if (foundCustomer != null && foundCustomer.Profile != null)
            {
                var foundProfileAddress = foundCustomer.Profile.ProfileAddresses.FirstOrDefault(x => x.ProfileId == foundCustomer.Profile.Id);
                if (foundProfileAddress != null)
                {
                    var foundUserDto = new UserDto
                    {
                        Email = foundCustomer.Email,
                        FirstName = foundCustomer.Profile.FirstName,
                        LastName = foundCustomer.Profile.LastName,
                        StreetName = foundProfileAddress.Address.StreetName,
                        PostalCode = foundProfileAddress.Address.PostalCode,
                        City = foundProfileAddress.Address.City,
                        TypeOfCustomer = foundCustomer.CustomerType.TypeOfCustomer,
                        Created = foundCustomer.Created
                    };
                    return foundUserDto;
                }
            }
            Console.WriteLine("User to update not found");
            return null!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error:: FindUser" + ex.Message);
            Console.WriteLine("User Not Found");
            return null!;
        }
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        try
        {

            var userDtos = new List<UserDto>();

            var customers = await _customerService.GetAllCustomerAsync();
           
            if (customers != null)
            {

                foreach (var customer in customers)
                {

                    var customerType = await _customerTypeRepository.GetOneAsync(ct => ct.Id == customer.CustomerTypeId);
                    var profile = await _profileRepository.GetOneAsync(x => x.CustomerId == customer.Id);
                    var profileAddress = await _profileAddressRepository.GetOneAsync(pa => pa.ProfileId == profile.Id);
                     var address = await _addressRepository.GetOneAsync(a => a.Id == profileAddress.AddressId);


     
                var dto = new UserDto
                        {
                            Email = customer.Email,
                            FirstName = profile.FirstName,
                            LastName = profile.LastName,
                            StreetName = address.StreetName,
                            PostalCode = address.PostalCode,
                            City = address.City,
                            TypeOfCustomer = customerType.TypeOfCustomer,
                            Created = customer.Created,
                        };

                        userDtos.Add(dto);
                    }
                return userDtos;
            }
               

            
            return null!;
        }
        
        catch (Exception ex)
        {
            Debug.WriteLine("Error ::GetAllUsersAsync " + ex.Message);
            return null!;
        }


    }
    public async Task<bool> UpdateUser(UserRegDto userRegDto)
    {
        try
        {
            var customerToUpdate = await _customerRepository.GetOneAsync(x => x.Email == userRegDto.Email);
            var addressToUpdate = await _addressService.UpdateAddressAsync(userRegDto);
            var profileToUpdate = await _profileService.UpdateProfileAsync(userRegDto);
            var customerTypeToUpdate = await _customerTypeService.UpdateCustomerTypeAsync(userRegDto);
            var profileAddressToUpdate = await _profileAddressService.CreateProfileAddressAsync(profileToUpdate.Id, addressToUpdate.Id);
            if (profileAddressToUpdate != null)
            {
                await _profileAddressService.DeleteDuplicateProfileAddressesAsync(profileAddressToUpdate.ProfileId, profileAddressToUpdate.AddressId);
                await _customerService.UpdateCustomerEmailAsync(customerToUpdate, userRegDto);
                return true;
            }
            return false;
        }


        catch (Exception ex)
        {
            Debug.WriteLine("Error UpdateUser:: " + ex.Message);
            return false;
        }


    }
}






















