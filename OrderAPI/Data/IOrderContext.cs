using MongoDB.Driver;
using OrderAPI.Entities;

namespace OrderAPI.Data
{
    public interface IOrderContext
    {
        IMongoCollection<Order> Orders { get; }
    }
}
