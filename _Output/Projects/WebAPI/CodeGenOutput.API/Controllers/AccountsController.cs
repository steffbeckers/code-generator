using CodeGenOutput.API.Requests;
using CodeGenOutput.API.Requests.Accounts;
using CodeGenOutput.API.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeGenOutput.API.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/accounts
        [HttpGet]
        public async Task<ActionResult<Response<List<AccountListVM>>>> GetAccounts([FromQuery] int skip = 0, [FromQuery] int take = 20)
        {
            return Ok(await _mediator.Send(new GetAccounts() { Skip = skip, Take = take }));
        }

        // GET: api/accounts/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Response<AccountVM>>> GetAccountById([FromRoute] Guid id)
        {
            return Ok(await _mediator.Send(new GetAccountById() { Id = id }));
        }

        // POST: api/accounts
        [HttpPost]
        public async Task<ActionResult<Response<AccountVM>>> CreateAccount([FromBody] AccountCreateVM accountCreateVM)
        {
            Response<AccountVM> response = await _mediator.Send(new CreateAccount() { AccountCreateVM = accountCreateVM });
            return CreatedAtAction("GetAccountById", new { id = response.Data.Id }, response);
        }

        // PUT: api/accounts/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Response<AccountVM>>> UpdateAccount([FromRoute] Guid id, [FromBody] AccountUpdateVM accountUpdateVM)
        {
            if (id != accountUpdateVM.Id) { return BadRequest(); }
            return Ok(await _mediator.Send(new UpdateAccount() { AccountUpdateVM = accountUpdateVM }));
        }

        // DELETE: api/accounts/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>> DeleteAccount([FromRoute] Guid id)
        {
            return Ok(await _mediator.Send(new DeleteAccount() { Id = id }));
        }
    }
}
