using System;
using System.Threading.Tasks;
using Inflow.Modules.Payments.Infrastructure.Clients.DTO;
using Inflow.Shared.Abstractions.Modules;

namespace Inflow.Modules.Payments.Infrastructure.Clients
{
    internal class CustomerApiClient : ICustomerApiClient
    {
        private readonly IModuleClient _moduleClient;

        public CustomerApiClient(IModuleClient moduleClient)
        {
            _moduleClient = moduleClient;
        }
        
        public Task<CustomerDto> GetAsync(Guid customerId)
            => _moduleClient.SendAsync<CustomerDto>("customers/get", new { customerId });
    }
}