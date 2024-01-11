using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using Xunit;
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
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();

        HttpRequestMessage httpRequest = new()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"/user/{_login}")
        };

        var response = await client.SendAsync(httpRequest);
        var statusCode = response.StatusCode;

        statusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact, TestPriority(1)]
    public async Task HttpGet_ShouldReturnForbiddenForUserRole()
    {
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();
        var token = await TokenFactory.CreateToken(TokenType.User).Get();

        HttpRequestMessage httpRequest = new()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"/user/{_login}")
        };
        httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.SendAsync(httpRequest);
        var statusCode = response.StatusCode;

        string result = await response.Content.ReadAsStringAsync();

        statusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact, TestPriority(2)]
    public async Task HttpPost_ShouldCreateUser()
    {
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();
        var token = await TokenFactory.CreateToken(TokenType.Admin).Get();

        HttpRequestMessage httpRequest = new()
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("/user"),
            Content = new StringContent(_userJson, Encoding.UTF8, new MediaTypeHeaderValue("application/json"))
        };
        httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.SendAsync(httpRequest);
        var statusCode = response.StatusCode;

        statusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact, TestPriority(3)]
    public async Task HttpGet_ShouldReturnUser()
    {
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();
        var token = await TokenFactory.CreateToken(TokenType.Admin).Get();

        HttpRequestMessage httpRequest = new()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"/user/{_login}")
        };
        httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.SendAsync(httpRequest);
        var statusCode = response.StatusCode;

        string result = await response.Content.ReadAsStringAsync();

        statusCode.Should().Be(HttpStatusCode.OK);
        result.Should().Be(_userJson.Replace("kuciapka", "HIDDEN"));
    }

    [Fact, TestPriority(4)]
    public async Task HttpDelete_ShouldDeleteUser()
    {
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();
        var token = await TokenFactory.CreateToken(TokenType.Admin).Get();

        HttpRequestMessage httpRequest = new()
        {
            Method = HttpMethod.Delete,
            RequestUri = new Uri($"/user/{_login}")
        };
        httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.SendAsync(httpRequest);
        var statusCode = response.StatusCode;

        statusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact, TestPriority(5)]
    public async Task HttpGet_ShouldReturnNotFound()
    {
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();
        var token = await TokenFactory.CreateToken(TokenType.Admin).Get();

        HttpRequestMessage httpRequest = new()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"/user/{_login}")
        };
        httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.SendAsync(httpRequest);
        var statusCode = response.StatusCode;

        statusCode.Should().Be(HttpStatusCode.NotFound);
    }
}