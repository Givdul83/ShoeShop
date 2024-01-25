using Infrastructure.Dtos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class CustomerEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string Email { get; set; } = null!;

    [Column(TypeName ="datetime2")] 
    public DateTime Created {  get; set; } 

    
    public int CustomerTypeId { get; set; }

    public virtual CustomerTypeEntity CustomerType { get; set; } = null!; 

    public virtual ProfileEntity Profile { get; set; } = null!;
   

    //public static implicit operator CustomerEntity(CustomerDtoReg dto)
    //{
    //    var customerEntity = new CustomerEntity
    //    {
    //        Email = dto.Email,
    //        Created = DateTime.Now,
    //        Id = new Guid(),
    //        CustomerTypeId= CustomerTypeEntity.Id
    //        };
    //    return customerEntity;
    //}
}
