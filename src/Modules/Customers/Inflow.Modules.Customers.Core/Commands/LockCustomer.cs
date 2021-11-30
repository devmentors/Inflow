using System;
using Inflow.Shared.Abstractions.Commands;

namespace Inflow.Modules.Customers.Core.Commands;

internal record LockCustomer(Guid CustomerId, string Notes = null) : ICommand;