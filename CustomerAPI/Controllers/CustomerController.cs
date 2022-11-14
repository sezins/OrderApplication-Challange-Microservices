using AutoMapper;
using CustomerAPI.Dto;
using CustomerAPI.Entities;
using CustomerAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using LoggerService.LoggerManager;

namespace CustomerAPI.Controllers
{
    [ApiController]
    [Route("api/customer")]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly LoggerManager _logger;
        private readonly IMapper _mapper;
        public CustomerController(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetCustomers()
        {
            var customers = _customerRepository.GetCustomers();
            var customersDto = _mapper.Map<IEnumerable<CustomerDto>>(customers);
            return Ok(customersDto);
        }
        [HttpGet("{id}", Name = "GetCustomer")]
        public IActionResult GetCustomer(string id)
        {
            var customer = _customerRepository.GetCustomer(id);
            if (customer == null)
            {
                _logger.LogInfo($"Customer with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var customerDto = _mapper.Map<CustomerDto>(customer);
            return Ok(customerDto);
        }
        [HttpGet("address/{id}")]
        public IActionResult GetCustomerForAddress(string id)
        {
            var customer = _customerRepository.GetCustomer(id);
            if (customer == null)
            {
                _logger.LogInfo($"Customer with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            return Ok(customer);
        }
        [HttpPost]
        public IActionResult CreateCustomer([FromBody] CustomerDto customer)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the CustomerForCreationDto object");
                return UnprocessableEntity(ModelState);
            }

            var customerEntity = _mapper.Map<Customer>(customer);
            _customerRepository.CreateCustomer(customerEntity);

            var customerToReturn = _mapper.Map<CustomerDto>(customerEntity);
            return CreatedAtRoute("GetCustomer", new { id = customerToReturn.Id }, customerToReturn);
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult DeleteCustomer(string id)
        {
            var customer = _customerRepository.GetCustomer(id);
            if (customer == null)
            {
                _logger.LogInfo($"Customer with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _customerRepository.DeleteCustomer(id);
            return NoContent();
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult UpdateCustomer(string id, [FromBody] CustomerDto customer)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the CustomerForCreationDto object");
                return UnprocessableEntity(ModelState);
            }

            var customerForCheck = _customerRepository.GetCustomer(id);
            if (customerForCheck == null)
            {
                _logger.LogInfo($"Customer with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var customerEntity = _mapper.Map<Customer>(customer);
            customerEntity.Id = id;
            customerEntity.CreatedAt = customerForCheck.CreatedAt;
            customerEntity.UpdatedAt = DateTime.Now;
            _customerRepository.UpdateCustomer(customerEntity);
            return NoContent();

        }
    }
}
