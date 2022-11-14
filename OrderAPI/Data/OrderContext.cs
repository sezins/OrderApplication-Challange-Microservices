using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using OrderAPI.Entities;

namespace OrderAPI.Data
{
    public class OrderContext : IOrderContext
    {
        public OrderContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            Orders = database.GetCollection<Order>(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        }
        public IMongoCollection<Order> Orders { get; }
    }
}
