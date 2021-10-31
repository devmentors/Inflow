using System;
using Inflow.Services.Customers.Core.DTO;
using Inflow.Shared.Abstractions.Queries;

namespace Inflow.Services.Customers.Core.Queries
{
    public class GetCustomer : IQuery<CustomerDetailsDto>
    {
        public Guid CustomerId { get; set; }
    }
}