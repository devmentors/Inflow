using System;
using System.Collections.Generic;
using Inflow.Modules.Payments.Infrastructure.Entities;
using Inflow.Modules.Payments.Infrastructure.ValueObjects;
using Inflow.Shared.Abstractions.Kernel.ValueObjects;

namespace Inflow.Modules.Payments.Core.Withdrawals.Domain.Entities
{
    internal class WithdrawalAccount
    {
        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public Customer Customer { get; private set; }
        public Currency Currency { get; private set; }
        public Iban Iban { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public IEnumerable<Withdrawal> Withdrawals { get; private set; }

        private WithdrawalAccount()
        {
        }

        public WithdrawalAccount(Guid id, Guid customerId, Currency currency, Iban iban, DateTime createdAt)
        {
            Id = id;
            CustomerId = customerId;
            Currency = currency;
            Iban = iban;
            CreatedAt = createdAt;
        }

        public Withdrawal CreateWithdrawal(Guid withdrawalId, Amount amount, DateTime createdAt)
            => new(withdrawalId, Id, amount, Currency, createdAt);
    }
}