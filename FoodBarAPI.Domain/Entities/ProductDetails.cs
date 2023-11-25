namespace FoodBarAPI.Domain.Entities;

public class ProductDetails
{
    public int Id { get; set; }
    public int Weight { get; set; }
    public int Energy { get; set; }
    public double Protein { get; set; }
    public double Fat { get; set; }
    public double Carbohydrates { get; set; }
    public double? Sugar { get; set; }
    public double? Salt { get; set; }
    public double? Fiber { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; } = default!;
}