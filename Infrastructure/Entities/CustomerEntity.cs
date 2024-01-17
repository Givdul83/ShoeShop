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
    public DateTime Created {  get; set; } = DateTime.Now;

    [Required]
    [ForeignKey(nameof(CustomerTypeEntity))]
    public int CustomerTypeId { get; set; }

    public virtual ProfileEntity Profile { get; set; } = null!;
}
