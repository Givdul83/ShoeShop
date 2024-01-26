using Infrastructure.Dtos;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class AddressEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string StreetName { get; set; } = null!;

    [Required]
    [Column(TypeName ="varchar(6)")]
    public string PostalCode { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string City { get; set; } = null!;

    public virtual ICollection<ProfileAddressEntity> ProfileAddresses { get; set; }= new List<ProfileAddressEntity>();

    public static implicit operator AddressEntity(UserRegDto dtoReg)
    {
        var addressEntity = new AddressEntity
        {
            StreetName = dtoReg.StreetName,
            PostalCode = dtoReg.PostalCode,
            City = dtoReg.City,
        };
        return addressEntity;
    }
}
