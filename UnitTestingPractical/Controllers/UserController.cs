using Microsoft.AspNetCore.Mvc;
using UnitTestingPractical.Models;
using UnitTestingPractical.Repository;

namespace UnitTestingPractical.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public UserController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public IActionResult GetCustomer(int id)
        {
            if (id > 0)
            {
                var customer = _customerRepository.GetCustomer(id);
                if (customer != null)
                    return Ok(customer);
                return NotFound();
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult RegisterNewCustomer(BankDetails bankDetails)
        {
            var newCustomer = _customerRepository.AddNewCustomer(bankDetails);
            if(newCustomer != null)
                return Ok(newCustomer);
            return BadRequest();
        }
    }
}
