using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class CustomerTypeEntity
{
    [Key]
    public int Id { get; set; }
     
    [Required]
    [Column(TypeName = "nvarchar(30)")]
    public string TypeOfCustomer { get; set; } = null!;
}