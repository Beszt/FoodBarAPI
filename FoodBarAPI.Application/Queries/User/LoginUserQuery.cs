using MediatR;

namespace FoodBarAPI.Application.Queries;

public class LoginUserQuery(string login, string password, string jwtKey, string jwtIssuer, int jwtExpire) : IRequest<string?>
{
    public string Login { get; set; } = login;
    public string Password { get; set; } = password;
    public string JwtKey { get; set; } = jwtKey;
    public string JwtIssuer { get; set; } = jwtIssuer;
    public int JwtExpire { get; set; } = jwtExpire;
}