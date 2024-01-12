using FluentAssertions;
using Xunit;
using FoodBarAPI.Tests.Tokens;

namespace FoodBarAPI.Tests;

public class LoginEndpointTest
{
    readonly string _bearerHeader = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9";

    [Fact]
    public async Task HttpPost_GetAdminToken()
    {
        var token = await TokenFactory.CreateToken(TokenType.Admin);
    
        token.Bearer.Should().Contain(_bearerHeader);
    }

    [Fact]
    public async Task HttpPost_GetUserToken()
    {
        var token = await TokenFactory.CreateToken(TokenType.User);
    
        token.Bearer.Should().Contain(_bearerHeader);
    }
}