using CodeGenOutput.API.BLL;
using CodeGenOutput.API.DAL;
using CodeGenOutput.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeGenOutput.API.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountBLL _bll;

        public AccountsController(IBusinessLogicLayer bll)
        {
            _bll = bll;
        }

        // GET: api/accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            return Ok(await _bll.GetAccountsAsync());
        }

        // GET: api/accounts/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccountById([FromRoute] Guid id)
        {
            return Ok(await _bll.GetAccountByIdAsync(id));
        }
        
        // POST: api/accounts
        [HttpPost]
        public async Task<ActionResult<Account>> CreateAccount([FromBody] Account account)
        {
            Account createdAccount = await _bll.CreateAccountAsync(account);
            return CreatedAtAction("GetAccountById", new { id = createdAccount.Id }, createdAccount);
        }

        // PUT: api/accounts/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Account>> UpdateAccount([FromRoute] Guid id, [FromBody] Account account)
        {
            if (id != account.Id) { return BadRequest(); }
            return Ok(await _bll.UpdateAccountAsync(account));
        }

        // DELETE: api/accounts/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(Guid id)
        {
            await _bll.DeleteAccountAsync(new Account() { Id = id });
            return NoContent();
        }
    }
}
