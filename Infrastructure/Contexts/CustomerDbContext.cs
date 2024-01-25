
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public partial class CustomerDbContext : DbContext
{

    public CustomerDbContext()
    {

    }
    public CustomerDbContext(DbContextOptions options) : base(options)
    { 
    }

    public virtual DbSet<ProfileEntity> Profiles { get; set; }

    public virtual DbSet<AddressEntity> Addresses { get; set; }

    public virtual DbSet<CustomerEntity> Customers { get; set; }

    public virtual DbSet<CustomerTypeEntity> CustomersTypes { get; set; }

    public virtual DbSet<ProfileAddressEntity> ProfileAddresses { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerEntity>()
            .HasIndex(x => x.Email)
            .IsUnique();

        modelBuilder.Entity<AddressEntity>()
            .HasIndex(x => new { x.StreetName, x.PostalCode, x.City })
            .IsUnique();

        modelBuilder.Entity<ProfileAddressEntity>()
            .HasKey(x => new { x.AddressId, x.ProfileId });

        
            
    }
}









 