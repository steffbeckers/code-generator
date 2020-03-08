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
	/// The OrderStates controller.
	/// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
	[Produces("application/json")]
    public class OrderStatesController : ControllerBase
    {
        private readonly ILogger<OrderStatesController> logger;
        private readonly IMapper mapper;
        private readonly OrderStateBLL bll;

		/// <summary>
		/// The constructor of the OrderStates controller.
		/// </summary>
        public OrderStatesController(
            ILogger<OrderStatesController> logger,
			IMapper mapper,
            OrderStateBLL bll
        )
        {
            this.logger = logger;
			this.mapper = mapper;
            this.bll = bll;
        }

        // GET: api/orderstates
		/// <summary>
		/// Retrieves all orderstates.
		/// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderStateVM>>> GetOrderStates()
        {
            IEnumerable<OrderState> orderstates = await this.bll.GetAllOrderStatesAsync();

			// Mapping
            return Ok(this.mapper.Map<IEnumerable<OrderState>, List<OrderStateVM>>(orderstates));
        }

        // GET: api/orderstates/{id}
		/// <summary>
		/// Retrieves a specific orderstate.
		/// </summary>
		/// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderStateVM>> GetOrderState([FromRoute] Guid id)
        {
            OrderState orderstate = await this.bll.GetOrderStateByIdAsync(id);
            if (orderstate == null)
            {
                return NotFound();
            }

			// Mapping
            return Ok(this.mapper.Map<OrderState, OrderStateVM>(orderstate));
        }

        // POST: api/orderstates
		/// <summary>
		/// Creates a new orderstate.
		/// </summary>
		/// <param name="orderstateVM"></param>
        [HttpPost]
        public async Task<ActionResult<OrderStateVM>> CreateOrderState([FromBody] OrderStateVM orderstateVM)
        {
			// Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Mapping
            OrderState orderstate = this.mapper.Map<OrderStateVM, OrderState>(orderstateVM);

            orderstate = await this.bll.CreateOrderStateAsync(orderstate);

			// Mapping
            return CreatedAtAction(
				"GetOrderState",
				new { id = orderstate.Id },
				this.mapper.Map<OrderState, OrderStateVM>(orderstate)
			);
        }

		// PUT: api/orderstates/{id}
		/// <summary>
		/// Updates a specific orderstate.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="orderStateVM"></param>
        [HttpPut("{id}")]
        public async Task<ActionResult<OrderStateVM>> UpdateOrderState([FromRoute] Guid id, [FromBody] OrderStateVM orderStateVM)
        {
			// Validation
            if (!ModelState.IsValid || id != orderStateVM.Id)
            {
                return BadRequest(ModelState);
            }

			// Mapping
            OrderState orderState = this.mapper.Map<OrderStateVM, OrderState>(orderStateVM);

            orderState = await this.bll.UpdateOrderStateAsync(orderState);

			// Mapping
			return Ok(this.mapper.Map<OrderState, OrderStateVM>(orderState));
        }

        // DELETE: api/orderstates/{id}
		/// <summary>
		/// Deletes a specific orderstate.
		/// </summary>
		/// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderStateVM>> DeleteOrderState([FromRoute] Guid id)
        {
            // Retrieve existing orderstate
            OrderState orderstate = await this.bll.GetOrderStateByIdAsync(id);
            if (orderstate == null)
            {
                return NotFound();
            }

            await this.bll.DeleteOrderStateAsync(orderstate);

            // Mapping
            return Ok(this.mapper.Map<OrderState, OrderStateVM>(orderstate));
        }
    }
}
