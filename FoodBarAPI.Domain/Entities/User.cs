namespace FoodBarAPI.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Login { get; set; } = default!;
    public string Password { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public int RoleId {get; set; }
    public Role Role { get; set; } = default!;
}