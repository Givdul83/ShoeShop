using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class ProfileAddressEntity
{ 
    public int ProfileId { get; set; }

    public virtual ProfileEntity Profile { get; set; } = null!;

    public int AddressId { get; set; }

    public virtual AddressEntity Address { get; set; } = null!;

    public virtual ICollection<AddressEntity> Addresses { get; set; } = new List<AddressEntity>();
}
