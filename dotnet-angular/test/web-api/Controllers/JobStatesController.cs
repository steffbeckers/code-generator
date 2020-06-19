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
	/// The JobStates controller.
	/// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
	[Produces("application/json")]
    public class JobStatesController : ControllerBase
    {
        private readonly ILogger<JobStatesController> logger;
        private readonly IMapper mapper;
        private readonly JobStateBLL bll;

		/// <summary>
		/// The constructor of the JobStates controller.
		/// </summary>
        public JobStatesController(
            ILogger<JobStatesController> logger,
			IMapper mapper,
            JobStateBLL bll
        )
        {
            this.logger = logger;
			this.mapper = mapper;
            this.bll = bll;
        }

        // GET: api/jobstates
		/// <summary>
		/// Retrieves all jobstates.
		/// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobStateVM>>> GetJobStates()
        {
            IEnumerable<JobState> jobstates = await this.bll.GetAllJobStatesAsync();

			// Mapping
            return Ok(this.mapper.Map<IEnumerable<JobState>, List<JobStateVM>>(jobstates));
        }

        // GET: api/jobstates/{id}
		/// <summary>
		/// Retrieves a specific jobstate.
		/// </summary>
		/// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<ActionResult<JobStateVM>> GetJobState([FromRoute] Guid id)
        {
            JobState jobstate = await this.bll.GetJobStateByIdAsync(id);
            if (jobstate == null)
            {
                return NotFound();
            }

			// Mapping
            return Ok(this.mapper.Map<JobState, JobStateVM>(jobstate));
        }

        // POST: api/jobstates
		/// <summary>
		/// Creates a new jobstate.
		/// </summary>
		/// <param name="jobstateVM"></param>
        [HttpPost]
        public async Task<ActionResult<JobStateVM>> CreateJobState([FromBody] JobStateVM jobstateVM)
        {
			// Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Mapping
            JobState jobstate = this.mapper.Map<JobStateVM, JobState>(jobstateVM);

            jobstate = await this.bll.CreateJobStateAsync(jobstate);

			// Mapping
            return CreatedAtAction(
				"GetJobState",
				new { id = jobstate.Id },
				this.mapper.Map<JobState, JobStateVM>(jobstate)
			);
        }

		// PUT: api/jobstates/{id}
		/// <summary>
		/// Updates a specific jobstate.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="jobStateVM"></param>
        [HttpPut("{id}")]
        public async Task<ActionResult<JobStateVM>> UpdateJobState([FromRoute] Guid id, [FromBody] JobStateVM jobStateVM)
        {
			// Validation
            if (!ModelState.IsValid || id != jobStateVM.Id)
            {
                return BadRequest(ModelState);
            }

			// Mapping
            JobState jobState = this.mapper.Map<JobStateVM, JobState>(jobStateVM);

            jobState = await this.bll.UpdateJobStateAsync(jobState);

			// Mapping
			return Ok(this.mapper.Map<JobState, JobStateVM>(jobState));
        }

        // DELETE: api/jobstates/{id}
		/// <summary>
		/// Deletes a specific jobstate.
		/// </summary>
		/// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<JobStateVM>> DeleteJobState([FromRoute] Guid id)
        {
            // Retrieve existing jobstate
            JobState jobstate = await this.bll.GetJobStateByIdAsync(id);
            if (jobstate == null)
            {
                return NotFound();
            }

            await this.bll.DeleteJobStateAsync(jobstate);

            // Mapping
            return Ok(this.mapper.Map<JobState, JobStateVM>(jobstate));
        }
    }
}
