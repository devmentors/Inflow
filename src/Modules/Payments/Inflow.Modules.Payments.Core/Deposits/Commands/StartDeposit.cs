using System;
using Inflow.Shared.Abstractions.Commands;

namespace Inflow.Modules.Payments.Core.Deposits.Commands;

internal record StartDeposit(Guid AccountId, Guid CustomerId, string Currency, decimal Amount) : ICommand
{
    public Guid DepositId { get; init; } = Guid.NewGuid();
}