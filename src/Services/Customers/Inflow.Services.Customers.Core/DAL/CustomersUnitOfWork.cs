using Inflow.Shared.Infrastructure.Postgres;

namespace Inflow.Services.Customers.Core.DAL
{
    public class CustomersUnitOfWork : PostgresUnitOfWork<CustomersDbContext>
    {
        public CustomersUnitOfWork(CustomersDbContext dbContext) : base(dbContext)
        {
        }
    }
}