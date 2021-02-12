using CodeGenOutput.API.Requests;
using CodeGenOutput.API.Requests.Contacts;
using CodeGenOutput.API.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CodeGenOutput.API.Controllers
{
    [Route("api/contacts")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContactsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/contacts
        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(await _mediator.Send(new GetContacts()));
        }

        // GET: api/contacts/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactById([FromRoute] Guid id)
        {
            return Ok(await _mediator.Send(new GetContactById() { Id = id }));
        }

        // POST: api/contacts
        [HttpPost]
        public async Task<IActionResult> CreateContact([FromBody] ContactCreateVM contactCreateVM)
        {
            Response response = await _mediator.Send(new CreateContact() { ContactCreateVM = contactCreateVM });
            return CreatedAtAction("GetContactById", new { id = (response.Data as ContactVM).Id }, response);
        }

        // PUT: api/contacts/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, [FromBody] ContactUpdateVM contactUpdateVM)
        {
            if (id != contactUpdateVM.Id) { return BadRequest(); }
            return Ok(await _mediator.Send(new UpdateContact() { ContactUpdateVM = contactUpdateVM }));
        }

        // DELETE: api/contacts/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            return Ok(await _mediator.Send(new DeleteContact() { Id = id }));
        }
    }
}
