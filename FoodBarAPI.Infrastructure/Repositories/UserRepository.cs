using Microsoft.EntityFrameworkCore;
using FoodBarAPI.Domain.Entities;
using FoodBarAPI.Domain.Interfaces;
using FoodBarAPI.Infrastructure.Persistence;

namespace FoodBarAPI.Infrastructure.Repositories;

public class UserRepository(FoodBarDbContext _dbContext) : IUserRepository
{
    public async Task<User?> Get(string login)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Login == login);
        if (user != null)
        {
            var role = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Id == user.RoleId);
            if (role != null)
                user.Role = role;
        }
 
        return user;
    }
}