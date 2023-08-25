using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UnitTestingPractical.Models;
using UnitTestingPractical.Repository;

namespace UnitTestingPractical.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet]
        public IActionResult Withdraw(int id, double amount)
        {
            if (id > 0 && amount >= 100)
            {
                var updatedAmount = _accountRepository.WithdrawAmount(id, amount);
                if(updatedAmount != 0)
                    return Ok(updatedAmount);
                return BadRequest();
            }
            return BadRequest();
        }

        [HttpGet]
        public IActionResult Credit(int id, double amount)
        {
            if (id > 0 && amount >= 100 && amount <= 20000)
            {
                var updatedAmount = _accountRepository.CreditAmount(id, amount);
                if(updatedAmount != 0 )
                    return Ok(updatedAmount);
                return BadRequest();
            }
            return BadRequest();
        }

        [HttpGet]
        public IActionResult TransferAmount(int sender, int recipient, double amt)
        {
            if(sender > 0 && recipient > 0 && amt >= 100 && sender != recipient)
            {
                var output = _accountRepository.FundTransfer(sender, recipient, amt);
                if(output == -1) return BadRequest();
                if(output == 0) return NotFound();
                if(output > 0) return Ok(output);
            }
            return BadRequest();
        }
    }
}