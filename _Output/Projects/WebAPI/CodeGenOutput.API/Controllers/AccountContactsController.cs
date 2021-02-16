using CodeGenOutput.API.Requests;
using CodeGenOutput.API.Requests.AccountContacts;
using CodeGenOutput.API.ViewModels;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CodeGenOutput.API.Controllers
{
    [Route("api/accountcontacts")]
    [ApiController]
    public class AccountContactsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountContactsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/accountcontacts
        [HttpGet]
        public async Task<IActionResult> GetAccountContacts([FromQuery] string include = "")
        {
            return Ok(await _mediator.Send(new GetAccountContacts() { Include = include }));
        }

        // GET: api/accountcontacts/{id}
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAccountContactById([FromRoute] Guid id, [FromQuery] string include = "")
        {
            return Ok(await _mediator.Send(new GetAccountContactById() { Id = id, Include = include }));
        }

        // POST: api/accountcontacts
        [HttpPost]
        public async Task<IActionResult> CreateAccountContact([FromBody] AccountContactCreateVM accountcontactCreateVM)
        {
            Response response = await _mediator.Send(new CreateAccountContact() { AccountContactCreateVM = accountcontactCreateVM });
            return CreatedAtAction("GetAccountContactById", new { id = (response.Data as AccountContactVM).Id }, response);
        }

        // PUT: api/accountcontacts/{id}
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateAccountContact([FromRoute] Guid id, [FromBody] AccountContactUpdateVM accountcontactUpdateVM)
        {
            if (id != accountcontactUpdateVM.Id) { return BadRequest(); }
            return Ok(await _mediator.Send(new UpdateAccountContact() { AccountContactUpdateVM = accountcontactUpdateVM }));
        }

        // PATCH: api/accountcontacts/{id}
        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> PatchAccountContact([FromRoute] Guid id, [FromBody] JsonPatchDocument<AccountContactUpdateVM> accountcontactPatchDocument)
        {
            return Ok(await _mediator.Send(new PatchAccountContact() { Id = id, PatchDocument = accountcontactPatchDocument }));
        }

        // DELETE: api/accountcontacts/{id}
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAccountContact([FromRoute] Guid id)
        {
            return Ok(await _mediator.Send(new DeleteAccountContact() { Id = id }));
        }
    }
}
