using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class ProfileEntity
{

    [Key]
    public int Id { get; set; }

    [Required]
    [ForeignKey(nameof(CustomerEntity))]
    public Guid CustomerId { get; set; }
    public virtual CustomerEntity Customer { get; set; } = null!;


    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string FirstName { get; set; }= null!;

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string LastName { get; set; } = null!;

   


}
