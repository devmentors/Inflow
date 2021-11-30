using System;
using System.Threading.Tasks;
using Inflow.Modules.Payments.Infrastructure.Clients.DTO;

namespace Inflow.Modules.Payments.Infrastructure.Clients;

internal interface ICustomerApiClient
{
    Task<CustomerDto> GetAsync(Guid customerId);
}