using System;
using Inflow.Shared.Abstractions.Commands;

namespace Inflow.Modules.Customers.Core.Commands;

internal record UnlockCustomer(Guid CustomerId, string Notes = null) : ICommand;