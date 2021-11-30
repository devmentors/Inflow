using System;
using Inflow.Shared.Abstractions.Events;

namespace Inflow.Modules.Wallets.Application.Wallets.Events;

internal record FundsLocked(Guid WalletId, string Currency, decimal Amount) : IEvent;