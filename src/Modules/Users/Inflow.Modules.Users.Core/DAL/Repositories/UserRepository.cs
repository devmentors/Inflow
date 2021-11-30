using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Inflow.Modules.Users.Core.Entities;
using Inflow.Modules.Users.Core.Repositories;

namespace Inflow.Modules.Users.Core.DAL.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly UsersDbContext _context;
    private readonly DbSet<User> _users;

    public UserRepository(UsersDbContext context)
    {
        _context = context;
        _users = _context.Users;
    }

    public  Task<User> GetAsync(Guid id)
        => _users.Include(x => x.Role).SingleOrDefaultAsync(x => x.Id == id);

    public  Task<User> GetAsync(string email)
        => _users.Include(x => x.Role).SingleOrDefaultAsync(x => x.Email == email);

    public async Task AddAsync(User user)
    {
        await _users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _users.Update(user);
        await _context.SaveChangesAsync();
    }
}