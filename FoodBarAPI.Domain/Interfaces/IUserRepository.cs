using FoodBarAPI.Domain.Entities;

namespace FoodBarAPI.Domain.Interfaces;

public interface IUserRepository
{
    Task Create(User user);
    Task<User?> Get(string login);
    Task Update(User user);
    Task Delete(string login);

    bool Exists(string login);
    int GetRoleIdByRoleName(string name);
    string? GetRoleName(string login);
    bool HasAdminRole(int id);
}