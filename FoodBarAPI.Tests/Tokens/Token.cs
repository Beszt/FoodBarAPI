using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;

namespace FoodBarAPI.Tests.Tokens;

public abstract class Token(string _jsonCredentials)
{
    public string? Bearer { get; private set; }
    
    public async Task<Token> InitializeAsync()
    {
        if (String.IsNullOrEmpty(Bearer))
        {
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();
            var body = _jsonCredentials;

            HttpRequestMessage httpRequest = new ()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("/login"),
                Content = new StringContent(body, Encoding.UTF8, new MediaTypeHeaderValue("application/json"))
            };

            var response = await client.SendAsync(httpRequest);
            var statusCode = response.StatusCode;
            if (statusCode == HttpStatusCode.OK)
                Bearer = await response.Content.ReadAsStringAsync();
        }

        return this;
    }
}
