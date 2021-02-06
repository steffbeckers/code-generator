using CodeGenOutput.API.BLL;
using CodeGenOutput.Models;
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
        private readonly IContactBLL _bll;

        public ContactsController(IBusinessLogicLayer bll)
        {
            _bll = bll;
        }

        // GET: api/contacts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
        {
            return Ok(await _bll.GetContactsAsync());
        }

        // GET: api/contacts/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContactById([FromRoute] Guid id)
        {
            return Ok(await _bll.GetContactByIdAsync(id));
        }

        // GET: api/contacts/search
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Contact>>> SearchContact([FromQuery] string term)
        {
            return Ok(await _bll.SearchContactAsync(term));
        }

        // POST: api/contacts
        [HttpPost]
        public async Task<ActionResult<Contact>> CreateContact([FromBody] Contact contact)
        {
            Contact createdContact = await _bll.CreateContactAsync(contact);
            return CreatedAtAction("GetContactById", new { id = createdContact.Id }, createdContact);
        }

        // PUT: api/contacts/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Contact>> UpdateContact([FromRoute] Guid id, [FromBody] Contact contact)
        {
            if (id != contact.Id) { return BadRequest(); }
            return Ok(await _bll.UpdateContactAsync(contact));
        }

        // DELETE: api/contacts/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(Guid id)
        {
            await _bll.DeleteContactAsync(new Contact() { Id = id });
            return NoContent();
        }
    }
}
