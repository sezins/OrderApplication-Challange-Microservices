using CustomerAPI.Data;
using CustomerAPI.Entities;
using System.Collections.Generic;
using System;
using MongoDB.Driver;

namespace CustomerAPI.Repositories
{
    public class CustomerRepository
    {
        private readonly ICustomerContext _context;
        public CustomerRepository(ICustomerContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public Customer CreateCustomer(Customer customer)
        {
            customer.CreatedAt = DateTime.Now;
            _context.Customers.InsertOne(customer);
            return customer;
        }

        public void DeleteCustomer(string id)
        {
            _context.Customers.DeleteOne(customer => customer.Id == id);
        }

        public Customer GetCustomer(string id)
        {
            return _context.Customers.Find<Customer>(customer => customer.Id == id).FirstOrDefault();
        }

        public List<Customer> GetCustomers()
        {
            return _context.Customers.Find(customer => true).ToList();
        }

        public Customer UpdateCustomer(Customer customer)
        {
            GetCustomer(customer.Id);
            customer.UpdatedAt = DateTime.Now;
            _context.Customers.ReplaceOne(c => c.Id == customer.Id, customer);
            return customer;
        }
    }
}
