using System.Net;
using FluentAssertions;
using Xunit;
using FoodBarAPI.Tests.Rest;

namespace FoodBarAPI.Tests;

public class RootEndpointTest
{
    [Fact]
    public async Task HttpGet_ShouldReturnStatusCodeNotFound()
    {
        HttpRequestMessage httpRequest = new()
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("/")
        };

        var response = await Client.Send(httpRequest);
    
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}