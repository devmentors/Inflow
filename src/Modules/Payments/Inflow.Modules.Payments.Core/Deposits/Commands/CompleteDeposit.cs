using System;
using Inflow.Shared.Abstractions.Commands;

namespace Inflow.Modules.Payments.Core.Deposits.Commands;

internal record CompleteDeposit(Guid DepositId, string Secret) : ICommand;