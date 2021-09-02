using System;
using Inflow.Modules.Payments.Core.Deposits.DTO;
using Inflow.Shared.Abstractions.Queries;

namespace Inflow.Modules.Payments.Core.Deposits.Queries
{
    internal class BrowseDeposits : PagedQuery<DepositDto>
    {
        public Guid? AccountId { get; set; }
        public Guid? CustomerId { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }
    }
}