using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using Xunit;
using FoodBarAPI.Tests.Tokens;

namespace FoodBarAPI.Tests;

[TestCaseOrderer("FoodBarAPI.Tests.Orderers.PriorityOrderer", "FoodBarAPI.Tests")]
public class ProductEndpointTest
{
    private readonly static string _productBarcode = "5900617041661";
    private readonly string _productJson = "{\"barcode\":" + _productBarcode + ",\"name\":\"GO ON Protein Crisp (Cookies & Cream)\",\"description\":\"Cookies & Cream motherfucker\",\"image\":\"\",\"weight\":50,\"energy\":216,\"protein\":10,\"fat\":8,\"carbohydrates\":26.5,\"sugar\":null,\"salt\":null,\"fiber\":null}";

    [Fact, TestPriority(1)]
    public async Task HttpGet_ShouldReturnUnauthorized()
    {
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();

        HttpRequestMessage httpRequest = new()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"/barcode/{_productBarcode}")
        };

        var response = await client.SendAsync(httpRequest);
        var statusCode = response.StatusCode;

        statusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact, TestPriority(2)]
    public async Task HttpPost_ShouldCreateProduct()
    {
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();
        var token = await TokenFactory.CreateToken(TokenType.User).Get();

        HttpRequestMessage httpRequest = new()
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("/barcode"),
            Content = new StringContent(_productJson, Encoding.UTF8, new MediaTypeHeaderValue("application/json"))
        };
        httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.SendAsync(httpRequest);
        var statusCode = response.StatusCode;

        statusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact, TestPriority(3)]
    public async Task HttpGet_ShouldReturnProduct()
    {
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();
        var token = await TokenFactory.CreateToken(TokenType.User).Get();

        HttpRequestMessage httpRequest = new()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"/barcode/{_productBarcode}")
        };
        httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.SendAsync(httpRequest);
        var statusCode = response.StatusCode;

        string result = await response.Content.ReadAsStringAsync();

        statusCode.Should().Be(HttpStatusCode.OK);
        result.Should().Be(_productJson);
    }

    [Fact, TestPriority(4)]
    public async Task HttpDelete_ShouldDeleteProduct()
    {
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();
        var token = await TokenFactory.CreateToken(TokenType.User).Get();

        HttpRequestMessage httpRequest = new()
        {
            Method = HttpMethod.Delete,
            RequestUri = new Uri($"/barcode/{_productBarcode}")
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
        var token = await TokenFactory.CreateToken(TokenType.User).Get();

        HttpRequestMessage httpRequest = new()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"/barcode/{_productBarcode}")
        };
        httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.SendAsync(httpRequest);
        var statusCode = response.StatusCode;

        statusCode.Should().Be(HttpStatusCode.NotFound);
    }
}