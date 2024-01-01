namespace FoodBarAPI.Infrastructure.Seeders;

public class Populator(ProductSeeder _productSeeder, UserSeeder _userSeeder) : IPopulator
{
    public async Task Populate()
    {
        await _productSeeder.Seed();
        await _userSeeder.Seed();
    }
}