using Inflow.Shared.Infrastructure.Postgres;

namespace Inflow.Modules.Customers.Core.DAL
{
    internal class CustomersUnitOfWork : PostgresUnitOfWork<CustomersDbContext>
    {
        public CustomersUnitOfWork(CustomersDbContext dbContext) : base(dbContext)
        {
        }
    }
}