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
	/// The DocumentTypes controller.
	/// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
	[Produces("application/json")]
    public class DocumentTypesController : ControllerBase
    {
        private readonly ILogger<DocumentTypesController> logger;
        private readonly IMapper mapper;
        private readonly DocumentTypeBLL bll;

		/// <summary>
		/// The constructor of the DocumentTypes controller.
		/// </summary>
        public DocumentTypesController(
            ILogger<DocumentTypesController> logger,
			IMapper mapper,
            DocumentTypeBLL bll
        )
        {
            this.logger = logger;
			this.mapper = mapper;
            this.bll = bll;
        }

        // GET: api/documenttypes
		/// <summary>
		/// Retrieves all documenttypes.
		/// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentTypeVM>>> GetDocumentTypes()
        {
            IEnumerable<DocumentType> documenttypes = await this.bll.GetAllDocumentTypesAsync();

			// Mapping
            return Ok(this.mapper.Map<IEnumerable<DocumentType>, List<DocumentTypeVM>>(documenttypes));
        }

        // GET: api/documenttypes/{id}
		/// <summary>
		/// Retrieves a specific documenttype.
		/// </summary>
		/// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentTypeVM>> GetDocumentType([FromRoute] Guid id)
        {
            DocumentType documenttype = await this.bll.GetDocumentTypeByIdAsync(id);
            if (documenttype == null)
            {
                return NotFound();
            }

			// Mapping
            return Ok(this.mapper.Map<DocumentType, DocumentTypeVM>(documenttype));
        }

        // POST: api/documenttypes
		/// <summary>
		/// Creates a new documenttype.
		/// </summary>
		/// <param name="documenttypeVM"></param>
        [HttpPost]
        public async Task<ActionResult<DocumentTypeVM>> CreateDocumentType([FromBody] DocumentTypeVM documenttypeVM)
        {
			// Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Mapping
            DocumentType documenttype = this.mapper.Map<DocumentTypeVM, DocumentType>(documenttypeVM);

            documenttype = await this.bll.CreateDocumentTypeAsync(documenttype);

			// Mapping
            return CreatedAtAction(
				"GetDocumentType",
				new { id = documenttype.Id },
				this.mapper.Map<DocumentType, DocumentTypeVM>(documenttype)
			);
        }

		// PUT: api/documenttypes/{id}
		/// <summary>
		/// Updates a specific documenttype.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="documentTypeVM"></param>
        [HttpPut("{id}")]
        public async Task<ActionResult<DocumentTypeVM>> UpdateDocumentType([FromRoute] Guid id, [FromBody] DocumentTypeVM documentTypeVM)
        {
			// Validation
            if (!ModelState.IsValid || id != documentTypeVM.Id)
            {
                return BadRequest(ModelState);
            }

			// Mapping
            DocumentType documentType = this.mapper.Map<DocumentTypeVM, DocumentType>(documentTypeVM);

            documentType = await this.bll.UpdateDocumentTypeAsync(documentType);

			// Mapping
			return Ok(this.mapper.Map<DocumentType, DocumentTypeVM>(documentType));
        }

        // DELETE: api/documenttypes/{id}
		/// <summary>
		/// Deletes a specific documenttype.
		/// </summary>
		/// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<DocumentTypeVM>> DeleteDocumentType([FromRoute] Guid id)
        {
            // Retrieve existing documenttype
            DocumentType documenttype = await this.bll.GetDocumentTypeByIdAsync(id);
            if (documenttype == null)
            {
                return NotFound();
            }

            await this.bll.DeleteDocumentTypeAsync(documenttype);

            // Mapping
            return Ok(this.mapper.Map<DocumentType, DocumentTypeVM>(documenttype));
        }
    }
}
