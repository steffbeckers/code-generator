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
	/// The Calls controller.
	/// </summary>
    [Route("api/[controller]")]
    [ApiController]
	[Produces("application/json")]
    public class CallsController : ControllerBase
    {
        private readonly ILogger<CallsController> logger;
        private readonly IMapper mapper;
        private readonly CallBLL bll;

		/// <summary>
		/// The constructor of the Calls controller.
		/// </summary>
        public CallsController(
            ILogger<CallsController> logger,
			IMapper mapper,
            CallBLL bll
        )
        {
            this.logger = logger;
			this.mapper = mapper;
            this.bll = bll;
        }

        // GET: api/Calls
		/// <summary>
		/// Retrieves all calls.
		/// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CallVM>>> GetCalls()
        {
            IEnumerable<Call> calls = await this.bll.GetAllCallsAsync();

            return this.mapper.Map<IEnumerable<Call>, List<CallVM>>(calls);
        }

        // GET: api/Calls/{id}
		/// <summary>
		/// Retrieves a specific call.
		/// </summary>
		/// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<ActionResult<CallVM>> GetCall([FromRoute] Guid id)
        {
            Call call = await this.bll.GetCallByIdAsync(id);
            if (call == null)
            {
                return NotFound();
            }

            return this.mapper.Map<Call, CallVM>(call);
        }

        // POST: api/Calls
		/// <summary>
		/// Creates a new call.
		/// </summary>
		/// <param name="callVM"></param>
        [HttpPost]
        public async Task<ActionResult<CallVM>> CreateCall([FromBody] CallVM callVM)
        {
			// Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Mapping
            Call call = this.mapper.Map<CallVM, Call>(callVM);

            call = await this.bll.CreateCallAsync(call);

            return CreatedAtAction(
				"GetCall",
				new { id = call.Id },
				this.mapper.Map<Call, CallVM>(call)
			);
        }

		// PUT: api/Calls/{id}
		/// <summary>
		/// Updates a specific call.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="callVM"></param>
        [HttpPut("{id}")]
        public async Task<ActionResult<CallVM>> UpdateCall([FromRoute] Guid id, [FromBody] CallVM callVM)
        {
			// Validation
            if (!ModelState.IsValid || id != callVM.Id)
            {
                return BadRequest(ModelState);
            }

            // Retrieve existing call
            Call call = await this.bll.GetCallByIdAsync(id);
            if (call == null)
            {
                return NotFound();
            }

			// Mapping
            Call callUpdate = this.mapper.Map<CallVM, Call>(callVM);

			// Update fields
            call.Date = callUpdate.Date;
			
            call = await this.bll.UpdateCallAsync(id, call);

			return this.mapper.Map<Call, CallVM>(call);
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

        // DELETE: api/Calls/{id}
		/// <summary>
		/// Deletes a specific call.
		/// </summary>
		/// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<CallVM>> DeleteCall([FromRoute] Guid id)
        {
            // Retrieve existing call
            Call call = await this.bll.GetCallByIdAsync(id);
            if (call == null)
            {
                return NotFound();
            }

            await this.bll.DeleteCallAsync(call);

            return this.mapper.Map<Call, CallVM>(call);
        }
    }
}
