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
	/// The Notes controller.
	/// </summary>
    [Route("api/[controller]")]
    [ApiController]
	[Produces("application/json")]
    public class NotesController : ControllerBase
    {
        private readonly ILogger<NotesController> logger;
        private readonly IMapper mapper;
        private readonly NoteBLL bll;

		/// <summary>
		/// The constructor of the Notes controller.
		/// </summary>
        public NotesController(
            ILogger<NotesController> logger,
			IMapper mapper,
            NoteBLL bll
        )
        {
            this.logger = logger;
			this.mapper = mapper;
            this.bll = bll;
        }

        // GET: api/Notes
		/// <summary>
		/// Retrieves all notes.
		/// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NoteVM>>> GetNotes()
        {
            IEnumerable<Note> notes = await this.bll.GetAllNotesAsync();

            return this.mapper.Map<IEnumerable<Note>, List<NoteVM>>(notes);
        }

        // GET: api/Notes/{id}
		/// <summary>
		/// Retrieves a specific note.
		/// </summary>
		/// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<ActionResult<NoteVM>> GetNote([FromRoute] Guid id)
        {
            Note note = await this.bll.GetNoteByIdAsync(id);
            if (note == null)
            {
                return NotFound();
            }

            return this.mapper.Map<Note, NoteVM>(note);
        }

        // POST: api/Notes
		/// <summary>
		/// Creates a new note.
		/// </summary>
		/// <param name="noteVM"></param>
        [HttpPost]
        public async Task<ActionResult<NoteVM>> CreateNote([FromBody] NoteVM noteVM)
        {
			// Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Mapping
            Note note = this.mapper.Map<NoteVM, Note>(noteVM);

            note = await this.bll.CreateNoteAsync(note);

            return CreatedAtAction(
				"GetNote",
				new { id = note.Id },
				this.mapper.Map<Note, NoteVM>(note)
			);
        }

		// PUT: api/Notes/{id}
		/// <summary>
		/// Updates a specific note.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="noteVM"></param>
        [HttpPut("{id}")]
        public async Task<ActionResult<NoteVM>> UpdateNote([FromRoute] Guid id, [FromBody] NoteVM noteVM)
        {
			// Validation
            if (!ModelState.IsValid || id != noteVM.Id)
            {
                return BadRequest(ModelState);
            }

            // Retrieve existing note
            Note note = await this.bll.GetNoteByIdAsync(id);
            if (note == null)
            {
                return NotFound();
            }

			// Mapping
            Note noteUpdate = this.mapper.Map<NoteVM, Note>(noteVM);

			// Update fields
            note.Title = noteUpdate.Title;
            note.Body = noteUpdate.Body;
			
            note = await this.bll.UpdateNoteAsync(id, note);

			return this.mapper.Map<Note, NoteVM>(note);
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

        // DELETE: api/Notes/{id}
		/// <summary>
		/// Deletes a specific note.
		/// </summary>
		/// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<NoteVM>> DeleteNote([FromRoute] Guid id)
        {
            // Retrieve existing note
            Note note = await this.bll.GetNoteByIdAsync(id);
            if (note == null)
            {
                return NotFound();
            }

            await this.bll.DeleteNoteAsync(note);

            return this.mapper.Map<Note, NoteVM>(note);
        }
    }
}
