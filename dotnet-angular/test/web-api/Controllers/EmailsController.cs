using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Test.API.BLL;
using Test.API.Models;
using Test.API.ViewModels;

namespace Test.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailsController : ControllerBase
    {
        private readonly ILogger<EmailsController> logger;
        private readonly IMapper mapper;
        private readonly EmailBLL bll;

        public EmailsController(
            ILogger<EmailsController> logger,
			IMapper mapper,
            EmailBLL bll
        )
        {
            this.logger = logger;
			this.mapper = mapper;
            this.bll = bll;
        }

        // GET: api/Emails
		/// <summary>
		/// Retrieves all emails.
		/// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmailVM>>> GetEmails()
        {
            IEnumerable<Email> emails = await this.bll.GetAllEmailsAsync();

            return this.mapper.Map<IEnumerable<Email>, List<EmailVM>>(emails);
        }

        // GET: api/Emails/{id}
		/// <summary>
		/// Retrieves a specific email.
		/// </summary>
		/// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<ActionResult<EmailVM>> GetEmail([FromRoute] Guid id)
        {
            Email email = await this.bll.GetEmailByIdAsync(id);
            if (email == null)
            {
                return NotFound();
            }

            return this.mapper.Map<Email, EmailVM>(email);
        }

        // POST: api/Emails
		/// <summary>
		/// Creates a new email.
		/// </summary>
		/// <param name="emailVM"></param>
        [HttpPost]
        public async Task<ActionResult<EmailVM>> CreateEmail([FromBody] EmailVM emailVM)
        {
			// Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Mapping
            Email email = this.mapper.Map<EmailVM, Email>(emailVM);

            email = await this.bll.CreateEmailAsync(email);

            return CreatedAtAction(
				"GetEmail",
				new { id = email.Id },
				this.mapper.Map<Email, EmailVM>(email)
			);
        }

		// PUT: api/Emails/{id}
		/// <summary>
		/// Updates a specific email.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="emailVM"></param>
        [HttpPut("{id}")]
        public async Task<ActionResult<EmailVM>> UpdateEmail([FromRoute] Guid id, [FromBody] EmailVM emailVM)
        {
			// Validation
            if (!ModelState.IsValid || id != emailVM.Id)
            {
                return BadRequest(ModelState);
            }

            // Retrieve existing email
            Email email = await this.bll.GetEmailByIdAsync(id);
            if (email == null)
            {
                return NotFound();
            }

			// Mapping
            Email emailUpdate = this.mapper.Map<EmailVM, Email>(emailVM);

			// Update fields
            email.Subject = emailUpdate.Subject;
            email.Body = emailUpdate.Body;
			
            email = await this.bll.UpdateEmailAsync(id, email);

			return this.mapper.Map<Email, EmailVM>(email);
        }

        // DELETE: api/Emails/{id}
		/// <summary>
		/// Deletes a specific email.
		/// </summary>
		/// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<EmailVM>> DeleteEmail([FromRoute] Guid id)
        {
            // Retrieve existing email
            Email email = await this.bll.GetEmailByIdAsync(id);
            if (email == null)
            {
                return NotFound();
            }

            await this.bll.RemoveEmailAsync(id);

            return this.mapper.Map<Email, EmailVM>(email);
        }
    }
}
