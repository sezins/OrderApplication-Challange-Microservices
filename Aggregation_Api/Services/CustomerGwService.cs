using Aggregation_Api.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Aggregation_Api.Services
{
    public class CustomerGwService : ICustomerGwService
    {
        private readonly HttpClient _client;
        public CustomerGwService(HttpClient client)
        {
            _client = client;
        }
        public async Task<Customer> CreateCustomer(Customer customer)
        {
            Customer data;
            var postResponse = await _client.PostAsJsonAsync("/api/customer", customer);
            postResponse.EnsureSuccessStatusCode();
            var stream = await postResponse.Content.ReadAsStreamAsync();
            using (var streamReader = new StreamReader(stream))
            {
                using (var jsonTextReader = new JsonTextReader(streamReader))
                {
                    var jsonSerializer = new JsonSerializer();
                    data = jsonSerializer.Deserialize<Customer>(jsonTextReader);
                }
            }
            return data;
        }

        public async Task<Customer> GetCustomer(string id)
        {
            return await _client.GetFromJsonAsync<Customer>($"/api/customer/{id}");
        }

        public async Task<Customer> GetCustomerForAddress(string id)
        {
            return await _client.GetFromJsonAsync<Customer>($"/api/customer/address/{id}");
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await _client.GetFromJsonAsync<List<Customer>>("/api/customer");
        }

        public async Task UpdateCustomer(string id, Customer customer)
        {
            var postResponse = await _client.PutAsJsonAsync($"/api/customer/{id}", customer);
            postResponse.EnsureSuccessStatusCode();
        }
    }
}
