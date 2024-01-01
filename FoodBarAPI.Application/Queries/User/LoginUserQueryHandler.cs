using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MediatR;
using FoodBarAPI.Domain.Interfaces;
using FoodBarAPI.Domain.Entities;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace FoodBarAPI.Application.Queries;

public class LoginUserQueryHandler(IUserRepository _userRepository) : IRequestHandler<LoginUserQuery, string?>
{
    public async Task<string?> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Get(request.Login);
        
        if (user == null)
            return null;

        var passwordHasher = new PasswordHasher<User>();

        if (passwordHasher.VerifyHashedPassword(user, user.Password, request.Password) == PasswordVerificationResult.Failed)
            return null;
        
        var claims = new List<Claim>()
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Login),
            new(ClaimTypes.Role, user.Role.Name)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(request.JwtKey));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(request.JwtExpire);

        var token = new JwtSecurityToken(
            request.JwtIssuer,
            request.JwtIssuer,
            claims,
            expires: expires,
            signingCredentials: cred
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }
}