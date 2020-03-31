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
	/// The Resumes controller.
	/// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
	[Produces("application/json")]
    public class ResumesController : ControllerBase
    {
        private readonly ILogger<ResumesController> logger;
        private readonly IMapper mapper;
        private readonly ResumeBLL bll;

		/// <summary>
		/// The constructor of the Resumes controller.
		/// </summary>
        public ResumesController(
            ILogger<ResumesController> logger,
			IMapper mapper,
            ResumeBLL bll
        )
        {
            this.logger = logger;
			this.mapper = mapper;
            this.bll = bll;
        }

        // GET: api/resumes
		/// <summary>
		/// Retrieves all resumes.
		/// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResumeVM>>> GetResumes()
        {
            IEnumerable<Resume> resumes = await this.bll.GetAllResumesAsync();

			// Mapping
            return Ok(this.mapper.Map<IEnumerable<Resume>, List<ResumeVM>>(resumes));
        }

        // GET: api/resumes/{id}
		/// <summary>
		/// Retrieves a specific resume.
		/// </summary>
		/// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<ActionResult<ResumeVM>> GetResume([FromRoute] Guid id)
        {
            Resume resume = await this.bll.GetResumeByIdAsync(id);
            if (resume == null)
            {
                return NotFound();
            }

			// Mapping
            return Ok(this.mapper.Map<Resume, ResumeVM>(resume));
        }

        // POST: api/resumes
		/// <summary>
		/// Creates a new resume.
		/// </summary>
		/// <param name="resumeVM"></param>
        [HttpPost]
        public async Task<ActionResult<ResumeVM>> CreateResume([FromBody] ResumeVM resumeVM)
        {
			// Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Mapping
            Resume resume = this.mapper.Map<ResumeVM, Resume>(resumeVM);

            resume = await this.bll.CreateResumeAsync(resume);

			// Mapping
            return CreatedAtAction(
				"GetResume",
				new { id = resume.Id },
				this.mapper.Map<Resume, ResumeVM>(resume)
			);
        }

		// PUT: api/resumes/{id}
		/// <summary>
		/// Updates a specific resume.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="resumeVM"></param>
        [HttpPut("{id}")]
        public async Task<ActionResult<ResumeVM>> UpdateResume([FromRoute] Guid id, [FromBody] ResumeVM resumeVM)
        {
			// Validation
            if (!ModelState.IsValid || id != resumeVM.Id)
            {
                return BadRequest(ModelState);
            }

			// Mapping
            Resume resume = this.mapper.Map<ResumeVM, Resume>(resumeVM);

            resume = await this.bll.UpdateResumeAsync(resume);

			// Mapping
			return Ok(this.mapper.Map<Resume, ResumeVM>(resume));
        }

        // PUT: api/resumes/documents/link
		/// <summary>
		/// Links a specific document to resume.
		/// </summary>
		/// <param name="documentResume"></param>
        [HttpPut("Documents/Link")]
        public async Task<ActionResult<ResumeVM>> LinkDocumentToResume([FromBody] DocumentResume documentResume)
        {
			// Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Resume resume = await this.bll.LinkDocumentToResumeAsync(documentResume);

            // Mapping
            return Ok(this.mapper.Map<Resume, ResumeVM>(resume));
        }

        // PUT: api/resumes/documents/unlink
		/// <summary>
		/// Unlinks a specific document from resume.
		/// </summary>
		/// <param name="documentResume"></param>
        [HttpPut("Documents/Unlink")]
        public async Task<ActionResult<ResumeVM>> UnlinkDocumentFromResume([FromBody] DocumentResume documentResume)
        {
			// Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Resume resume = await this.bll.UnlinkDocumentFromResumeAsync(documentResume);

            // Mapping
            return Ok(this.mapper.Map<Resume, ResumeVM>(resume));
        }

        // PUT: api/resumes/skills/link
		/// <summary>
		/// Links a specific skill to resume.
		/// </summary>
		/// <param name="resumeSkill"></param>
        [HttpPut("Skills/Link")]
        public async Task<ActionResult<ResumeVM>> LinkSkillToResume([FromBody] ResumeSkill resumeSkill)
        {
			// Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Resume resume = await this.bll.LinkSkillToResumeAsync(resumeSkill);

            // Mapping
            return Ok(this.mapper.Map<Resume, ResumeVM>(resume));
        }

        // PUT: api/resumes/skills/unlink
		/// <summary>
		/// Unlinks a specific skill from resume.
		/// </summary>
		/// <param name="resumeSkill"></param>
        [HttpPut("Skills/Unlink")]
        public async Task<ActionResult<ResumeVM>> UnlinkSkillFromResume([FromBody] ResumeSkill resumeSkill)
        {
			// Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Resume resume = await this.bll.UnlinkSkillFromResumeAsync(resumeSkill);

            // Mapping
            return Ok(this.mapper.Map<Resume, ResumeVM>(resume));
        }

        // DELETE: api/resumes/{id}
		/// <summary>
		/// Deletes a specific resume.
		/// </summary>
		/// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResumeVM>> DeleteResume([FromRoute] Guid id)
        {
            // Retrieve existing resume
            Resume resume = await this.bll.GetResumeByIdAsync(id);
            if (resume == null)
            {
                return NotFound();
            }

            await this.bll.DeleteResumeAsync(resume);

            // Mapping
            return Ok(this.mapper.Map<Resume, ResumeVM>(resume));
        }
    }
}
