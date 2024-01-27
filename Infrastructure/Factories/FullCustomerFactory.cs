
using Infrastructure.Dtos;
using Infrastructure.Entities;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace Infrastructure.Factories;

public static class FullCustomerFactory
{
    public static UserRegDto Create(UserRegDto newCustomerDto)
    {
        try
        {
            var customerEntity = CustomerFactory.CreateCustomerEntity(newCustomerDto.Email);
            var profileEntity = ProfileFactory.CreateProfileEntity(newCustomerDto.FirstName, newCustomerDto.LastName);
            var addressEntity = AddressFactory.CreateAddressEntity(newCustomerDto.StreetName, newCustomerDto.PostalCode, newCustomerDto.City);
            var customerTypeEntity = CustomerTypeFactory.CreateCustomerType(newCustomerDto.TypeOfCustomer);


            UserRegDto createdCustomerDto = new UserRegDto
            {
                Email = customerEntity.Email,
                FirstName = profileEntity.FirstName,
                LastName = profileEntity.LastName,
                StreetName = addressEntity.StreetName,
                PostalCode = addressEntity.PostalCode,
                City = addressEntity.City,
               TypeOfCustomer = customerTypeEntity.TypeOfCustomer
            };
            return createdCustomerDto;
        }
        catch
           (Exception ex)
                {
            Debug.WriteLine("ERROR PROFILEFACTORY: : " + ex.Message);
            }
        return null!;
     }
}

