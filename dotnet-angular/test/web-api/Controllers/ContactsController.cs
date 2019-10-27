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

            // Retrieve existing contact
            Contact contact = await this.bll.GetContactByIdAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

			// Mapping
            Contact contactUpdate = this.mapper.Map<ContactVM, Contact>(contactVM);

			// Update fields
            contact.FirstName = contactUpdate.FirstName;
            contact.LastName = contactUpdate.LastName;
			
            contact = await this.bll.UpdateContactAsync(id, contact);

			return this.mapper.Map<Contact, ContactVM>(contact);
        }

		// TODO
        //// PUT: api/Skills/5/SkillTags/123/Link
        //[HttpPut("{skillId}/SkillTags/{skillTagId}/Link")]
        //public async Task<ActionResult<SkillVM>> LinkSkillTagToSkill([FromRoute] Guid skillId, [FromRoute] Guid skillTagId)
        //{
        //    Skill skill = await context.Skills
        //                        .Include(s => s.SkillSkillTag)
        //                            .ThenInclude(cs => cs.SkillTag)
        //                        .SingleOrDefaultAsync(s => s.Id == skillId);
        //    if (skill == null)
        //    {
        //        return NotFound("Skill not found");
        //    }

        //    SkillTag skillTag = await context.SkillTags.FindAsync(skillTagId);
        //    if (skillTag == null)
        //    {
        //        return NotFound("Skill tag not found");
        //    }

        //    // Retrieve existing link
        //    SkillSkillTag skillSkillTag = await context.SkillSkillTag
        //                                    .Include(cs => cs.SkillTag)
        //                                    .Where(cs => cs.SkillId == skill.Id && cs.SkillTagId == skillTag.Id)
        //                                    .SingleOrDefaultAsync();
        //    if (skillSkillTag != null)
        //    {
        //        // Link already exists

        //        // Update in local
        //        int skillSkillTagIndex = skill.SkillSkillTag.IndexOf(skillSkillTag);

        //        // Update link
        //        skillSkillTag.DateModified = DateTime.Now;

        //        context.SkillSkillTag.Update(skillSkillTag);
        //        await context.SaveChangesAsync();

        //        // Update in local
        //        if (skillSkillTagIndex != -1)
        //            skill.SkillSkillTag[skillSkillTagIndex] = skillSkillTag;
        //    }
        //    else
        //    {
        //        // Link doesn't exist yet

        //        // Add link
        //        skillSkillTag = new SkillSkillTag();
        //        skillSkillTag.Id = Guid.NewGuid();
        //        skillSkillTag.SkillId = skill.Id;
        //        skillSkillTag.SkillTagId = skillTag.Id;
        //        skillSkillTag.SkillTag = skillTag;
        //        skillSkillTag.DateEntered = DateTime.Now;
        //        skillSkillTag.DateModified = DateTime.Now;

        //        await context.SkillSkillTag.AddAsync(skillSkillTag);
        //        await context.SaveChangesAsync();
        //    }

        //    return Ok(this.mapper.Map<Skill, SkillVM>(skill));
        //}

        // TODO
        //// DELETE: api/Skills/5/SkillTags/123/Unlink
        //[HttpDelete("{skillId}/SkillTags/{skillTagId}/Unlink")]
        //public async Task<ActionResult<SkillVM>> UnlinkSkillTagFromSkill([FromRoute] Guid skillId, [FromRoute] Guid skillTagId)
        //{
        //    Skill skill = await context.Skills
        //                        .Include(s => s.SkillSkillTag)
        //                            .ThenInclude(cs => cs.SkillTag)
        //                        .SingleOrDefaultAsync(s => s.Id == skillId);
        //    if (skill == null)
        //    {
        //        return NotFound("Skill not found");
        //    }

        //    SkillTag skillTag = await context.SkillTags.FindAsync(skillTagId);
        //    if (skillTag == null)
        //    {
        //        return NotFound("Skill tag not found");
        //    }

        //    // Retrieve existing link
        //    SkillSkillTag skillSkillTag = await context.SkillSkillTag
        //                                    .Include(cs => cs.SkillTag)
        //                                    .Where(cs => cs.SkillId == skill.Id && cs.SkillTagId == skillTag.Id)
        //                                    .SingleOrDefaultAsync();
        //    if (skillSkillTag != null)
        //    {
        //        // Link exists

        //        // Remove from local
        //        int skillSkillTagIndex = skill.SkillSkillTag.IndexOf(skillSkillTag);

        //        // Remove link
        //        context.SkillSkillTag.Remove(skillSkillTag);
        //        await context.SaveChangesAsync();

        //        // Remove from local
        //        if (skillSkillTagIndex != -1)
        //            skill.SkillSkillTag.RemoveAt(skillSkillTagIndex);
        //    }

        //    return Ok(this.mapper.Map<Skill, SkillVM>(skill));
        //}

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
