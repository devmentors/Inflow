using Inflow.Shared.Abstractions.Kernel.ValueObjects;

namespace Inflow.Modules.Payments.Core.Deposits.Domain.Services;

internal interface ICurrencyResolver
{
    Currency GetForNationality(Nationality nationality);
}