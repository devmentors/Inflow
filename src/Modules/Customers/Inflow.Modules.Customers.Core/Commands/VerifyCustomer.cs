using System;
using Inflow.Shared.Abstractions.Commands;

namespace Inflow.Modules.Customers.Core.Commands;

internal record VerifyCustomer(Guid CustomerId) : ICommand;