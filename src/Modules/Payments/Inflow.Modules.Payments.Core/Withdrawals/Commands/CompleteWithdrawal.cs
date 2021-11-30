using System;
using Inflow.Shared.Abstractions.Commands;

namespace Inflow.Modules.Payments.Core.Withdrawals.Commands;

internal record CompleteWithdrawal(Guid WithdrawalId, string Secret) : ICommand;