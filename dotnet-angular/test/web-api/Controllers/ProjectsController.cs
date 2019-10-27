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
	/// The Projects controller.
	/// </summary>
    [Route("api/[controller]")]
    [ApiController]
	[Produces("application/json")]
    public class ProjectsController : ControllerBase
    {
        private readonly ILogger<ProjectsController> logger;
        private readonly IMapper mapper;
        private readonly ProjectBLL bll;

		/// <summary>
		/// The constructor of the Projects controller.
		/// </summary>
        public ProjectsController(
            ILogger<ProjectsController> logger,
			IMapper mapper,
            ProjectBLL bll
        )
        {
            this.logger = logger;
			this.mapper = mapper;
            this.bll = bll;
        }

        // GET: api/Projects
		/// <summary>
		/// Retrieves all projects.
		/// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectVM>>> GetProjects()
        {
            IEnumerable<Project> projects = await this.bll.GetAllProjectsAsync();

            return this.mapper.Map<IEnumerable<Project>, List<ProjectVM>>(projects);
        }

        // GET: api/Projects/{id}
		/// <summary>
		/// Retrieves a specific project.
		/// </summary>
		/// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectVM>> GetProject([FromRoute] Guid id)
        {
            Project project = await this.bll.GetProjectByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            return this.mapper.Map<Project, ProjectVM>(project);
        }

        // POST: api/Projects
		/// <summary>
		/// Creates a new project.
		/// </summary>
		/// <param name="projectVM"></param>
        [HttpPost]
        public async Task<ActionResult<ProjectVM>> CreateProject([FromBody] ProjectVM projectVM)
        {
			// Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Mapping
            Project project = this.mapper.Map<ProjectVM, Project>(projectVM);

            project = await this.bll.CreateProjectAsync(project);

            return CreatedAtAction(
				"GetProject",
				new { id = project.Id },
				this.mapper.Map<Project, ProjectVM>(project)
			);
        }

		// PUT: api/Projects/{id}
		/// <summary>
		/// Updates a specific project.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="projectVM"></param>
        [HttpPut("{id}")]
        public async Task<ActionResult<ProjectVM>> UpdateProject([FromRoute] Guid id, [FromBody] ProjectVM projectVM)
        {
			// Validation
            if (!ModelState.IsValid || id != projectVM.Id)
            {
                return BadRequest(ModelState);
            }

            // Retrieve existing project
            Project project = await this.bll.GetProjectByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }

			// Mapping
            Project projectUpdate = this.mapper.Map<ProjectVM, Project>(projectVM);

			// Update fields
            project.Name = projectUpdate.Name;
            project.Description = projectUpdate.Description;
			
            project = await this.bll.UpdateProjectAsync(id, project);

			return this.mapper.Map<Project, ProjectVM>(project);
        }

        // DELETE: api/Projects/{id}
		/// <summary>
		/// Deletes a specific project.
		/// </summary>
		/// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProjectVM>> DeleteProject([FromRoute] Guid id)
        {
            // Retrieve existing project
            Project project = await this.bll.GetProjectByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            await this.bll.DeleteProjectAsync(project);

            return this.mapper.Map<Project, ProjectVM>(project);
        }
    }
}
