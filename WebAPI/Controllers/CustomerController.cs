using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("add")]
        public IActionResult Add(Customer customer)
        {
            var result = _customerService.Add(customer);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Customer customer)
        {
            var result = _customerService.Delete(customer);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("update")]
        public IActionResult Update(Customer customer)
        {
            var result = _customerService.Update(customer);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }


        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _customerService.GetAll();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _customerService.GetById(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("getdetails")]
        public IActionResult GetDetails()
        {
            var result = _customerService.GetCustomersDetails();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
