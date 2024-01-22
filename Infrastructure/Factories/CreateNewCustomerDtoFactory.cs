
using Infrastructure.Dtos;
using System.Diagnostics;

namespace Infrastructure.Factories;

public static class CreateNewCustomerDtoFactory
{
    public static NewCustomerDto Create(string firstname, string lastName, string email, string streetName, string postalCode, string city, string customerType)
    {
        try
        {
            var customerDto = new NewCustomerDto
            {
                FirstName = firstname,
                LastName = lastName,
                Email = email,
                StreetName = streetName,
                PostalCode = postalCode,
                City = city,
                CustomerType = customerType

            };
            return customerDto;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR CustomerDtoFactory" + ex.Message);
        }
        return null!;
    }

}
