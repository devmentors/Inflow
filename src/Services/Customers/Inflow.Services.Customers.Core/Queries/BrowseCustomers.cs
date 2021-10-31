using Inflow.Services.Customers.Core.DTO;
using Inflow.Shared.Abstractions.Queries;

namespace Inflow.Services.Customers.Core.Queries
{
    public class BrowseCustomers : PagedQuery<CustomerDto>
    {
        public string State { get; set; }
    }
}