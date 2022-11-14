using Aggregation_Api.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace Aggregation_Api.Services
{
    public class OrderGwService : IOrderGwService
    {
        private readonly HttpClient _client;
        public OrderGwService(HttpClient client)
        {
            _client = client;
        }
        public async Task<Order> CreateOrder(Order order)
        {
            Order data;
            var postResponse = await _client.PostAsJsonAsync("/api/order", order);
            postResponse.EnsureSuccessStatusCode();
            var stream = await postResponse.Content.ReadAsStreamAsync();
            using (var streamReader = new StreamReader(stream))
            {
                using (var jsonTextReader = new JsonTextReader(streamReader))
                {
                    var jsonSerializer = new JsonSerializer();
                    data = jsonSerializer.Deserialize<Order>(jsonTextReader);
                }
            }
            return data;
        }

        public async Task<Order> GetOrder(string id)
        {
            return await _client.GetFromJsonAsync<Order>($"/api/order/{id}");
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await _client.GetFromJsonAsync<List<Order>>("/api/order");
        }

        public async Task UpdateOrder(string id, Order order)
        {
            var postResponse = await _client.PutAsJsonAsync($"/api/order/{id}", order);
            postResponse.EnsureSuccessStatusCode();
        }
    }
}
