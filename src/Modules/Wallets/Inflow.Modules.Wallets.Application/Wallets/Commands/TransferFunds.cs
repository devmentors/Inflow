using System;
using Inflow.Shared.Abstractions.Commands;

namespace Inflow.Modules.Wallets.Application.Wallets.Commands
{
    internal record TransferFunds(Guid OwnerId, Guid OwnerWalletId, Guid ReceiverWalletId, string Currency,
        decimal Amount) : ICommand;
}