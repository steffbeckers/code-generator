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
	/// The Calls controller.
	/// </summary>
    [Route("api/[controller]")]
    [ApiController]
	[Produces("application/json")]
    public class CallsController : ControllerBase
    {
        private readonly ILogger<CallsController> logger;
        private readonly IMapper mapper;
        private readonly CallBLL bll;

		/// <summary>
		/// The constructor of the Calls controller.
		/// </summary>
        public CallsController(
            ILogger<CallsController> logger,
			IMapper mapper,
            CallBLL bll
        )
        {
            this.logger = logger;
			this.mapper = mapper;
            this.bll = bll;
        }

        // GET: api/Calls
		/// <summary>
		/// Retrieves all calls.
		/// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CallVM>>> GetCalls()
        {
            IEnumerable<Call> calls = await this.bll.GetAllCallsAsync();

            return this.mapper.Map<IEnumerable<Call>, List<CallVM>>(calls);
        }

        // GET: api/Calls/{id}
		/// <summary>
		/// Retrieves a specific call.
		/// </summary>
		/// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<ActionResult<CallVM>> GetCall([FromRoute] Guid id)
        {
            Call call = await this.bll.GetCallByIdAsync(id);
            if (call == null)
            {
                return NotFound();
            }

            return this.mapper.Map<Call, CallVM>(call);
        }

        // POST: api/Calls
		/// <summary>
		/// Creates a new call.
		/// </summary>
		/// <param name="callVM"></param>
        [HttpPost]
        public async Task<ActionResult<CallVM>> CreateCall([FromBody] CallVM callVM)
        {
			// Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Mapping
            Call call = this.mapper.Map<CallVM, Call>(callVM);

            call = await this.bll.CreateCallAsync(call);

            return CreatedAtAction(
				"GetCall",
				new { id = call.Id },
				this.mapper.Map<Call, CallVM>(call)
			);
        }

		// PUT: api/Calls/{id}
		/// <summary>
		/// Updates a specific call.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="callVM"></param>
        [HttpPut("{id}")]
        public async Task<ActionResult<CallVM>> UpdateCall([FromRoute] Guid id, [FromBody] CallVM callVM)
        {
			// Validation
            if (!ModelState.IsValid || id != callVM.Id)
            {
                return BadRequest(ModelState);
            }

            // Retrieve existing call
            Call call = await this.bll.GetCallByIdAsync(id);
            if (call == null)
            {
                return NotFound();
            }

			// Mapping
            Call callUpdate = this.mapper.Map<CallVM, Call>(callVM);

			// Update fields
            call.Date = callUpdate.Date;
			
            call = await this.bll.UpdateCallAsync(id, call);

			return this.mapper.Map<Call, CallVM>(call);
        }

        // DELETE: api/Calls/{id}
		/// <summary>
		/// Deletes a specific call.
		/// </summary>
		/// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<CallVM>> DeleteCall([FromRoute] Guid id)
        {
            // Retrieve existing call
            Call call = await this.bll.GetCallByIdAsync(id);
            if (call == null)
            {
                return NotFound();
            }

            await this.bll.DeleteCallAsync(call);

            return this.mapper.Map<Call, CallVM>(call);
        }
    }
}
