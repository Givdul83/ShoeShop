using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class ProfileAddressEntity
{
    
    public int ProfileId { get; set; }

    public ProfileEntity Profile { get; set; } = null!;


   
    public int AddressId { get; set; }

    public AddressEntity Address { get; set; } = null!;
   
}
