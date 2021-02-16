using CodeGenOutput.API.Requests;
using CodeGenOutput.API.Requests.Contacts;
using CodeGenOutput.API.ViewModels;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
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
        public async Task<IActionResult> GetContacts([FromQuery] string include = "")
        {
            return Ok(await _mediator.Send(new GetContacts() { Include = include }));
        }

        // GET: api/contacts/{id}
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetContactById([FromRoute] Guid id, [FromQuery] string include = "")
        {
            return Ok(await _mediator.Send(new GetContactById() { Id = id, Include = include }));
        }

        // POST: api/contacts
        [HttpPost]
        public async Task<IActionResult> CreateContact([FromBody] ContactCreateVM contactCreateVM)
        {
            Response response = await _mediator.Send(new CreateContact() { ContactCreateVM = contactCreateVM });
            return CreatedAtAction("GetContactById", new { id = (response.Data as ContactVM).Id }, response);
        }

        // PUT: api/contacts/{id}
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, [FromBody] ContactUpdateVM contactUpdateVM)
        {
            if (id != contactUpdateVM.Id) { return BadRequest(); }
            return Ok(await _mediator.Send(new UpdateContact() { ContactUpdateVM = contactUpdateVM }));
        }

        // PATCH: api/contacts/{id}
        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> PatchContact([FromRoute] Guid id, [FromBody] JsonPatchDocument<ContactUpdateVM> contactPatchDocument)
        {
            return Ok(await _mediator.Send(new PatchContact() { Id = id, PatchDocument = contactPatchDocument }));
        }

        // DELETE: api/contacts/{id}
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            return Ok(await _mediator.Send(new DeleteContact() { Id = id }));
        }
    }
}
