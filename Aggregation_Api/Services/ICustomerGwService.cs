using Aggregation_Api.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aggregation_Api.Services
{
    public interface ICustomerGwService
    {
        Task<IEnumerable<Customer>> GetCustomers();
        Task<Customer> GetCustomer(string id);
        Task<Customer> CreateCustomer(Customer customer);
        Task UpdateCustomer(string id, Customer customer);
        Task<Customer> GetCustomerForAddress(string id);
    }
}
