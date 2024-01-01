using FoodBarAPI.Domain.Entities;
using FoodBarAPI.Infrastructure.Persistence;

namespace FoodBarAPI.Infrastructure.Seeders;

public class ProductSeeder(FoodBarDbContext _dbContext) : ISeeder
{
    public async Task Seed()
    {
        if (await _dbContext.Database.CanConnectAsync() && !_dbContext.Products.Any())
        {
            var products = new List<Product>
            {
                new() {
                    Barcode = 4056489301806,
                    Name = "Twaróg Wędzony z Wielkopolski",
                    Description = "Twaróg zajebisty - wpierdalaj!",
                    ProductDetails = new()
                    {
                        Weight = 275,
                        Energy = 217,
                        Protein = 13.0,
                        Fat = 17.0,
                        Carbohydrates = 3.0,
                        Sugar = 3.0,
                        Salt = 1.5,
                        Fiber = 0.0,
                    }
                },
                new() {
                    Barcode = 4056489282631,
                    Name = "Polędwica z piersi kurczaka",
                    Description = "Dobra szynka, polecam!",
                    ProductDetails = new()
                    {
                        Weight = 250,
                        Energy = 108,
                        Protein = 16.0,
                        Fat = 4.0,
                        Carbohydrates = 2.0,
                        Sugar = 0.6,
                        Salt = 2.2,
                        Fiber = 0.0,
                    }
                }
            };

            await _dbContext.Products.AddRangeAsync(products);
            await _dbContext.SaveChangesAsync();
        }
    }
}