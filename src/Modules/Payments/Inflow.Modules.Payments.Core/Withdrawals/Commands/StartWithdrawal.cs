using System;
using Inflow.Shared.Abstractions.Commands;

namespace Inflow.Modules.Payments.Core.Withdrawals.Commands
{
    internal record StartWithdrawal(Guid AccountId, Guid CustomerId, string Currency, decimal Amount) : ICommand
    {
        public Guid WithdrawalId { get; init; } = Guid.NewGuid();
    }
}