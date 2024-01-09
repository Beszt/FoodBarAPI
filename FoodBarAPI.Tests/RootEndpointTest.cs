using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using Xunit;

namespace FoodBarAPI.Tests;

public class RootEndpointTest
{
    [Fact]
    public async Task HttpGet_ShouldReturnStatusCodeNotFound()
    {
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();

        var response = await client.GetAsync("/");
        var statusCode = response.StatusCode;
    
        statusCode.Should().Be(HttpStatusCode.NotFound);
    }
}