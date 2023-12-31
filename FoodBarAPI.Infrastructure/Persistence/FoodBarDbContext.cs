using Microsoft.EntityFrameworkCore;
using FoodBarAPI.Domain.Entities;

namespace FoodBarAPI.Infrastructure.Persistence;

public class FoodBarDbContext(DbContextOptions<FoodBarDbContext> _options) : DbContext(_options)
{
    public DbSet<Product> Products { get; set; } = default!;
    public DbSet<ProductDetails> ProductsDetails { get; set; } = default!;
    public DbSet<User> Users { get; set; } = default!;
    public DbSet<Role> Roles { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>()
            .HasIndex(c => c.Barcode)
            .IsUnique();

        modelBuilder.Entity<Product>()
            .HasOne(c => c.ProductDetails)
            .WithOne(c => c.Product);

        modelBuilder.Entity<Product>()
            .HasOne(c => c.User)
            .WithMany(c => c.Product)
            .HasForeignKey(c => c.CreatedBy);

        modelBuilder.Entity<ProductDetails>()
            .HasOne(c => c.Product)
            .WithOne(c => c.ProductDetails)
            .HasForeignKey<ProductDetails>(c => c.ProductId);

        modelBuilder.Entity<User>()
            .HasIndex(c => c.Login)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasOne(c => c.Role)
            .WithMany(c => c.Users)
            .HasForeignKey(c => c.RoleId);

        modelBuilder.Entity<Role>()
            .HasMany(c => c.Users)
            .WithOne(c => c.Role);
    }
}