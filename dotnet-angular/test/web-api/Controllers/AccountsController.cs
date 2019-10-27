using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Test.API.BLL;
using Test.API.Models;
using Test.API.ViewModels;

namespace Test.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ILogger<AccountsController> logger;
        private readonly IMapper mapper;
        private readonly AccountBLL bll;

        public AccountsController(
            ILogger<AccountsController> logger,
			IMapper mapper,
            AccountBLL bll
        )
        {
            this.logger = logger;
			this.mapper = mapper;
            this.bll = bll;
        }

        // GET: api/Accounts
		/// <summary>
		/// Retrieves all accounts.
		/// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountVM>>> GetAccounts()
        {
            IEnumerable<Account> accounts = await this.bll.GetAllAccountsAsync();

            return this.mapper.Map<IEnumerable<Account>, List<AccountVM>>(accounts);
        }

        // GET: api/Accounts/{id}
		/// <summary>
		/// Retrieves a specific account.
		/// </summary>
		/// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountVM>> GetAccount([FromRoute] Guid id)
        {
            Account account = await this.bll.GetAccountByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            return this.mapper.Map<Account, AccountVM>(account);
        }

        // POST: api/Accounts
		/// <summary>
		/// Creates a new account.
		/// </summary>
		/// <param name="accountVM"></param>
        [HttpPost]
        public async Task<ActionResult<AccountVM>> CreateAccount([FromBody] AccountVM accountVM)
        {
			// Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Mapping
            Account account = this.mapper.Map<AccountVM, Account>(accountVM);

            account = await this.bll.CreateAccountAsync(account);

            return CreatedAtAction(
				"GetAccount",
				new { id = account.Id },
				this.mapper.Map<Account, AccountVM>(account)
			);
        }

		// PUT: api/Accounts/{id}
		/// <summary>
		/// Updates a specific account.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="accountVM"></param>
        [HttpPut("{id}")]
        public async Task<ActionResult<AccountVM>> UpdateAccount([FromRoute] Guid id, [FromBody] AccountVM accountVM)
        {
			// Validation
            if (!ModelState.IsValid || id != accountVM.Id)
            {
                return BadRequest(ModelState);
            }

            // Retrieve existing account
            Account account = await this.bll.GetAccountByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }

			// Mapping
            Account accountUpdate = this.mapper.Map<AccountVM, Account>(accountVM);

			// Update fields
            account.Name = accountUpdate.Name;
            account.Website = accountUpdate.Website;
            account.Telephone = accountUpdate.Telephone;
            account.Email = accountUpdate.Email;
			
            account = await this.bll.UpdateAccountAsync(id, account);

			return this.mapper.Map<Account, AccountVM>(account);
        }

        // DELETE: api/Accounts/{id}
		/// <summary>
		/// Deletes a specific account.
		/// </summary>
		/// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<AccountVM>> DeleteAccount([FromRoute] Guid id)
        {
            // Retrieve existing account
            Account account = await this.bll.GetAccountByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            await this.bll.RemoveAccountAsync(id);

            return this.mapper.Map<Account, AccountVM>(account);
        }
    }
}
