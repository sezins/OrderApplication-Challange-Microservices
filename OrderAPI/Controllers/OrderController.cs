using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderAPI.Dto;
using OrderAPI.Entities;
using System.Collections.Generic;
using System;
using OrderAPI.Repositories;
using LoggerService.LoggerManager;

namespace OrderAPI.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly LoggerManager _logger;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository orderRepository, IMapper mapper, LoggerManager logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            var orders = _orderRepository.GetOrders();
            var ordersDto = _mapper.Map<IEnumerable<OrderDto>>(orders);
            return Ok(ordersDto);
        }

        [HttpGet("{id}", Name = "GetOrder")]
        public IActionResult GetOrder(string id)
        {
            var order = _orderRepository.GetOrder(id);
            if (order == null)
            {
                _logger.LogInfo($"Order with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var orderDto = _mapper.Map<OrderDto>(order);
            return Ok(orderDto);           
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] OrderDto order)
        {
            var orderEntity = _mapper.Map<Order>(order);
            _orderRepository.AddOrder(orderEntity);
            var orderToReturn = _mapper.Map<OrderDto>(orderEntity);
            return CreatedAtRoute("GetOrder", new { id = orderToReturn.Id }, orderToReturn);
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult DeleteOrder(string id)
        {
            var orderForCheck = _orderRepository.GetOrder(id);
            if (orderForCheck == null)
            {
                _logger.LogInfo($"Order with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _orderRepository.DeleteOrder(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrder(string id, [FromBody] OrderDto order)
        {
            var orderForCheck = _orderRepository.GetOrder(id);
            if (orderForCheck == null)
            {
               _logger.LogInfo($"Order with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var orderEntity = _mapper.Map<Order>(order);
            orderEntity.Id = id;
            orderEntity.CreatedAt = orderForCheck.CreatedAt;
            orderEntity.UpdatedAt = DateTime.Now;
            _orderRepository.UpdateOrder(orderEntity);
            return NoContent();                       
        }
    }
}
