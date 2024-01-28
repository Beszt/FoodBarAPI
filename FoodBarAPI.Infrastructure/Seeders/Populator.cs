using FoodBarAPI.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoodBarAPI.Infrastructure.Seeders;

public class Populator(FoodBarDbContext _dbContext,ProductSeeder _productSeeder, UserSeeder _userSeeder) : IPopulator
{
    public async Task Populate()
    {
        if (_dbContext.Database.CanConnect())
        {
            var pendingMigrations = _dbContext.Database.GetPendingMigrations();
            if (pendingMigrations != null && pendingMigrations.Any())
                _dbContext.Database.Migrate();

            await _userSeeder.Seed();
            await _productSeeder.Seed();
        }
    }
}