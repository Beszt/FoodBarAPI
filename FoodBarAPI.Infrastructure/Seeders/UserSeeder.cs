using Microsoft.AspNetCore.Identity;
using FoodBarAPI.Domain.Entities;
using FoodBarAPI.Infrastructure.Persistence;

namespace FoodBarAPI.Infrastructure.Seeders;

public class UserSeeder(FoodBarDbContext _dbContext) : ISeeder
{
    public async Task Seed()
    {
        if (await _dbContext.Database.CanConnectAsync() && !_dbContext.Roles.Any())
        {
            var roles = new List<Role>
            {
                new() {
                    Id = 1,
                    Name = "admin"
                },
                new() {
                    Id = 2,
                    Name = "user"
                }
            };

            var passwordHasher = new PasswordHasher<User>();

            var admin = new User() {
                Login = "admin",
                RoleId = 1,
            };
            admin.Password = passwordHasher.HashPassword(admin, "admin"); // TP-Link tribute

            var user = new User() {
                Login = "user",
                RoleId = 2,
            };
            user.Password = passwordHasher.HashPassword(user, "12345");

            await _dbContext.Roles.AddRangeAsync(roles);
            await _dbContext.Users.AddAsync(admin);
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}