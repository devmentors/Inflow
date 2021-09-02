using Inflow.Shared.Abstractions.Kernel.ValueObjects;

namespace Inflow.Modules.Payments.Core.Deposits.Domain.Services
{
    internal class CurrencyResolver : ICurrencyResolver
    {
        public Currency GetForNationality(Nationality nationality)
            => nationality.Value switch
            {
                "PL" => "PLN",
                "DE" => "EUR",
                "FR" => "EUR",
                "ES" => "EUR",
                "GB" => "GBP",
                _ => "EUR"
            };
    }
}