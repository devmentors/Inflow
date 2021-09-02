using System;
using Inflow.Shared.Abstractions.Commands;

namespace Inflow.Modules.Payments.Core.Withdrawals.Commands
{
    internal record AddWithdrawalAccount(Guid CustomerId, string Currency, string Iban) : ICommand
    {
        public Guid AccountId { get; init; } = Guid.NewGuid();
    }
}