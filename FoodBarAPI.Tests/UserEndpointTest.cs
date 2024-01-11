using System.Net;
using System.Net.Http.Headers;
using System.Text;
using FluentAssertions;
using Xunit;
using FoodBarAPI.Tests.Rest;
using FoodBarAPI.Tests.Tokens;

namespace FoodBarAPI.Tests;

[TestCaseOrderer("FoodBarAPI.Tests.Orderers.PriorityOrderer", "FoodBarAPI.Tests")]
public class UserEndpointTest
{
    private readonly static string _login = "krzychu";
    private readonly string _userJson = "{\"login\":\"" + _login + "\",\"password\":\"kuciapka\",\"role\":\"user\"}";

    [Fact, TestPriority(1)]
    public async Task HttpGet_ShouldReturnUnauthorizedForAnonymous()
    {
        HttpRequestMessage httpRequest = new()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"/user/{_login}")
        };
        
        var response = await Client.Send(httpRequest);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact, TestPriority(1)]
    public async Task HttpGet_ShouldReturnForbiddenForUserRole()
    {
        HttpRequestMessage httpRequest = new()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"/user/{_login}")
        };
        
        var response = await Client.Send(httpRequest, TokenType.User);

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact, TestPriority(2)]
    public async Task HttpPost_ShouldCreateUser()
    {
        HttpRequestMessage httpRequest = new()
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("/user"),
            Content = new StringContent(_userJson, Encoding.UTF8, new MediaTypeHeaderValue("application/json"))
        };
        
        var response = await Client.Send(httpRequest, TokenType.Admin);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact, TestPriority(3)]
    public async Task HttpGet_ShouldReturnUser()
    {
        HttpRequestMessage httpRequest = new()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"/user/{_login}")
        };
        
        var response = await Client.Send(httpRequest, TokenType.Admin);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Body.Should().Be(_userJson.Replace("kuciapka", "HIDDEN"));
    }

    [Fact, TestPriority(4)]
    public async Task HttpDelete_ShouldDeleteUser()
    {
        HttpRequestMessage httpRequest = new()
        {
            Method = HttpMethod.Delete,
            RequestUri = new Uri($"/user/{_login}")
        };
        
        var response = await Client.Send(httpRequest, TokenType.Admin);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact, TestPriority(5)]
    public async Task HttpGet_ShouldReturnNotFound()
    {
        HttpRequestMessage httpRequest = new()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"/user/{_login}")
        };
        
        var response = await Client.Send(httpRequest, TokenType.Admin);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}