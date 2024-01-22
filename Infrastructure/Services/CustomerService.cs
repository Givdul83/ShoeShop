
using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Repositories;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Infrastructure.Services;

public class CustomerService(AddressRepository addressRepository, ProfileRepository profileRepository, CustomerRepository customerRepository, CustomerTypeRepository customerTypeRepository, ProfileAddressRepository profileAddressRepository)
{
    private readonly AddressRepository _addressRepository = addressRepository;
    private readonly ProfileRepository _profileRepository = profileRepository;
    private readonly CustomerRepository _customerRepository = customerRepository;
    private readonly CustomerTypeRepository _customerTypeRepository = customerTypeRepository;
    private readonly ProfileAddressRepository _profileAddressRepository = profileAddressRepository;

    public async Task<bool> CreateCustomerDto(NewCustomerDto customerDto)
    {
        try
        {
            if (!await ControlUserExist(customerDto.Email))
            {
                var customerTypeEntity = await _customerTypeRepository.CreateAsync(new CustomerTypeEntity
                {
                    TypeOfCustomer = customerDto.CustomerType
                });

                var customerEntity = await _customerRepository.CreateAsync(new CustomerEntity
                {
                    Id = Guid.NewGuid(),
                    Created = DateTime.Now,
                    Email = customerDto.Email,
                    CustomerTypeId = customerTypeEntity.Id
                });



                var profileEntity = await _profileRepository.CreateAsync(new ProfileEntity
                {
                    FirstName = customerDto.FirstName,
                    LastName = customerDto.LastName,
                    CustomerId = customerEntity.Id
                });

                var addressEntity = await _addressRepository.CreateAsync(new AddressEntity
                {
                    StreetName = customerDto.StreetName,
                    City = customerDto.City,
                    PostalCode = customerDto.PostalCode
                });

                
                var profileAddress = await _profileAddressRepository.CreateAsync(new ProfileAddressEntity
                {
                    AddressId = addressEntity.Id,
                    ProfileId = profileEntity.Id
                });

                Console.WriteLine("sparat");
                return true;
            }
            else
            {
                Console.WriteLine("GIck ej");
                return false;
            }

        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR:: CreateCustomerDto" + ex.Message);
             return false;
        }
    }
    public async Task<bool> ControlUserExist(string email)
    {
        if (await _customerRepository.ExistAsync(x => x.Email == email))
        {
            Console.WriteLine($"User with {email} is already created", "ControlUSerExist");
            return true;
        }

        return false;

    }


}


    //public bool AddCustomer(CustomerDto customer)
    //{


    //    if (!_customerRepository.Exist(x => x.Email == customer.Email))
    //    {
    //        var customerEntity = _customerRepository.GetOne(x => x.Email == customer.Email);
    //        customerEntity ??= _customerRepository.Create(new CustomerEntity
    //        {
    //            Email = customer.Email
    //        });




    //        var profileEntity = _profileRepository.GetOne(x => x.CustomerId == customerEntity.Id);
    //        profileEntity ??= _profileRepository.Create(new ProfileEntity
    //        {
    //            FirstName = customer.FirstName,
    //            LastName = customer.LastName,
    //            CustomerId = customerEntity.Id
    //        });

    //        var addressEntity = _addressRepository.GetOne(x => x.StreetName== customer.StreetName && x.PostalCode == customer.PostalCode && x.City == customer.City);
    //        addressEntity ??=  _addressRepository.Create(new AddressEntity
    //        {
    //            StreetName = customer.StreetName,
    //            City = customer.City,
    //            PostalCode = customer.PostalCode,
    //        });

    //        var customerTypeEntity = _customerTypeRepository.GetOne(x => x.TypeOfCustomer == customer.CustomerType);
    //           customerTypeEntity ??=_customerTypeRepository.Create(new CustomerTypeEntity
    //        {
    //            TypeOfCustomer = customer.CustomerType
    //        });

    //        var profileAdressEntity = _profileAddressRepository.Create(new ProfileAddressEntity
    //        {
    //            AddressId = addressEntity.Id,
    //            ProfileId = profileEntity.Id
    //        });

    //        return true;
    //    }
               
      //else { return false; }    
    

      //  }



        //var newCustomer = new CustomerDto();

        //newCustomer.FirstName = _customer.FirstName;
        //newCustomer.LastName = _customer.LastName; 
        //newCustomer.Email = _customer.Email;
        //newCustomer.CustomerType = _customer.CustomerType;
        //newCustomer.StreetName = _customer.StreetName;
        //newCustomer.PostalCode = _customer.PostalCode;
        //newCustomer.City = _customer.City;

        //var customer = new CustomerEntity();

        //customer.Email = _customer.Email;
        //customer.Created = DateTime.Now;
        //customer.Id = new Guid();

        //_customerRepository.Create(customer);

        //var profile = new ProfileEntity();

        //profile.FirstName = _customer.FirstName;
        //profile.LastName = _customer.LastName;  
     
        //_profileRepository.Create(profile);

        //var customerType= new CustomerTypeEntity();

        //customerType.TypeOfCustomer = _customer.CustomerType;

        //_customerTypeRepository.Create(customerType);

        //var address = new AddressEntity();

        //address.StreetName = _customer.StreetName;
        //address.PostalCode = _customer.PostalCode;
        //address.City = _customer.City;

        //_addressRepository.Create(address);

        
    


