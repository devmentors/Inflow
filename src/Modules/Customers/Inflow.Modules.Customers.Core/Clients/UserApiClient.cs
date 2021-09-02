using System.Threading.Tasks;
using Inflow.Modules.Customers.Core.Clients.DTO;
using Inflow.Shared.Abstractions.Modules;

namespace Inflow.Modules.Customers.Core.Clients
{
    internal class UserApiClient : IUserApiClient
    {
        private readonly IModuleClient _moduleClient;

        public UserApiClient(IModuleClient moduleClient)
        {
            _moduleClient = moduleClient;
        }

        public Task<UserDto> GetAsync(string email)
            => _moduleClient.SendAsync<UserDto>("users/get", new { email });
    }
}