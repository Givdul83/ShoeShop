
using Infrastructure.Contexts;
using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Repositories;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Services;

public class CustomerService(AddressRepository addressRepository, ProfileRepository profileRepository, CustomerRepository customerRepository, CustomerTypeRepository customerTypeRepository, ProfileAddressRepository profileAddressRepository)
{

    private readonly AddressRepository _addressRepository = addressRepository;
    private readonly ProfileRepository _profileRepository = profileRepository;
    private readonly CustomerRepository _customerRepository = customerRepository;
    private readonly CustomerTypeRepository _customerTypeRepository = customerTypeRepository;
    private readonly ProfileAddressRepository _profileAddressRepository = profileAddressRepository;
  







    //public async Task<bool> CreateCustomer(CustomerDtoReg dtoReg)
    //{
    //    var newcustomerTypeEntity =  await _customerTypeRepository.CreateAsync(dtoReg);
        
    //    await _customerRepository.CreateAsync(dtoReg);
    //    await _profileRepository.CreateAsync(dtoReg);
    //    await _addressRepository.CreateAsync(dtoReg);

    //    return true;
    //}

    public async Task <CustomerDto> GetCustomerDto(string email)
    {
        var foundCustomer = await _customerRepository.GetOneAsync(x => x.Email == email);
        var foundCustomerType = await _customerTypeRepository.GetOneAsync(x => x.Id == foundCustomer.CustomerTypeId);
        var foundProfile = await _profileRepository.GetOneAsync(x => x.CustomerId == foundCustomer.Id);
        var foundProfileAddress = await _profileAddressRepository.GetOneAsync(x => x.ProfileId == foundProfile.Id);
        var foundAddress = await _addressRepository.GetOneAsync(x => x.Id == foundProfileAddress.AddressId);


        var foundDto = CustomerDto.FromEntities(foundCustomer, foundProfile, foundAddress,foundCustomerType);

        return foundDto;
       
        {
            
        }
    }










    //public async Task<bool> CreateCustomerDto(CustomerDto customerDto)
    //{
    //    try
    //    {
    //        if (!await ControlCustomerExist(customerDto.Email))
    //        {
    //            var customerTypeEntity = await _customerTypeRepository.CreateCustomerTypeAsync(customerDto.CustomerType);

    //            var customerEntity = await _customerRepository.CreateAsync(new CustomerEntity
    //            {
    //                Id = Guid.NewGuid(),
    //                Created = DateTime.Now,
    //                Email = customerDto.Email,
    //                CustomerTypeId = customerTypeEntity.Id
    //            });
    //            var profileEntity = await _profileRepository.CreateAsync(new ProfileEntity
    //            {
    //                FirstName = customerDto.FirstName,
    //                LastName = customerDto.LastName,
    //                CustomerId = customerEntity.Id
    //            });
    //            var addressEntity = await _addressRepository.CreateAddressAsync(customerDto.StreetName, customerDto.PostalCode, customerDto.City);

    //            var profileAddress = await _profileAddressRepository.CreateAsync(new ProfileAddressEntity
    //            {
    //                AddressId = addressEntity.Id,
    //                ProfileId = profileEntity.Id
    //            });

    //            Console.WriteLine("sparat");
    //            return true;
    //        }
    //        else
    //        {
    //            Console.WriteLine("GIck ej");
    //            return false;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Debug.WriteLine("ERROR:: CreateCustomerDto" + ex.Message);
    //        return false;
    //    }
    //}
    //public async Task<bool> ControlCustomerExist(string email)
    //{
    //    if (await _customerRepository.ExistAsync(x => x.Email == email))
    //    {
    //        Console.WriteLine($"User with {email} is already created", "ControlUSerExist");
    //        return true;
    //    }

    //    return false;

    //}

    //public async Task<bool> DeleteCustomer(string email)
    //{
    //    try
    //    {

    //        if (!await ControlCustomerExist(email))
    //        {

    //            Console.WriteLine("Customer not found");
    //            return false;
    //        }
    //        var customerToDelete = await _customerRepository.GetOneAsync(x => x.Email == email);
    //        if (customerToDelete != null)
    //        {
    //            var profileToDelete = await _profileRepository.GetOneAsync(x => x.CustomerId == customerToDelete.Id);
    //            if (profileToDelete != null)
    //            {
    //                var profileAddressToDelete = await _profileAddressRepository.GetOneAsync(x => x.ProfileId == profileToDelete.Id);
    //                if (profileAddressToDelete != null)
    //                    await _profileAddressRepository.DeleteAsync(profileAddressToDelete);
    //                await _profileRepository.DeleteAsync(profileToDelete);
    //                await _customerRepository.DeleteAsync(customerToDelete);

    //                Console.WriteLine("CustomerDeleted");
    //                return true;
    //            }
    //        }
    //        return false;
    //    }

    //    catch (Exception ex)
    //    {
    //        Debug.WriteLine("Error DeleteCustomer:: " + ex.Message);
    //        return false;
    //    }
    //}

