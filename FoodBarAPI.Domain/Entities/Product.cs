namespace FoodBarAPI.Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public long Barcode { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public byte[]? Image { get; set; }
    public int CreatedBy {get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int UpdatedBy {get; set; }
    public DateTime UpdatedAt { get; set; }

    public ProductDetails ProductDetails { get; set; } = default!;
    public User User {get; set; } = default!;
}