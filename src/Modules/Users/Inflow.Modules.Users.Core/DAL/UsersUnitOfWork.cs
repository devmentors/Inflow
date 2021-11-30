using Inflow.Shared.Infrastructure.Postgres;

namespace Inflow.Modules.Users.Core.DAL;

internal class UsersUnitOfWork : PostgresUnitOfWork<UsersDbContext>
{
    public UsersUnitOfWork(UsersDbContext dbContext) : base(dbContext)
    {
    }
}