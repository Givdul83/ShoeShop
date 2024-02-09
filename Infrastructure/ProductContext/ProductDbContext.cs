using System;
using System.Collections.Generic;
using Infrastructure.ProductEntities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.ProductContext;

public partial class ProductDbContext : DbContext
{
    public ProductDbContext()
    {
    }

    public ProductDbContext(DbContextOptions<ProductDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Price> Prices { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-PTC1NT1;Initial Catalog=ProductCatalog;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Images__3214EC07FC3EFB8D");

            entity.HasIndex(e => e.ImageUrl, "UQ__Images__372DE2C562DDA470").IsUnique();

            entity.Property(e => e.ImageUrl).HasColumnName("ImageURL");
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Manufact__3214EC0785C3CAD2");

            entity.HasIndex(e => e.Manufacturer1, "UQ__Manufact__D194335A987B254A").IsUnique();

            entity.Property(e => e.Manufacturer1)
                .HasMaxLength(50)
                .HasColumnName("Manufacturer");
        });

        modelBuilder.Entity<Price>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Prices__3214EC076CB2B187");

            entity.HasIndex(e => e.Price1, "UQ__Prices__6089BD09E43A688C").IsUnique();

            entity.Property(e => e.Price1)
                .HasColumnType("money")
                .HasColumnName("Price");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Products__3214EC0716BA048C");

            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.Manufacturer).WithMany(p => p.Products)
                .HasForeignKey(d => d.ManufacturerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Products__Manufa__7D439ABD");

            entity.HasOne(d => d.Price).WithMany(p => p.Products)
                .HasForeignKey(d => d.PriceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Products__PriceI__7E37BEF6");

            entity.HasMany(d => d.Images).WithMany(p => p.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductImage",
                    r => r.HasOne<Image>().WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ProductIm__Image__02084FDA"),
                    l => l.HasOne<Product>().WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ProductIm__Produ__01142BA1"),
                    j =>
                    {
                        j.HasKey("ProductId", "ImageId").HasName("PK__ProductI__635DA9BD1E85E79A");
                        j.ToTable("ProductImages");
                    });
        });

       

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
