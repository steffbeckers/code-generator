using CodeGen.API.Requests;
using CodeGen.API.Requests.Projects;
using CodeGen.API.ViewModels;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CodeGen.API.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/projects
        [HttpGet]
        public async Task<IActionResult> GetProjects([FromQuery] string include)
        {
            return Ok(await _mediator.Send(new GetProjects() { Include = include }));
        }

        // GET: api/projects/{id}
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetProjectById([FromRoute] Guid id, [FromQuery] string include)
        {
            return Ok(await _mediator.Send(new GetProjectById() { Id = id, Include = include }));
        }

        // GET: api/projects/{id}/generate
        [HttpGet]
        [Route("{id}/generate")]
        public async Task<IActionResult> GenerateProjectById([FromRoute] Guid id)
        {
            return Ok(await _mediator.Send(new GenerateProjectById() { Id = id }));
        }

        // POST: api/projects
        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] ProjectCreateVM projectCreateVM)
        {
            Response response = await _mediator.Send(new CreateProject() { ProjectCreateVM = projectCreateVM });
            return CreatedAtAction("GetProjectById", new { id = (response.Data as ProjectVM).Id }, response);
        }

        // PUT: api/projects/{id}
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateProject([FromRoute] Guid id, [FromBody] ProjectUpdateVM projectUpdateVM)
        {
            if (id != projectUpdateVM.Id) { return BadRequest(); }
            return Ok(await _mediator.Send(new UpdateProject() { ProjectUpdateVM = projectUpdateVM }));
        }

        // PATCH: api/projects/{id}
        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> PatchProject([FromRoute] Guid id, [FromBody] JsonPatchDocument<ProjectUpdateVM> projectPatchDocument)
        {
            return Ok(await _mediator.Send(new PatchProject() { Id = id, PatchDocument = projectPatchDocument }));
        }

        // DELETE: api/projects/{id}
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProject([FromRoute] Guid id)
        {
            return Ok(await _mediator.Send(new DeleteProject() { Id = id }));
        }
    }
}
