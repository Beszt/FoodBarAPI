using Microsoft.EntityFrameworkCore;
using FoodBarAPI.Domain.Entities;

namespace FoodBarAPI.Infrastructure.Persistence;

public class ProductDbContext(DbContextOptions<ProductDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; } = default!;
    public DbSet<ProductDetails> ProductsDetails { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>()
            .HasOne(c => c.ProductDetails)
            .WithOne(c => c.Product);

        modelBuilder.Entity<ProductDetails>()
            .HasOne(c => c.Product)
            .WithOne(c => c.ProductDetails)
            .HasForeignKey<ProductDetails>(c => c.ProductId);
    }
}