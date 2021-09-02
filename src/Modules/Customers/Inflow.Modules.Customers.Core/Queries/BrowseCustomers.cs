using Inflow.Modules.Customers.Core.DTO;
using Inflow.Shared.Abstractions.Queries;

namespace Inflow.Modules.Customers.Core.Queries
{
    internal class BrowseCustomers : PagedQuery<CustomerDto>
    {
        public string State { get; set; }
    }
}