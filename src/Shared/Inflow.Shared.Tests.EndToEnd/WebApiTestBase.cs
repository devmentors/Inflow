using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Inflow.Bootstrapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Inflow.Shared.Tests.EndToEnd;

[Collection("tests")]
public abstract class WebApiTestBase : IDisposable, IClassFixture<WebApplicationFactory<Startup>>
{
    protected readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new JsonStringEnumConverter() }
    };
        
    private string _route;

    protected void SetPath(string route)
    {
        if (string.IsNullOrWhiteSpace(route))
        {
            _route = string.Empty;
            return;
        }

        if (route.StartsWith("/"))
        {
            route = route.Substring(1, route.Length - 1);
        }

        if (route.EndsWith("/"))
        {
            route = route.Substring(0, route.Length - 1);
        }

        _route = $"{route}/";
    }

    protected Task<T> GetAsync<T>(string endpoint)
        => Client.GetFromJsonAsync<T>(endpoint);

    protected Task<HttpResponseMessage> PostAsync<T>(string endpoint, T payload)
        => Client.PostAsJsonAsync(GetEndpoint(endpoint), payload, SerializerOptions);

    protected Task<HttpResponseMessage> PutAsync<T>(string endpoint, T payload)
        => Client.PutAsJsonAsync(GetEndpoint(endpoint), payload, SerializerOptions);

    protected Task<HttpResponseMessage> DeleteAsync(string endpoint)
        => Client.DeleteAsync(GetEndpoint(endpoint));
        
    protected void Authenticate(Guid userId, IDictionary<string, IEnumerable<string>> claims = null)
    {
        var jwt = AuthHelper.GenerateJwt(userId, claims: claims);
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
    }

    protected T Map<T>(object data)
        => JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(data, SerializerOptions), SerializerOptions);

    private string GetEndpoint(string endpoint) => $"{_route}{endpoint}";

    #region Arrange

    protected readonly HttpClient Client;
    protected readonly WebApplicationFactory<Startup> Factory;

    protected WebApiTestBase(WebApplicationFactory<Startup> factory, string environment = "test")
    {
        Factory = factory.WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment(environment);
            builder.ConfigureServices(ConfigureServices);
        });
        Client = Factory.CreateClient();
    }

    protected virtual void ConfigureServices(IServiceCollection services)
    {
    }

    public virtual void Dispose()
    {
    }

    #endregion
}