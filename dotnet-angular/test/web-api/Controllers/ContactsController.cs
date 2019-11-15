using AutoMapper;
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
	/// The Contacts controller.
	/// </summary>
    [Route("api/[controller]")]
    [ApiController]
	[Produces("application/json")]
    public class ContactsController : ControllerBase
    {
        private readonly ILogger<ContactsController> logger;
        private readonly IMapper mapper;
        private readonly ContactBLL bll;

		/// <summary>
		/// The constructor of the Contacts controller.
		/// </summary>
        public ContactsController(
            ILogger<ContactsController> logger,
			IMapper mapper,
            ContactBLL bll
        )
        {
            this.logger = logger;
			this.mapper = mapper;
            this.bll = bll;
        }

        // GET: api/Contacts
		/// <summary>
		/// Retrieves all contacts.
		/// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactVM>>> GetContacts()
        {
            IEnumerable<Contact> contacts = await this.bll.GetAllContactsAsync();

			// Mapping
            return this.mapper.Map<IEnumerable<Contact>, List<ContactVM>>(contacts);
        }

        // GET: api/Contacts/{id}
		/// <summary>
		/// Retrieves a specific contact.
		/// </summary>
		/// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactVM>> GetContact([FromRoute] Guid id)
        {
            Contact contact = await this.bll.GetContactByIdAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

			// Mapping
            return this.mapper.Map<Contact, ContactVM>(contact);
        }

        // POST: api/Contacts
		/// <summary>
		/// Creates a new contact.
		/// </summary>
		/// <param name="contactVM"></param>
        [HttpPost]
        public async Task<ActionResult<ContactVM>> CreateContact([FromBody] ContactVM contactVM)
        {
			// Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Mapping
            Contact contact = this.mapper.Map<ContactVM, Contact>(contactVM);

            contact = await this.bll.CreateContactAsync(contact);

			// Mapping
            return CreatedAtAction(
				"GetContact",
				new { id = contact.Id },
				this.mapper.Map<Contact, ContactVM>(contact)
			);
        }

		// PUT: api/Contacts/{id}
		/// <summary>
		/// Updates a specific contact.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="contactVM"></param>
        [HttpPut("{id}")]
        public async Task<ActionResult<ContactVM>> UpdateContact([FromRoute] Guid id, [FromBody] ContactVM contactVM)
        {
			// Validation
            if (!ModelState.IsValid || id != contactVM.Id)
            {
                return BadRequest(ModelState);
            }

			// Mapping
            Contact contact = this.mapper.Map<ContactVM, Contact>(contactVM);

            contact = await this.bll.UpdateContactAsync(contact);

			// Mapping
			return this.mapper.Map<Contact, ContactVM>(contact);
        }

        // DELETE: api/Contacts/{id}
		/// <summary>
		/// Deletes a specific contact.
		/// </summary>
		/// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ContactVM>> DeleteContact([FromRoute] Guid id)
        {
            // Retrieve existing contact
            Contact contact = await this.bll.GetContactByIdAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            await this.bll.DeleteContactAsync(contact);

            return this.mapper.Map<Contact, ContactVM>(contact);
        }
    }
}
