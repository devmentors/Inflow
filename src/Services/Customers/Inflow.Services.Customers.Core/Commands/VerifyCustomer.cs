using System;
using Inflow.Shared.Abstractions.Commands;

namespace Inflow.Services.Customers.Core.Commands;

public record VerifyCustomer(Guid CustomerId) : ICommand;