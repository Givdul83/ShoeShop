
namespace Infrastructure.Dtos;

public class UserRegDto
{

    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public string Email {  get; set; } = null!;

    public string StreetName { get; set; } = null!;
    public string City { get; set; } = null!;

    public string PostalCode = null!;

    public string TypeOfCustomer { get; set; } = null!;

    }

