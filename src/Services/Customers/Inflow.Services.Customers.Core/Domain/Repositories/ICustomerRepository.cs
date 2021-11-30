using System;
using System.Threading.Tasks;
using Inflow.Services.Customers.Core.Domain.Entities;

namespace Inflow.Services.Customers.Core.Domain.Repositories;

public interface ICustomerRepository
{
    Task<bool> ExistsAsync(string name);
    Task<Customer> GetAsync(Guid id);
    Task AddAsync(Customer customer);
    Task UpdateAsync(Customer customer);
}