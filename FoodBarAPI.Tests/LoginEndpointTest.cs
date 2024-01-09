using System.Net;
using FluentAssertions;
using Xunit;
using FoodBarAPI.Tests.Tokens;

namespace FoodBarAPI.Tests;

public class LoginEndpointTest
{
    [Fact]
    public async Task HttpPost_GetAdminToken()
    {
        var token = TokenFactory.CreateToken(TokenType.Admin);
        await token.Get();
    
        token.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task HttpPost_GetUserToken()
    {
        var token = TokenFactory.CreateToken(TokenType.User);
        await token.Get();
    
        token.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}