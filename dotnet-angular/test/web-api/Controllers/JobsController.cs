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
	/// The Jobs controller.
	/// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
	[Produces("application/json")]
    public class JobsController : ControllerBase
    {
        private readonly ILogger<JobsController> logger;
        private readonly IMapper mapper;
        private readonly JobBLL bll;

		/// <summary>
		/// The constructor of the Jobs controller.
		/// </summary>
        public JobsController(
            ILogger<JobsController> logger,
			IMapper mapper,
            JobBLL bll
        )
        {
            this.logger = logger;
			this.mapper = mapper;
            this.bll = bll;
        }

        // GET: api/jobs
		/// <summary>
		/// Retrieves all jobs.
		/// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobVM>>> GetJobs()
        {
            IEnumerable<Job> jobs = await this.bll.GetAllJobsAsync();

			// Mapping
            return Ok(this.mapper.Map<IEnumerable<Job>, List<JobVM>>(jobs));
        }

        // GET: api/jobs/{id}
		/// <summary>
		/// Retrieves a specific job.
		/// </summary>
		/// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<ActionResult<JobVM>> GetJob([FromRoute] Guid id)
        {
            Job job = await this.bll.GetJobByIdAsync(id);
            if (job == null)
            {
                return NotFound();
            }

			// Mapping
            return Ok(this.mapper.Map<Job, JobVM>(job));
        }

        // POST: api/jobs
		/// <summary>
		/// Creates a new job.
		/// </summary>
		/// <param name="jobVM"></param>
        [HttpPost]
        public async Task<ActionResult<JobVM>> CreateJob([FromBody] JobVM jobVM)
        {
			// Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Mapping
            Job job = this.mapper.Map<JobVM, Job>(jobVM);

            job = await this.bll.CreateJobAsync(job);

			// Mapping
            return CreatedAtAction(
				"GetJob",
				new { id = job.Id },
				this.mapper.Map<Job, JobVM>(job)
			);
        }

		// PUT: api/jobs/{id}
		/// <summary>
		/// Updates a specific job.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="jobVM"></param>
        [HttpPut("{id}")]
        public async Task<ActionResult<JobVM>> UpdateJob([FromRoute] Guid id, [FromBody] JobVM jobVM)
        {
			// Validation
            if (!ModelState.IsValid || id != jobVM.Id)
            {
                return BadRequest(ModelState);
            }

			// Mapping
            Job job = this.mapper.Map<JobVM, Job>(jobVM);

            job = await this.bll.UpdateJobAsync(job);

			// Mapping
			return Ok(this.mapper.Map<Job, JobVM>(job));
        }

        // PUT: api/jobs/skills/link
		/// <summary>
		/// Links a specific skill to job.
		/// </summary>
		/// <param name="jobSkill"></param>
        [HttpPut("Skills/Link")]
        public async Task<ActionResult<JobVM>> LinkSkillToJob([FromBody] JobSkill jobSkill)
        {
			// Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Job job = await this.bll.LinkSkillToJobAsync(jobSkill);

            // Mapping
            return Ok(this.mapper.Map<Job, JobVM>(job));
        }

        // PUT: api/jobs/skills/unlink
		/// <summary>
		/// Unlinks a specific skill from job.
		/// </summary>
		/// <param name="jobSkill"></param>
        [HttpPut("Skills/Unlink")]
        public async Task<ActionResult<JobVM>> UnlinkSkillFromJob([FromBody] JobSkill jobSkill)
        {
			// Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Job job = await this.bll.UnlinkSkillFromJobAsync(jobSkill);

            // Mapping
            return Ok(this.mapper.Map<Job, JobVM>(job));
        }

        // DELETE: api/jobs/{id}
		/// <summary>
		/// Deletes a specific job.
		/// </summary>
		/// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<JobVM>> DeleteJob([FromRoute] Guid id)
        {
            // Retrieve existing job
            Job job = await this.bll.GetJobByIdAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            await this.bll.DeleteJobAsync(job);

            // Mapping
            return Ok(this.mapper.Map<Job, JobVM>(job));
        }
    }
}
