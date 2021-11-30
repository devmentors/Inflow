using System;
using Inflow.Shared.Abstractions.Commands;

namespace Inflow.Services.Customers.Core.Commands;

public record UnlockCustomer(Guid CustomerId, string Notes = null) : ICommand;