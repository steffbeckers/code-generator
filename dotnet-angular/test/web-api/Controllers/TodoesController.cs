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
	/// The Todoes controller.
	/// </summary>
    [Route("api/[controller]")]
    [ApiController]
	[Produces("application/json")]
    public class TodoesController : ControllerBase
    {
        private readonly ILogger<TodoesController> logger;
        private readonly IMapper mapper;
        private readonly TodoBLL bll;

		/// <summary>
		/// The constructor of the Todoes controller.
		/// </summary>
        public TodoesController(
            ILogger<TodoesController> logger,
			IMapper mapper,
            TodoBLL bll
        )
        {
            this.logger = logger;
			this.mapper = mapper;
            this.bll = bll;
        }

        // GET: api/Todoes
		/// <summary>
		/// Retrieves all todoes.
		/// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoVM>>> GetTodoes()
        {
            IEnumerable<Todo> todoes = await this.bll.GetAllTodoesAsync();

            return this.mapper.Map<IEnumerable<Todo>, List<TodoVM>>(todoes);
        }

        // GET: api/Todoes/{id}
		/// <summary>
		/// Retrieves a specific todo.
		/// </summary>
		/// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoVM>> GetTodo([FromRoute] Guid id)
        {
            Todo todo = await this.bll.GetTodoByIdAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            return this.mapper.Map<Todo, TodoVM>(todo);
        }

        // POST: api/Todoes
		/// <summary>
		/// Creates a new todo.
		/// </summary>
		/// <param name="todoVM"></param>
        [HttpPost]
        public async Task<ActionResult<TodoVM>> CreateTodo([FromBody] TodoVM todoVM)
        {
			// Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Mapping
            Todo todo = this.mapper.Map<TodoVM, Todo>(todoVM);

            todo = await this.bll.CreateTodoAsync(todo);

            return CreatedAtAction(
				"GetTodo",
				new { id = todo.Id },
				this.mapper.Map<Todo, TodoVM>(todo)
			);
        }

		// PUT: api/Todoes/{id}
		/// <summary>
		/// Updates a specific todo.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="todoVM"></param>
        [HttpPut("{id}")]
        public async Task<ActionResult<TodoVM>> UpdateTodo([FromRoute] Guid id, [FromBody] TodoVM todoVM)
        {
			// Validation
            if (!ModelState.IsValid || id != todoVM.Id)
            {
                return BadRequest(ModelState);
            }

            // Retrieve existing todo
            Todo todo = await this.bll.GetTodoByIdAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

			// Mapping
            Todo todoUpdate = this.mapper.Map<TodoVM, Todo>(todoVM);

			// Update fields
            todo.Title = todoUpdate.Title;
            todo.Body = todoUpdate.Body;
			
            todo = await this.bll.UpdateTodoAsync(id, todo);

			return this.mapper.Map<Todo, TodoVM>(todo);
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

        // DELETE: api/Todoes/{id}
		/// <summary>
		/// Deletes a specific todo.
		/// </summary>
		/// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoVM>> DeleteTodo([FromRoute] Guid id)
        {
            // Retrieve existing todo
            Todo todo = await this.bll.GetTodoByIdAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            await this.bll.DeleteTodoAsync(todo);

            return this.mapper.Map<Todo, TodoVM>(todo);
        }
    }
}
