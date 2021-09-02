using Inflow.Shared.Infrastructure.Postgres;

namespace Inflow.Modules.Payments.Core.DAL
{
    internal class PaymentsUnitOfWork : PostgresUnitOfWork<PaymentsDbContext>
    {
        public PaymentsUnitOfWork(PaymentsDbContext dbContext) : base(dbContext)
        {
        }
    }
}