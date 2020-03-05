using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.API.BLL;
using Test.API.Models;
using Test.API.ViewModels;

namespace Test.API.Controllers
{
	/// <summary>
	/// The Accounts controller.
	/// </summary>
    [Route("api/[controller]")]
    [ApiController]
	[Produces("application/json")]
    public class AccountsController : ControllerBase
    {
        private readonly ILogger<AccountsController> logger;
        private readonly IMapper mapper;
        private readonly AccountBLL bll;

		/// <summary>
		/// The constructor of the Accounts controller.
		/// </summary>
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

        // GET: api/accounts
		/// <summary>
		/// Retrieves all accounts.
		/// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountVM>>> GetAccounts()
        {
            IEnumerable<Account> accounts = await this.bll.GetAllAccountsAsync();

			// Mapping
            return Ok(this.mapper.Map<IEnumerable<Account>, List<AccountVM>>(accounts));
        }

        // GET: api/accounts/{id}
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

			// Mapping
            return Ok(this.mapper.Map<Account, AccountVM>(account));
        }

        // POST: api/accounts
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

			// Mapping
            return CreatedAtAction(
				"GetAccount",
				new { id = account.Id },
				this.mapper.Map<Account, AccountVM>(account)
			);
        }

		// PUT: api/accounts/{id}
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

			// Mapping
            Account account = this.mapper.Map<AccountVM, Account>(accountVM);

            account = await this.bll.UpdateAccountAsync(account);

			// Mapping
			return Ok(this.mapper.Map<Account, AccountVM>(account));
        }

        // DELETE: api/accounts/{id}
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

            await this.bll.DeleteAccountAsync(account);

            // Mapping
            return Ok(this.mapper.Map<Account, AccountVM>(account));
        }
    }
}
