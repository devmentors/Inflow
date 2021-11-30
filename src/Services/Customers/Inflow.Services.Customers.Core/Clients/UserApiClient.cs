using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Inflow.Services.Customers.Core.Clients.DTO;

namespace Inflow.Services.Customers.Core.Clients;

// Extract URL to the service registry & discovery tool, add Polly for retries, error handling etc.
public class UserApiClient : IUserApiClient
{
    private const string ApiUrl = "http://localhost:5010";
    private readonly IHttpClientFactory _clientFactory;

    public UserApiClient(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public Task<UserDto> GetAsync(string email)
    {
        var client = _clientFactory.CreateClient();
        return client.GetFromJsonAsync<UserDto>($"{ApiUrl}/users/by-email/{email}");
    }
}