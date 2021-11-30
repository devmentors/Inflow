using System.Collections.Generic;
using Inflow.Modules.Users.Core.DTO;
using Inflow.Modules.Users.Core.Entities;

namespace Inflow.Modules.Users.Core.Queries.Handlers;

internal static class Extensions
{
    private static readonly Dictionary<UserState, string> States = new()
    {
        [UserState.Active] = UserState.Active.ToString().ToLowerInvariant(),
        [UserState.Locked] = UserState.Locked.ToString().ToLowerInvariant()
    };
        
    public static UserDto AsDto(this User member)
        => member.Map<UserDto>();

    public static UserDetailsDto AsDetailsDto(this User user)
    {
        var dto = user.Map<UserDetailsDto>();
        dto.Permissions = user.Role.Permissions;

        return dto;
    }

    private static T Map<T>(this User user) where T : UserDto, new()
        => new()
        {
            UserId = user.Id,
            Email = user.Email,
            State = States[user.State],
            Role = user.Role.Name,
            CreatedAt = user.CreatedAt
        };
}