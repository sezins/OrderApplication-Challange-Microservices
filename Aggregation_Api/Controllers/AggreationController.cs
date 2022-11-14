using Aggregation_Api.Entities;
using Aggregation_Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aggregation_Api.Controllers
{
    [ApiController]
    [Route("api/aggreation")]
    public class AggreationController : Controller
    {
        private readonly ICustomerGwService _customerGwService;
        private readonly IOrderGwService _orderGwService;
        public AggreationController(ICustomerGwService customerGwService, IOrderGwService orderGwService)
        {
            _customerGwService = customerGwService;
            _orderGwService = orderGwService;
            
        }
        //COSTUMER APİ
        [Route("customer")]
        [HttpGet]
        public async Task<IEnumerable<Customer>> GetCostumers()
        {
            return await _customerGwService.GetCustomers();
        }
        [HttpGet("customer/{id}", Name = "GetCustomer")]
        public async Task<Customer> GetCostumer(string id)
        {
            return await _customerGwService.GetCustomer(id);
        }
        [Route("customer")]
        [HttpPost]
        public async Task<CreatedAtRouteResult> CreateCustomer(Customer customerForCreation)
        {
            var customer = await _customerGwService.CreateCustomer(customerForCreation);
            return CreatedAtRoute("GetCustomer", new { id = customer.Id }, customer);
        }
        [HttpPut("customer/{id}")]
        public async Task<NoContentResult> UpdateCustomer(string id, [FromBody] Customer customerForCreation)
        {
            await _customerGwService.UpdateCustomer(id, customerForCreation);
            return NoContent();
        }
        //ORDER APİ
        [Route("order")]
        [HttpGet]
        public async Task<IEnumerable<Order>> GetOrder()
        {
            return await _orderGwService.GetOrders();
        }

        [HttpGet("order/{id}", Name = "GetOrder")]
        public async Task<Order> GetOrder(string id)
        {
            return await _orderGwService.GetOrder(id);
        }

        [Route("order")]
        [HttpPost]
        public async Task<CreatedAtRouteResult> CreateOrder(Order orderPostRequest)
        {
            var address = _customerGwService.GetCustomerForAddress(orderPostRequest.CustomerId).Result.Address;
            
            var orderCreation = new Order
            {
                CustomerId = orderPostRequest.CustomerId,
                Quantity = orderPostRequest.Quantity,
                Price = orderPostRequest.Price,
                Status = orderPostRequest.Status,
                Address = address,
                
            };
            var order = await _orderGwService.CreateOrder(orderCreation);
            return CreatedAtRoute("GetOrder", new { id = order.Id }, order);
        }
    }
}
