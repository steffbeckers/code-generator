using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
	/// The RelationTypes controller.
	/// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
	[Produces("application/json")]
    public class RelationTypesController : ControllerBase
    {
        private readonly ILogger<RelationTypesController> logger;
        private readonly IMapper mapper;
        private readonly RelationTypeBLL bll;

		/// <summary>
		/// The constructor of the RelationTypes controller.
		/// </summary>
        public RelationTypesController(
            ILogger<RelationTypesController> logger,
			IMapper mapper,
            RelationTypeBLL bll
        )
        {
            this.logger = logger;
			this.mapper = mapper;
            this.bll = bll;
        }

        // GET: api/relationtypes
		/// <summary>
		/// Retrieves all relationtypes.
		/// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RelationTypeVM>>> GetRelationTypes()
        {
            IEnumerable<RelationType> relationtypes = await this.bll.GetAllRelationTypesAsync();

			// Mapping
            return Ok(this.mapper.Map<IEnumerable<RelationType>, List<RelationTypeVM>>(relationtypes));
        }

        // GET: api/relationtypes/{id}
		/// <summary>
		/// Retrieves a specific relationtype.
		/// </summary>
		/// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<ActionResult<RelationTypeVM>> GetRelationType([FromRoute] Guid id)
        {
            RelationType relationtype = await this.bll.GetRelationTypeByIdAsync(id);
            if (relationtype == null)
            {
                return NotFound();
            }

			// Mapping
            return Ok(this.mapper.Map<RelationType, RelationTypeVM>(relationtype));
        }

        // POST: api/relationtypes
		/// <summary>
		/// Creates a new relationtype.
		/// </summary>
		/// <param name="relationtypeVM"></param>
        [HttpPost]
        public async Task<ActionResult<RelationTypeVM>> CreateRelationType([FromBody] RelationTypeVM relationtypeVM)
        {
			// Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Mapping
            RelationType relationtype = this.mapper.Map<RelationTypeVM, RelationType>(relationtypeVM);

            relationtype = await this.bll.CreateRelationTypeAsync(relationtype);

			// Mapping
            return CreatedAtAction(
				"GetRelationType",
				new { id = relationtype.Id },
				this.mapper.Map<RelationType, RelationTypeVM>(relationtype)
			);
        }

		// PUT: api/relationtypes/{id}
		/// <summary>
		/// Updates a specific relationtype.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="relationTypeVM"></param>
        [HttpPut("{id}")]
        public async Task<ActionResult<RelationTypeVM>> UpdateRelationType([FromRoute] Guid id, [FromBody] RelationTypeVM relationTypeVM)
        {
			// Validation
            if (!ModelState.IsValid || id != relationTypeVM.Id)
            {
                return BadRequest(ModelState);
            }

			// Mapping
            RelationType relationType = this.mapper.Map<RelationTypeVM, RelationType>(relationTypeVM);

            relationType = await this.bll.UpdateRelationTypeAsync(relationType);

			// Mapping
			return Ok(this.mapper.Map<RelationType, RelationTypeVM>(relationType));
        }

        // DELETE: api/relationtypes/{id}
		/// <summary>
		/// Deletes a specific relationtype.
		/// </summary>
		/// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<RelationTypeVM>> DeleteRelationType([FromRoute] Guid id)
        {
            // Retrieve existing relationtype
            RelationType relationtype = await this.bll.GetRelationTypeByIdAsync(id);
            if (relationtype == null)
            {
                return NotFound();
            }

            await this.bll.DeleteRelationTypeAsync(relationtype);

            // Mapping
            return Ok(this.mapper.Map<RelationType, RelationTypeVM>(relationtype));
        }
    }
}
