using OrderAPI.Entities;
using System.Collections.Generic;

namespace OrderAPI.Repositories
{
    public interface IOrderRepository
    {
        List<Order> GetOrders();
        Order GetOrder(string id);
        Order AddOrder(Order order);
        void DeleteOrder(string id);
        Order UpdateOrder(Order order);
    }
}
