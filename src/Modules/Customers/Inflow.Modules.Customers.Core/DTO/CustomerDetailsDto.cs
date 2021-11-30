namespace Inflow.Modules.Customers.Core.DTO;

public class CustomerDetailsDto : CustomerDto
{
    public string Address { get; set; }
    public IdentityDto Identity { get; set; }
    public string Notes { get; set; }
}