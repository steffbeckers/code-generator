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
	/// The Documents controller.
	/// </summary>
    [Route("api/[controller]")]
    [ApiController]
	[Produces("application/json")]
    public class DocumentsController : ControllerBase
    {
        private readonly ILogger<DocumentsController> logger;
        private readonly IMapper mapper;
        private readonly DocumentBLL bll;

		/// <summary>
		/// The constructor of the Documents controller.
		/// </summary>
        public DocumentsController(
            ILogger<DocumentsController> logger,
			IMapper mapper,
            DocumentBLL bll
        )
        {
            this.logger = logger;
			this.mapper = mapper;
            this.bll = bll;
        }

        // GET: api/Documents
		/// <summary>
		/// Retrieves all documents.
		/// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentVM>>> GetDocuments()
        {
            IEnumerable<Document> documents = await this.bll.GetAllDocumentsAsync();

            return this.mapper.Map<IEnumerable<Document>, List<DocumentVM>>(documents);
        }

        // GET: api/Documents/{id}
		/// <summary>
		/// Retrieves a specific document.
		/// </summary>
		/// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentVM>> GetDocument([FromRoute] Guid id)
        {
            Document document = await this.bll.GetDocumentByIdAsync(id);
            if (document == null)
            {
                return NotFound();
            }

            return this.mapper.Map<Document, DocumentVM>(document);
        }

        // POST: api/Documents
		/// <summary>
		/// Creates a new document.
		/// </summary>
		/// <param name="documentVM"></param>
        [HttpPost]
        public async Task<ActionResult<DocumentVM>> CreateDocument([FromBody] DocumentVM documentVM)
        {
			// Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Mapping
            Document document = this.mapper.Map<DocumentVM, Document>(documentVM);

            document = await this.bll.CreateDocumentAsync(document);

            return CreatedAtAction(
				"GetDocument",
				new { id = document.Id },
				this.mapper.Map<Document, DocumentVM>(document)
			);
        }

		// PUT: api/Documents/{id}
		/// <summary>
		/// Updates a specific document.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="documentVM"></param>
        [HttpPut("{id}")]
        public async Task<ActionResult<DocumentVM>> UpdateDocument([FromRoute] Guid id, [FromBody] DocumentVM documentVM)
        {
			// Validation
            if (!ModelState.IsValid || id != documentVM.Id)
            {
                return BadRequest(ModelState);
            }

            // Retrieve existing document
            Document document = await this.bll.GetDocumentByIdAsync(id);
            if (document == null)
            {
                return NotFound();
            }

			// Mapping
            Document documentUpdate = this.mapper.Map<DocumentVM, Document>(documentVM);

			// Update fields
            document.Name = documentUpdate.Name;
			
            document = await this.bll.UpdateDocumentAsync(id, document);

			return this.mapper.Map<Document, DocumentVM>(document);
        }

        // DELETE: api/Documents/{id}
		/// <summary>
		/// Deletes a specific document.
		/// </summary>
		/// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<DocumentVM>> DeleteDocument([FromRoute] Guid id)
        {
            // Retrieve existing document
            Document document = await this.bll.GetDocumentByIdAsync(id);
            if (document == null)
            {
                return NotFound();
            }

            await this.bll.DeleteDocumentAsync(document);

            return this.mapper.Map<Document, DocumentVM>(document);
        }
    }
}
