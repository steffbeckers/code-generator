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
	/// The Orders controller.
	/// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
	[Produces("application/json")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> logger;
        private readonly IMapper mapper;
        private readonly OrderBLL bll;

		/// <summary>
		/// The constructor of the Orders controller.
		/// </summary>
        public OrdersController(
            ILogger<OrdersController> logger,
			IMapper mapper,
            OrderBLL bll
        )
        {
            this.logger = logger;
			this.mapper = mapper;
            this.bll = bll;
        }

        // GET: api/orders
		/// <summary>
		/// Retrieves all orders.
		/// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderVM>>> GetOrders()
        {
            IEnumerable<Order> orders = await this.bll.GetAllOrdersAsync();

			// Mapping
            return Ok(this.mapper.Map<IEnumerable<Order>, List<OrderVM>>(orders));
        }

        // GET: api/orders/{id}
		/// <summary>
		/// Retrieves a specific order.
		/// </summary>
		/// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderVM>> GetOrder([FromRoute] Guid id)
        {
            Order order = await this.bll.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

			// Mapping
            return Ok(this.mapper.Map<Order, OrderVM>(order));
        }

        // POST: api/orders
		/// <summary>
		/// Creates a new order.
		/// </summary>
		/// <param name="orderVM"></param>
        [HttpPost]
        public async Task<ActionResult<OrderVM>> CreateOrder([FromBody] OrderVM orderVM)
        {
			// Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Mapping
            Order order = this.mapper.Map<OrderVM, Order>(orderVM);

            order = await this.bll.CreateOrderAsync(order);

			// Mapping
            return CreatedAtAction(
				"GetOrder",
				new { id = order.Id },
				this.mapper.Map<Order, OrderVM>(order)
			);
        }

		// PUT: api/orders/{id}
		/// <summary>
		/// Updates a specific order.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="orderVM"></param>
        [HttpPut("{id}")]
        public async Task<ActionResult<OrderVM>> UpdateOrder([FromRoute] Guid id, [FromBody] OrderVM orderVM)
        {
			// Validation
            if (!ModelState.IsValid || id != orderVM.Id)
            {
                return BadRequest(ModelState);
            }

			// Mapping
            Order order = this.mapper.Map<OrderVM, Order>(orderVM);

            order = await this.bll.UpdateOrderAsync(order);

			// Mapping
			return Ok(this.mapper.Map<Order, OrderVM>(order));
        }

        // DELETE: api/orders/{id}
		/// <summary>
		/// Deletes a specific order.
		/// </summary>
		/// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderVM>> DeleteOrder([FromRoute] Guid id)
        {
            // Retrieve existing order
            Order order = await this.bll.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            await this.bll.DeleteOrderAsync(order);

            // Mapping
            return Ok(this.mapper.Map<Order, OrderVM>(order));
        }
    }
}
