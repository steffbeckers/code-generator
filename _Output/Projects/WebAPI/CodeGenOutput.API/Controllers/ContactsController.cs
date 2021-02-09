using CodeGenOutput.API.Requests;
using CodeGenOutput.API.Requests.Contacts;
using CodeGenOutput.API.ViewModels;
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

        // GET: api/contacts/{code}
        [HttpGet("{code}")]
        public async Task<ActionResult<Response<ContactVM>>> GetContactByCode([FromRoute] string code)
        {
            return Ok(await _mediator.Send(new GetContactByCode() { Code = code }));
        }

        // POST: api/contacts
        [HttpPost]
        public async Task<ActionResult<Response<ContactVM>>> CreateContact([FromBody] ContactCreateVM contactCreateVM)
        {
            Response<ContactVM> response = await _mediator.Send(new CreateContact() { ContactCreateVM = contactCreateVM });
            return CreatedAtAction("GetContactByCode", new { code = response.Data.Code }, response);
        }

        // PUT: api/contacts/{code}
        [HttpPut("{code}")]
        public async Task<ActionResult<Response<ContactVM>>> UpdateContact([FromRoute] string code, [FromBody] ContactUpdateVM contactUpdateVM)
        {
            if (code != contactUpdateVM.Code) { return BadRequest(); }
            return Ok(await _mediator.Send(new UpdateContact() { ContactUpdateVM = contactUpdateVM }));
        }

        // DELETE: api/contacts/{code}
        [HttpDelete("{code}")]
        public async Task<ActionResult<Response>> DeleteContact([FromRoute] string code)
        {
            return Ok(await _mediator.Send(new DeleteContact() { Code = code }));
        }
    }
}
