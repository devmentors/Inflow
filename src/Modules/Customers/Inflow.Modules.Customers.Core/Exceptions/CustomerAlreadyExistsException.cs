using System;
using Inflow.Shared.Abstractions.Exceptions;

namespace Inflow.Modules.Customers.Core.Exceptions
{
    internal class CustomerAlreadyExistsException : InflowException
    {
        public string Name { get; }
        public Guid CustomerId { get; }

        public CustomerAlreadyExistsException(string name)
            : base($"Customer with name: '{name}' already exists.")
        {
            Name = name;
        }
        
        public CustomerAlreadyExistsException(Guid customerId)
            : base($"Customer with ID: '{customerId}' already exists.")
        {
            CustomerId = customerId;
        }
    }
}