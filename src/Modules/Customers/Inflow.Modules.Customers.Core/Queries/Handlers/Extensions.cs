using Inflow.Modules.Customers.Core.Domain.Entities;
using Inflow.Modules.Customers.Core.DTO;

namespace Inflow.Modules.Customers.Core.Queries.Handlers;

internal static class Extensions
{
    public static CustomerDto AsDto(this Customer customer)
        => customer.Map<CustomerDto>();

    public static CustomerDetailsDto AsDetailsDto(this Customer customer)
    {
        var dto = customer.Map<CustomerDetailsDto>();
        dto.Address = customer.Address;
        dto.Identity = customer.Identity is null
            ? null
            : new IdentityDto
            {
                Type = customer.Identity.Type,
                Series = customer.Identity.Series
            };
        dto.Notes = customer.Notes;

        return dto;
    }

    private static T Map<T>(this Customer customer) where T : CustomerDto, new()
        => new()
        {
            CustomerId = customer.Id,
            Email = customer.Email,
            Name = customer.Name,
            FullName = customer.FullName,
            Nationality = customer.Nationality,
            IsActive = customer.IsActive,
            State = customer.GetState(),
            CreatedAt = customer.CreatedAt
        };

    private static string GetState(this Customer customer)
    {
        if (!customer.IsActive)
        {
            return "locked";
        }

        if (customer.VerifiedAt.HasValue)
        {
            return "verified";
        }
            
        return customer.CompletedAt.HasValue ? "completed" : "new";
    }
}