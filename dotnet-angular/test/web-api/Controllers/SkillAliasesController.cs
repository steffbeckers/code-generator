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
	/// The SkillAliases controller.
	/// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
	[Produces("application/json")]
    public class SkillAliasesController : ControllerBase
    {
        private readonly ILogger<SkillAliasesController> logger;
        private readonly IMapper mapper;
        private readonly SkillAliasBLL bll;

		/// <summary>
		/// The constructor of the SkillAliases controller.
		/// </summary>
        public SkillAliasesController(
            ILogger<SkillAliasesController> logger,
			IMapper mapper,
            SkillAliasBLL bll
        )
        {
            this.logger = logger;
			this.mapper = mapper;
            this.bll = bll;
        }

        // GET: api/skillaliases
		/// <summary>
		/// Retrieves all skillaliases.
		/// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkillAliasVM>>> GetSkillAliases()
        {
            IEnumerable<SkillAlias> skillaliases = await this.bll.GetAllSkillAliasesAsync();

			// Mapping
            return Ok(this.mapper.Map<IEnumerable<SkillAlias>, List<SkillAliasVM>>(skillaliases));
        }

        // GET: api/skillaliases/{id}
		/// <summary>
		/// Retrieves a specific skillalias.
		/// </summary>
		/// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<ActionResult<SkillAliasVM>> GetSkillAlias([FromRoute] Guid id)
        {
            SkillAlias skillalias = await this.bll.GetSkillAliasByIdAsync(id);
            if (skillalias == null)
            {
                return NotFound();
            }

			// Mapping
            return Ok(this.mapper.Map<SkillAlias, SkillAliasVM>(skillalias));
        }

        // POST: api/skillaliases
		/// <summary>
		/// Creates a new skillalias.
		/// </summary>
		/// <param name="skillaliasVM"></param>
        [HttpPost]
        public async Task<ActionResult<SkillAliasVM>> CreateSkillAlias([FromBody] SkillAliasVM skillaliasVM)
        {
			// Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Mapping
            SkillAlias skillalias = this.mapper.Map<SkillAliasVM, SkillAlias>(skillaliasVM);

            skillalias = await this.bll.CreateSkillAliasAsync(skillalias);

			// Mapping
            return CreatedAtAction(
				"GetSkillAlias",
				new { id = skillalias.Id },
				this.mapper.Map<SkillAlias, SkillAliasVM>(skillalias)
			);
        }

		// PUT: api/skillaliases/{id}
		/// <summary>
		/// Updates a specific skillalias.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="skillAliasVM"></param>
        [HttpPut("{id}")]
        public async Task<ActionResult<SkillAliasVM>> UpdateSkillAlias([FromRoute] Guid id, [FromBody] SkillAliasVM skillAliasVM)
        {
			// Validation
            if (!ModelState.IsValid || id != skillAliasVM.Id)
            {
                return BadRequest(ModelState);
            }

			// Mapping
            SkillAlias skillAlias = this.mapper.Map<SkillAliasVM, SkillAlias>(skillAliasVM);

            skillAlias = await this.bll.UpdateSkillAliasAsync(skillAlias);

			// Mapping
			return Ok(this.mapper.Map<SkillAlias, SkillAliasVM>(skillAlias));
        }

        // DELETE: api/skillaliases/{id}
		/// <summary>
		/// Deletes a specific skillalias.
		/// </summary>
		/// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<SkillAliasVM>> DeleteSkillAlias([FromRoute] Guid id)
        {
            // Retrieve existing skillalias
            SkillAlias skillalias = await this.bll.GetSkillAliasByIdAsync(id);
            if (skillalias == null)
            {
                return NotFound();
            }

            await this.bll.DeleteSkillAliasAsync(skillalias);

            // Mapping
            return Ok(this.mapper.Map<SkillAlias, SkillAliasVM>(skillalias));
        }
    }
}
