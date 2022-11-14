using CustomerAPI.Entities;
using System.Collections.Generic;

namespace CustomerAPI.Repositories
{
    public interface ICustomerRepository
    {
        List<Customer> GetCustomers();
        Customer CreateCustomer(Customer customer);
        Customer GetCustomer(string id);
        void DeleteCustomer(string id);
        Customer UpdateCustomer(Customer customer);
    }
}
