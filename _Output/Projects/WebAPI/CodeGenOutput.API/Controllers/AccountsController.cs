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

        // GET: api/accounts/{code}
        [HttpGet("{code}")]
        public async Task<ActionResult<Response<AccountVM>>> GetAccountByCode([FromRoute] string code)
        {
            return Ok(await _mediator.Send(new GetAccountByCode() { Code = code }));
        }

        // POST: api/accounts
        [HttpPost]
        public async Task<ActionResult<Response<AccountVM>>> CreateAccount([FromBody] AccountCreateVM accountCreateVM)
        {
            Response<AccountVM> response = await _mediator.Send(new CreateAccount() { AccountCreateVM = accountCreateVM });
            return CreatedAtAction("GetAccountByCode", new { code = response.Data.Code }, response);
        }

        // PUT: api/accounts/{code}
        [HttpPut("{code}")]
        public async Task<ActionResult<Response<AccountVM>>> UpdateAccount([FromRoute] string code, [FromBody] AccountUpdateVM accountUpdateVM)
        {
            if (code != accountUpdateVM.Code) { return BadRequest(); }
            return Ok(await _mediator.Send(new UpdateAccount() { AccountUpdateVM = accountUpdateVM }));
        }

        // DELETE: api/accounts/{code}
        [HttpDelete("{code}")]
        public async Task<ActionResult<Response>> DeleteAccount([FromRoute] string code)
        {
            return Ok(await _mediator.Send(new DeleteAccount() { Code = code }));
        }
    }
}