    //public async Task<CustomerDto> FindCustomer(string email)
    //{
    //    try
    //    {
    //        var foundCustomer = await _customerRepository.GetOneAsync(x => x.Email == email);
    //        if (foundCustomer != null)
    //        {
    //            var foundCustomerType = await _customerTypeRepository.GetOneAsync(x => x.Id == foundCustomer.CustomerTypeId);
    //            if (foundCustomerType != null)
    //            {
    //                var foundProfile = await _profileRepository.GetOneAsync(x => x.CustomerId == foundCustomer.Id);
    //                if (foundProfile != null)
    //                {
    //                    var foundProfileAddress = await _profileAddressRepository.GetOneAsync(x => x.ProfileId == foundProfile.Id);
    //                    if (foundProfileAddress != null)
    //                    {
    //                        var foundAddress = await _addressRepository.GetOneAsync(x => x.Id == foundProfileAddress.AddressId);
    //                        if (foundAddress != null)
    //                        {
    //                            var foundCustomerDto = new CustomerDto
    //                            {
    //                                Email = foundCustomer.Email,
    //                                FirstName = foundProfile.FirstName,
    //                                LastName = foundProfile.LastName,
    //                                StreetName = foundAddress.StreetName,
    //                                PostalCode = foundAddress.PostalCode,
    //                                City = foundAddress.City,
    //                                CustomerType = foundCustomerType.TypeOfCustomer
    //                            };
    //                            return foundCustomerDto;
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //        return null!;
    //    }
    //    catch (Exception ex)
    //    {
    //        Debug.WriteLine("Error:: FindCustomer" + ex.Message);
    //        return null!;
    //    }
    //}
    //public async Task<bool> UpdateCustomer(CustomerDto customerDto)
    //{
    //    try
    //    {
    //        var customerDtoToUpdate = await FindCustomer(customerDto.Email);

    //        if (customerDtoToUpdate != null)
    //        {
    //            var customerToUpdate = await _customerRepository.GetOneAsync(x => x.Email == customerDto.Email);
    //            await _customerRepository.UpdateCustomerAsync(customerToUpdate);

    //            var customerTypeToUpdate = await _customerTypeRepository.GetOneAsync(x => x.Id == customerToUpdate.CustomerTypeId);
    //            if (customerTypeToUpdate != null)
    //            {
    //                customerTypeToUpdate.TypeOfCustomer = customerDto.CustomerType;
    //                await _customerTypeRepository.CreateCustomerTypeAsync(customerDto.CustomerType);

    //                var profileToUpdate = await _profileRepository.GetOneAsync(x => x.CustomerId == customerToUpdate.Id);
    //                if (profileToUpdate != null)
    //                {
    //                    profileToUpdate.FirstName = customerDto.FirstName;
    //                    profileToUpdate.LastName = customerDto.LastName;

    //                    await _profileRepository.UpdateProfileAsync(profileToUpdate);


    //                    var profileAddressToUpdate = await _profileAddressRepository.GetOneAsync(x => x.ProfileId == profileToUpdate.Id);
    //                    if (profileAddressToUpdate != null)
    //                    {
    //                        await _profileAddressRepository.UpdateProfileAddressAsync(profileAddressToUpdate);

    //                        var addressToUpdate = await _addressRepository.GetOneAsync(x => x.Id == profileAddressToUpdate.AddressId);
    //                        if (addressToUpdate != null)
    //                        {
    //                            if (addressToUpdate.StreetName == customerDto.StreetName && addressToUpdate.PostalCode == customerDto.PostalCode && addressToUpdate.City == customerDto.City)
    //                            {
    //                                await _addressRepository.UpdateAddressAsync(addressToUpdate);
    //                            }

    //                            else
    //                            {
    //                               var deleteProfileAddress= await _profileAddressRepository.DeleteProfileAdressEntityAsync(profileAddressToUpdate);

    //                                var newAddressFromUpdate = await _addressRepository.CreateAddressAsync(customerDto.StreetName, customerDto.PostalCode, customerDto.City);
    //                                if (newAddressFromUpdate != null)
    //                                {
    //                                    Console.WriteLine("new AdressEntity created");
                                        

    //                                    if(profileAddressToUpdate != null)
    //                                    {
    //                                        profileAddressToUpdate.AddressId = newAddressFromUpdate.Id;
    //                                        profileAddressToUpdate.ProfileId = profileToUpdate.Id;
    //                                        await _profileAddressRepository.UpdateProfileAddressAsync(profileAddressToUpdate);
    //                                    }
    //                                }

    //                            }

    //                        }


    //                    }
    //                }


    //            }
    //            return true;
    //        }



    //        else
    //        {
    //            return false;
    //        }
    //    }

    //    catch (Exception ex)
    //    {
    //        Debug.WriteLine("ERROR :: UpdateCustomer " + ex.Message);
    //        return false;

    //    }
    //}
}








    
        
    


