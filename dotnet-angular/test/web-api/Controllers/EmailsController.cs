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
    /// The Emails controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class EmailsController : ControllerBase
    {
        private readonly ILogger<EmailsController> logger;
        private readonly IMapper mapper;
        private readonly EmailBLL bll;

        /// <summary>
        /// The constructor of the Emails controller.
        /// </summary>
        public EmailsController(
            ILogger<EmailsController> logger,
            IMapper mapper,
            EmailBLL bll
        )
        {
            this.logger = logger;
            this.mapper = mapper;
            this.bll = bll;
        }

        // GET: api/Emails
        /// <summary>
        /// Retrieves all emails.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmailVM>>> GetEmails()
        {
            IEnumerable<Email> emails = await this.bll.GetAllEmailsAsync();

            return this.mapper.Map<IEnumerable<Email>, List<EmailVM>>(emails);
        }

        // GET: api/Emails/{id}
        /// <summary>
        /// Retrieves a specific email.
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<ActionResult<EmailVM>> GetEmail([FromRoute] Guid id)
        {
            Email email = await this.bll.GetEmailByIdAsync(id);
            if (email == null)
            {
                return NotFound();
            }

            return this.mapper.Map<Email, EmailVM>(email);
        }

        // POST: api/Emails
        /// <summary>
        /// Creates a new email.
        /// </summary>
        /// <param name="emailVM"></param>
        [HttpPost]
        public async Task<ActionResult<EmailVM>> CreateEmail([FromBody] EmailVM emailVM)
        {
            // Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Mapping
            Email email = this.mapper.Map<EmailVM, Email>(emailVM);

            email = await this.bll.CreateEmailAsync(email);

            return CreatedAtAction(
                "GetEmail",
                new { id = email.Id },
                this.mapper.Map<Email, EmailVM>(email)
            );
        }

        // PUT: api/Emails/{id}
        /// <summary>
        /// Updates a specific email.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="emailVM"></param>
        [HttpPut("{id}")]
        public async Task<ActionResult<EmailVM>> UpdateEmail([FromRoute] Guid id, [FromBody] EmailVM emailVM)
        {
            // Validation
            if (!ModelState.IsValid || id != emailVM.Id)
            {
                return BadRequest(ModelState);
            }

            // Retrieve existing email
            Email email = await this.bll.GetEmailByIdAsync(id);
            if (email == null)
            {
                return NotFound();
            }

            // Mapping
            Email emailUpdate = this.mapper.Map<EmailVM, Email>(emailVM);

            // Update fields
            email.Subject = emailUpdate.Subject;
            email.Body = emailUpdate.Body;

            email = await this.bll.UpdateEmailAsync(id, email);

            return this.mapper.Map<Email, EmailVM>(email);
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

        // DELETE: api/Emails/{id}
        /// <summary>
        /// Deletes a specific email.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<EmailVM>> DeleteEmail([FromRoute] Guid id)
        {
            // Retrieve existing email
            Email email = await this.bll.GetEmailByIdAsync(id);
            if (email == null)
            {
                return NotFound();
            }

            await this.bll.DeleteEmailAsync(email);

            return this.mapper.Map<Email, EmailVM>(email);
        }
    }
}
