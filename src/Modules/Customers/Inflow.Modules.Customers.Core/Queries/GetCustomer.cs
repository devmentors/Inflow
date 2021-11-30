using System;
using Inflow.Modules.Customers.Core.DTO;
using Inflow.Shared.Abstractions.Queries;

namespace Inflow.Modules.Customers.Core.Queries;

internal class GetCustomer : IQuery<CustomerDetailsDto>
{
    public Guid CustomerId { get; set; }
}