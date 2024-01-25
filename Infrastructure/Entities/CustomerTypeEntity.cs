using Infrastructure.Dtos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Infrastructure.Entities;

public class CustomerTypeEntity
{
    [Key]
    public int Id { get; set; }
     
    [Required]
    [Column(TypeName = "nvarchar(30)")]
    public string TypeOfCustomer { get; set; } = null!;

    public virtual ICollection<CustomerEntity> Customers { get; set; } = new List<CustomerEntity>();


    public static implicit operator CustomerTypeEntity(CustomerDtoReg dtoReg)
    {
        var customerTypeEntity= new CustomerTypeEntity
        {
            TypeOfCustomer = dtoReg.CustomerType
        };
        return customerTypeEntity;
        }

}