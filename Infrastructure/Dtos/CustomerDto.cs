
using Infrastructure.Entities;

namespace Infrastructure.Dtos;

public class CustomerDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string StreetName { get; set; } = null!;
    public string City { get; set; } = null!;

    public string PostalCode = null!;

    public string CustomerType { get; set; } = null!;

    public DateTime? Created { get; set; }


    public static  CustomerDto FromEntities(CustomerEntity customerEntity, ProfileEntity profileEntity, AddressEntity addressEntity, CustomerTypeEntity customerTypeEntity)
    {
        var newCustomerDto=  new CustomerDto
        {
            FirstName = profileEntity.FirstName,
            LastName = profileEntity.LastName,
            Email = customerEntity.Email,
            StreetName = addressEntity.StreetName,
            PostalCode = addressEntity.PostalCode,
            City = addressEntity.City,
            CustomerType = customerTypeEntity.TypeOfCustomer,
            Created = customerEntity.Created,
        };
        return newCustomerDto;
    }
}
