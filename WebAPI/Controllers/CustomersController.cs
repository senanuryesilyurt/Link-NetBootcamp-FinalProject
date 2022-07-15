using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomersController(ICustomerService customerService)
        {
            _customerService= customerService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var customers = _customerService.GetAll();
            if (customers.Success)
            {
                return Ok(customers);
            }
            return BadRequest();
        }
        [HttpGet("get")]
        public IActionResult GetCustomer(int customerId)
        {
            var customer = _customerService.Get(customerId);
            if(customer is not null)
            {
                return Ok(customer);
            }

            return BadRequest();
        }
        [HttpPost]
        public IActionResult Add(Customer customer)
        {
            var addedCustomer = _customerService.Add(customer);
            if(addedCustomer.Success)
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpPut]
        public IActionResult Update(Customer customer)
        {
            var updatedCustomer = _customerService.Update(customer);
            if (updatedCustomer.Success)
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpDelete]
        public IActionResult Delete(Customer customer)
        {
            var deletedCustomer = _customerService.Delete(customer);
            if (deletedCustomer.Success)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
