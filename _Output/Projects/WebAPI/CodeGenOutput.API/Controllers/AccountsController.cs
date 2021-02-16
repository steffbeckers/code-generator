using CodeGenOutput.API.Requests;
using CodeGenOutput.API.Requests.Accounts;
using CodeGenOutput.API.ViewModels;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
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
        public async Task<IActionResult> GetAccounts([FromQuery] string include = "")
        {
            return Ok(await _mediator.Send(new GetAccounts() { Include = include }));
        }

        // GET: api/accounts/{id}
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAccountById([FromRoute] Guid id, [FromQuery] string include = "")
        {
            return Ok(await _mediator.Send(new GetAccountById() { Id = id, Include = include }));
        }

        // POST: api/accounts
        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] AccountCreateVM accountCreateVM)
        {
            Response response = await _mediator.Send(new CreateAccount() { AccountCreateVM = accountCreateVM });
            return CreatedAtAction("GetAccountById", new { id = (response.Data as AccountVM).Id }, response);
        }

        // PUT: api/accounts/{id}
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateAccount([FromRoute] Guid id, [FromBody] AccountUpdateVM accountUpdateVM)
        {
            if (id != accountUpdateVM.Id) { return BadRequest(); }
            return Ok(await _mediator.Send(new UpdateAccount() { AccountUpdateVM = accountUpdateVM }));
        }

        // PATCH: api/accounts/{id}
        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> PatchAccount([FromRoute] Guid id, [FromBody] JsonPatchDocument<AccountUpdateVM> accountPatchDocument)
        {
            return Ok(await _mediator.Send(new PatchAccount() { Id = id, PatchDocument = accountPatchDocument }));
        }

        // DELETE: api/accounts/{id}
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAccount([FromRoute] Guid id)
        {
            return Ok(await _mediator.Send(new DeleteAccount() { Id = id }));
        }
    }
}
