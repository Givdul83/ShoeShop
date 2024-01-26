
using Infrastructure.Entities;

namespace Infrastructure.Dtos;

public class UserDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string StreetName { get; set; } = null!;
    public string City { get; set; } = null!;

    public string PostalCode = null!;

    public string TypeOfCustomer { get; set; } = null!;

    public DateTime? Created { get; set; }


    public static  UserDto FromEntities(CustomerEntity customerEntity, ProfileEntity profileEntity, AddressEntity addressEntity, CustomerTypeEntity customerTypeEntity)
    {
        var newCustomerDto=  new UserDto
        {
            FirstName = profileEntity.FirstName,
            LastName = profileEntity.LastName,
            Email = customerEntity.Email,
            StreetName = addressEntity.StreetName,
            PostalCode = addressEntity.PostalCode,
            City = addressEntity.City,
            TypeOfCustomer = customerTypeEntity.TypeOfCustomer,
            Created = customerEntity.Created,
        };
        return newCustomerDto;
    }
}
