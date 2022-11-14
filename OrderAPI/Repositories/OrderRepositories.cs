using MongoDB.Driver;
using OrderAPI.Data;
using OrderAPI.Entities;
using System;
using System.Collections.Generic;

namespace OrderAPI.Repositories
{
    public class OrderRepositories : IOrderRepository
    {
        private readonly IOrderContext _context;
        public OrderRepositories(IOrderContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Order AddOrder(Order order)
        {
            order.CreatedAt = DateTime.Now;
            _context.Orders.InsertOne(order);
            return order;
        }

        public void DeleteOrder(string id)
        {
            _context.Orders.DeleteOne(order => order.Id == id);
        }

        public Order GetOrder(string id)
        {
            return _context.Orders.Find<Order>(order => order.Id == id).FirstOrDefault();
        }

        public List<Order> GetOrders()
        {
            return _context.Orders.Find(order => true).ToList();
        }

        public Order UpdateOrder(Order order)
        {
            GetOrder(order.Id);
            _context.Orders.ReplaceOne(o => o.Id == order.Id, order);
            return order;
        }
    }
}
