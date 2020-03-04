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
	/// The WorkOrders controller.
	/// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
	[Produces("application/json")]
    public class WorkOrdersController : ControllerBase
    {
        private readonly ILogger<WorkOrdersController> logger;
        private readonly IMapper mapper;
        private readonly WorkOrderBLL bll;

		/// <summary>
		/// The constructor of the WorkOrders controller.
		/// </summary>
        public WorkOrdersController(
            ILogger<WorkOrdersController> logger,
			IMapper mapper,
            WorkOrderBLL bll
        )
        {
            this.logger = logger;
			this.mapper = mapper;
            this.bll = bll;
        }

        // GET: api/workorders
		/// <summary>
		/// Retrieves all workorders.
		/// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkOrderVM>>> GetWorkOrders()
        {
            IEnumerable<WorkOrder> workorders = await this.bll.GetAllWorkOrdersAsync();

			// Mapping
            return Ok(this.mapper.Map<IEnumerable<WorkOrder>, List<WorkOrderVM>>(workorders));
        }

        // GET: api/workorders/{id}
		/// <summary>
		/// Retrieves a specific workorder.
		/// </summary>
		/// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkOrderVM>> GetWorkOrder([FromRoute] Guid id)
        {
            WorkOrder workorder = await this.bll.GetWorkOrderByIdAsync(id);
            if (workorder == null)
            {
                return NotFound();
            }

			// Mapping
            return Ok(this.mapper.Map<WorkOrder, WorkOrderVM>(workorder));
        }

        // POST: api/workorders
		/// <summary>
		/// Creates a new workorder.
		/// </summary>
		/// <param name="workorderVM"></param>
        [HttpPost]
        public async Task<ActionResult<WorkOrderVM>> CreateWorkOrder([FromBody] WorkOrderVM workorderVM)
        {
			// Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Mapping
            WorkOrder workorder = this.mapper.Map<WorkOrderVM, WorkOrder>(workorderVM);

            workorder = await this.bll.CreateWorkOrderAsync(workorder);

			// Mapping
            return CreatedAtAction(
				"GetWorkOrder",
				new { id = workorder.Id },
				this.mapper.Map<WorkOrder, WorkOrderVM>(workorder)
			);
        }

		// PUT: api/workorders/{id}
		/// <summary>
		/// Updates a specific workorder.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="workOrderVM"></param>
        [HttpPut("{id}")]
        public async Task<ActionResult<WorkOrderVM>> UpdateWorkOrder([FromRoute] Guid id, [FromBody] WorkOrderVM workOrderVM)
        {
			// Validation
            if (!ModelState.IsValid || id != workOrderVM.Id)
            {
                return BadRequest(ModelState);
            }

			// Mapping
            WorkOrder workOrder = this.mapper.Map<WorkOrderVM, WorkOrder>(workOrderVM);

            workOrder = await this.bll.UpdateWorkOrderAsync(workOrder);

			// Mapping
			return Ok(this.mapper.Map<WorkOrder, WorkOrderVM>(workOrder));
        }

        // DELETE: api/workorders/{id}
		/// <summary>
		/// Deletes a specific workorder.
		/// </summary>
		/// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<WorkOrderVM>> DeleteWorkOrder([FromRoute] Guid id)
        {
            // Retrieve existing workorder
            WorkOrder workorder = await this.bll.GetWorkOrderByIdAsync(id);
            if (workorder == null)
            {
                return NotFound();
            }

            await this.bll.DeleteWorkOrderAsync(workorder);

            // Mapping
            return Ok(this.mapper.Map<WorkOrder, WorkOrderVM>(workorder));
        }
    }
}
