using System;
using Inflow.Modules.Payments.Core.Withdrawals.DTO;
using Inflow.Shared.Abstractions.Queries;

namespace Inflow.Modules.Payments.Core.Withdrawals.Queries
{
    internal class BrowseWithdrawalAccounts : PagedQuery<WithdrawalAccountDto>
    {
        public Guid? CustomerId { get; set; }
        public string Currency { get; set; }
    }
}