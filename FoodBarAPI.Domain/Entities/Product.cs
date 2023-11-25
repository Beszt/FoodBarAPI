namespace FoodBarAPI.Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public string Barcode { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public byte[]? Image { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public ProductDetails ProductDetails { get; set; } = default!;
}