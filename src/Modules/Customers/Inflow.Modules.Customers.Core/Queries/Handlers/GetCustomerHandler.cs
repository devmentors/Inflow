using System.Threading;
using System.Threading.Tasks;
using Inflow.Modules.Customers.Core.DAL;
using Inflow.Modules.Customers.Core.DTO;
using Microsoft.EntityFrameworkCore;
using Inflow.Shared.Abstractions.Queries;

namespace Inflow.Modules.Customers.Core.Queries.Handlers;

internal sealed class GetCustomerHandler : IQueryHandler<GetCustomer, CustomerDetailsDto>
{
    private readonly CustomersDbContext _dbContext;

    public GetCustomerHandler(CustomersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CustomerDetailsDto> HandleAsync(GetCustomer query, CancellationToken cancellationToken = default)
    {
        var customer = await _dbContext.Customers
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == query.CustomerId, cancellationToken);

        return customer?.AsDetailsDto();
    }
}