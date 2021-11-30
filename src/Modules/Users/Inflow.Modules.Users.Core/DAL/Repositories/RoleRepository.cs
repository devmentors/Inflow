using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Inflow.Modules.Users.Core.Entities;
using Inflow.Modules.Users.Core.Repositories;

namespace Inflow.Modules.Users.Core.DAL.Repositories;

internal class RoleRepository : IRoleRepository
{
    private readonly UsersDbContext _context;
    private readonly DbSet<Role> _roles;

    public RoleRepository(UsersDbContext context)
    {
        _context = context;
        _roles = context.Roles;
    }

    public Task<Role> GetAsync(string name) => _roles.SingleOrDefaultAsync(x => x.Name == name);

    public async Task<IReadOnlyList<Role>> GetAllAsync() => await _roles.ToListAsync();
        
    public async Task AddAsync(Role role)
    {
        await _roles.AddAsync(role);
        await _context.SaveChangesAsync();
    }
}