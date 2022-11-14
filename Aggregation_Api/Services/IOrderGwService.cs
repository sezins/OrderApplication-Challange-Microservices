using Aggregation_Api.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aggregation_Api.Services
{
    public interface IOrderGwService
    {
        Task<IEnumerable<Order>> GetOrders();
        Task<Order> GetOrder(string id);
        Task<Order> CreateOrder(Order order);
        Task UpdateOrder(string id, Order order);
    }
}
