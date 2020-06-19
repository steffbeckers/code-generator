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
	/// The ResumeStates controller.
	/// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
	[Produces("application/json")]
    public class ResumeStatesController : ControllerBase
    {
        private readonly ILogger<ResumeStatesController> logger;
        private readonly IMapper mapper;
        private readonly ResumeStateBLL bll;

		/// <summary>
		/// The constructor of the ResumeStates controller.
		/// </summary>
        public ResumeStatesController(
            ILogger<ResumeStatesController> logger,
			IMapper mapper,
            ResumeStateBLL bll
        )
        {
            this.logger = logger;
			this.mapper = mapper;
            this.bll = bll;
        }

        // GET: api/resumestates
		/// <summary>
		/// Retrieves all resumestates.
		/// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResumeStateVM>>> GetResumeStates()
        {
            IEnumerable<ResumeState> resumestates = await this.bll.GetAllResumeStatesAsync();

			// Mapping
            return Ok(this.mapper.Map<IEnumerable<ResumeState>, List<ResumeStateVM>>(resumestates));
        }

        // GET: api/resumestates/{id}
		/// <summary>
		/// Retrieves a specific resumestate.
		/// </summary>
		/// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<ActionResult<ResumeStateVM>> GetResumeState([FromRoute] Guid id)
        {
            ResumeState resumestate = await this.bll.GetResumeStateByIdAsync(id);
            if (resumestate == null)
            {
                return NotFound();
            }

			// Mapping
            return Ok(this.mapper.Map<ResumeState, ResumeStateVM>(resumestate));
        }

        // POST: api/resumestates
		/// <summary>
		/// Creates a new resumestate.
		/// </summary>
		/// <param name="resumestateVM"></param>
        [HttpPost]
        public async Task<ActionResult<ResumeStateVM>> CreateResumeState([FromBody] ResumeStateVM resumestateVM)
        {
			// Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Mapping
            ResumeState resumestate = this.mapper.Map<ResumeStateVM, ResumeState>(resumestateVM);

            resumestate = await this.bll.CreateResumeStateAsync(resumestate);

			// Mapping
            return CreatedAtAction(
				"GetResumeState",
				new { id = resumestate.Id },
				this.mapper.Map<ResumeState, ResumeStateVM>(resumestate)
			);
        }

		// PUT: api/resumestates/{id}
		/// <summary>
		/// Updates a specific resumestate.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="resumeStateVM"></param>
        [HttpPut("{id}")]
        public async Task<ActionResult<ResumeStateVM>> UpdateResumeState([FromRoute] Guid id, [FromBody] ResumeStateVM resumeStateVM)
        {
			// Validation
            if (!ModelState.IsValid || id != resumeStateVM.Id)
            {
                return BadRequest(ModelState);
            }

			// Mapping
            ResumeState resumeState = this.mapper.Map<ResumeStateVM, ResumeState>(resumeStateVM);

            resumeState = await this.bll.UpdateResumeStateAsync(resumeState);

			// Mapping
			return Ok(this.mapper.Map<ResumeState, ResumeStateVM>(resumeState));
        }

        // DELETE: api/resumestates/{id}
		/// <summary>
		/// Deletes a specific resumestate.
		/// </summary>
		/// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResumeStateVM>> DeleteResumeState([FromRoute] Guid id)
        {
            // Retrieve existing resumestate
            ResumeState resumestate = await this.bll.GetResumeStateByIdAsync(id);
            if (resumestate == null)
            {
                return NotFound();
            }

            await this.bll.DeleteResumeStateAsync(resumestate);

            // Mapping
            return Ok(this.mapper.Map<ResumeState, ResumeStateVM>(resumestate));
        }
    }
}
