using CodeGenOutput.API.Requests;
using CodeGenOutput.API.Requests.Contacts;
using CodeGenOutput.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        public async Task<ActionResult<Response<List<ContactListVM>>>> GetContacts([FromQuery] int skip = 0, [FromQuery] int take = 20)
        {
            return Ok(await _mediator.Send(new GetContacts() { Skip = skip, Take = take }));
        }

        // GET: api/contacts/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Response<ContactVM>>> GetContactById([FromRoute] Guid id)
        {
            return Ok(await _mediator.Send(new GetContactById() { Id = id }));
        }

        // POST: api/contacts
        [HttpPost]
        public async Task<ActionResult<Response<ContactVM>>> CreateContact([FromBody] ContactCreateVM contactCreateVM)
        {
            Response<ContactVM> response = await _mediator.Send(new CreateContact() { ContactCreateVM = contactCreateVM });
            return CreatedAtAction("GetContactById", new { id = response.Data.Id }, response);
        }

        // PUT: api/contacts/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Response<ContactVM>>> UpdateContact([FromRoute] Guid id, [FromBody] ContactVM contactVM)
        {
            if (id != contactVM.Id) { return BadRequest(); }
            return Ok(await _mediator.Send(new UpdateContact() { ContactVM = contactVM }));
        }

        // DELETE: api/contacts/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>> DeleteContact([FromRoute] Guid id)
        {
            return Ok(await _mediator.Send(new DeleteContact() { Id = id }));
        }
    }
}
