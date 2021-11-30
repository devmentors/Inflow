using System;

namespace Inflow.Modules.Payments.Infrastructure.Clients.DTO;

internal class CustomerDto
{
    public Guid CustomerId { get; set; }
    public string State { get; set; }
    public bool IsActive { get; set; }
}