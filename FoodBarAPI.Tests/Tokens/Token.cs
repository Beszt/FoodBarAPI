using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;

namespace FoodBarAPI.Tests.Tokens;

public abstract class Token
{
    protected string _jsonCredentials = default!;
    private string? _token = default;
    
    public HttpStatusCode StatusCode { get; protected set; } = default;

    public async Task<string?> Get()
    {
        if (String.IsNullOrEmpty(_token))
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
            StatusCode = response.StatusCode;
            if (StatusCode == HttpStatusCode.OK)
                _token = await response.Content.ReadAsStringAsync();
        }

        return _token;
    }
}
