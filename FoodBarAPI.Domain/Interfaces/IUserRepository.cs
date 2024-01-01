using FoodBarAPI.Domain.Entities;

namespace FoodBarAPI.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> Get(string name);
}