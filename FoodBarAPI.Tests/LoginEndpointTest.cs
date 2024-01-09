using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using Xunit;
using System.Text;
using System.Net.Http.Headers;
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