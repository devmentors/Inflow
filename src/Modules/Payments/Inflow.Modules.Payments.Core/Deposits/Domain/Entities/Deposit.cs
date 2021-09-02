using System;
using Inflow.Modules.Payments.Core.Deposits.Exceptions;
using Inflow.Shared.Abstractions.Kernel.ValueObjects;

namespace Inflow.Modules.Payments.Core.Deposits.Domain.Entities
{
    internal class Deposit
    {
        public Guid Id { get; private set; }
        public DepositStatus Status { get; private set; }
        public Guid AccountId { get; private set; }
        public DepositAccount Account { get; private set; }
        public Amount Amount { get; private set; }
        public Currency Currency { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? ProcessedAt { get; private set; }

        private Deposit()
        {
        }

        public Deposit(Guid id, Guid accountId, Amount amount, Currency currency, DateTime createdAt)
        {
            Id = id;
            AccountId = accountId;
            Amount = amount;
            Currency = currency;
            CreatedAt = createdAt;
            Status = DepositStatus.Started;
        }

        public void Complete(DateTime completedAt)
        {
            if (Status != DepositStatus.Started)
            {
                throw new CannotCompleteDepositException(Id);
            }
            
            Status = DepositStatus.Completed;
            ProcessedAt = completedAt;
        }
        
        public void Reject(DateTime rejectedAt)
        {
            if (Status != DepositStatus.Started)
            {
                throw new CannotRejectDepositException(Id);
            }
            
            Status = DepositStatus.Rejected;
            ProcessedAt = rejectedAt;
        }
    }
}