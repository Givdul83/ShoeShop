using Infrastructure.Dtos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Infrastructure.Repositories;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Identity.Client;

namespace Infrastructure.Entities;

public class CustomerEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string Email { get; set; } = null!;

    [Column(TypeName ="datetime2")] 
    public DateTime Created {  get; set; } = DateTime.Now;

    [ForeignKey("CustumerType")]
    public int CustomerTypeId { get; set; }

    public virtual CustomerTypeEntity CustomerType { get; set; } = null!; 

    public virtual ProfileEntity Profile { get; set; } = null!;

    
    //public static implicit operator CustomerEntity(UserRegDto dto)
    //{
        
    
    //    var customerEntity = new CustomerEntity
    //    {
    //        Email = dto.Email,
    //        Created = DateTime.Now,
    //        Id = new Guid(),
    //    };

       
    //    return customerEntity;
    //}
}
