using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RJM.API.BLL;
using RJM.API.Models;
using RJM.API.ViewModels;

namespace RJM.API.Controllers
{
	/// <summary>
	/// The Skills controller.
	/// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
	[Produces("application/json")]
    public class SkillsController : ControllerBase
    {
        private readonly ILogger<SkillsController> logger;
        private readonly IMapper mapper;
        private readonly SkillBLL bll;

		/// <summary>
		/// The constructor of the Skills controller.
		/// </summary>
        public SkillsController(
            ILogger<SkillsController> logger,
			IMapper mapper,
            SkillBLL bll
        )
        {
            this.logger = logger;
			this.mapper = mapper;
            this.bll = bll;
        }

        // GET: api/skills
		/// <summary>
		/// Retrieves all skills.
		/// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkillVM>>> GetSkills()
        {
            IEnumerable<Skill> skills = await this.bll.GetAllSkillsAsync();

			// Mapping
            return Ok(this.mapper.Map<IEnumerable<Skill>, List<SkillVM>>(skills));
        }

        // GET: api/skills/{id}
		/// <summary>
		/// Retrieves a specific skill.
		/// </summary>
		/// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<ActionResult<SkillVM>> GetSkill([FromRoute] Guid id)
        {
            Skill skill = await this.bll.GetSkillByIdAsync(id);
            if (skill == null)
            {
                return NotFound();
            }

			// Mapping
            return Ok(this.mapper.Map<Skill, SkillVM>(skill));
        }

        // POST: api/skills
		/// <summary>
		/// Creates a new skill.
		/// </summary>
		/// <param name="skillVM"></param>
        [HttpPost]
        public async Task<ActionResult<SkillVM>> CreateSkill([FromBody] SkillVM skillVM)
        {
			// Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Mapping
            Skill skill = this.mapper.Map<SkillVM, Skill>(skillVM);

            skill = await this.bll.CreateSkillAsync(skill);

			// Mapping
            return CreatedAtAction(
				"GetSkill",
				new { id = skill.Id },
				this.mapper.Map<Skill, SkillVM>(skill)
			);
        }

		// PUT: api/skills/{id}
		/// <summary>
		/// Updates a specific skill.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="skillVM"></param>
        [HttpPut("{id}")]
        public async Task<ActionResult<SkillVM>> UpdateSkill([FromRoute] Guid id, [FromBody] SkillVM skillVM)
        {
			// Validation
            if (!ModelState.IsValid || id != skillVM.Id)
            {
                return BadRequest(ModelState);
            }

			// Mapping
            Skill skill = this.mapper.Map<SkillVM, Skill>(skillVM);

            skill = await this.bll.UpdateSkillAsync(skill);

			// Mapping
			return Ok(this.mapper.Map<Skill, SkillVM>(skill));
        }

        // DELETE: api/skills/{id}
		/// <summary>
		/// Deletes a specific skill.
		/// </summary>
		/// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<SkillVM>> DeleteSkill([FromRoute] Guid id)
        {
            // Retrieve existing skill
            Skill skill = await this.bll.GetSkillByIdAsync(id);
            if (skill == null)
            {
                return NotFound();
            }

            await this.bll.DeleteSkillAsync(skill);

            // Mapping
            return Ok(this.mapper.Map<Skill, SkillVM>(skill));
        }
    }
}
