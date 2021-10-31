using System;

namespace Inflow.Services.Customers.Core.DTO
{
    public class CustomerDto
    {
        public Guid CustomerId { get; set; }
        public string State { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Nationality { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}