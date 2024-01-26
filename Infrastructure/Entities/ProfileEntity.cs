using Infrastructure.Dtos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class ProfileEntity
{

    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string FirstName { get; set; }= null!;

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string LastName { get; set; } = null!;
    
    public Guid CustomerId { get; set; }
    public CustomerEntity Customer { get; set; } = null!;

    public virtual ICollection<ProfileAddressEntity> ProfileAddresses { get; set; } = new List<ProfileAddressEntity>();

    public static implicit operator ProfileEntity(UserRegDto dtoReg)
    {
        var profileEntity= new ProfileEntity
        {
            FirstName = dtoReg.FirstName,
            LastName = dtoReg.LastName,
        };
        return profileEntity;
    }

}
