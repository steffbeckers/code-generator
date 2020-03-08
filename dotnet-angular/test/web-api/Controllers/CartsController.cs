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
	/// The Carts controller.
	/// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
	[Produces("application/json")]
    public class CartsController : ControllerBase
    {
        private readonly ILogger<CartsController> logger;
        private readonly IMapper mapper;
        private readonly CartBLL bll;

		/// <summary>
		/// The constructor of the Carts controller.
		/// </summary>
        public CartsController(
            ILogger<CartsController> logger,
			IMapper mapper,
            CartBLL bll
        )
        {
            this.logger = logger;
			this.mapper = mapper;
            this.bll = bll;
        }

        // GET: api/carts
		/// <summary>
		/// Retrieves all carts.
		/// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartVM>>> GetCarts()
        {
            IEnumerable<Cart> carts = await this.bll.GetAllCartsAsync();

			// Mapping
            return Ok(this.mapper.Map<IEnumerable<Cart>, List<CartVM>>(carts));
        }

        // GET: api/carts/{id}
		/// <summary>
		/// Retrieves a specific cart.
		/// </summary>
		/// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<ActionResult<CartVM>> GetCart([FromRoute] Guid id)
        {
            Cart cart = await this.bll.GetCartByIdAsync(id);
            if (cart == null)
            {
                return NotFound();
            }

			// Mapping
            return Ok(this.mapper.Map<Cart, CartVM>(cart));
        }

        // POST: api/carts
		/// <summary>
		/// Creates a new cart.
		/// </summary>
		/// <param name="cartVM"></param>
        [HttpPost]
        public async Task<ActionResult<CartVM>> CreateCart([FromBody] CartVM cartVM)
        {
			// Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Mapping
            Cart cart = this.mapper.Map<CartVM, Cart>(cartVM);

            cart = await this.bll.CreateCartAsync(cart);

			// Mapping
            return CreatedAtAction(
				"GetCart",
				new { id = cart.Id },
				this.mapper.Map<Cart, CartVM>(cart)
			);
        }

		// PUT: api/carts/{id}
		/// <summary>
		/// Updates a specific cart.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="cartVM"></param>
        [HttpPut("{id}")]
        public async Task<ActionResult<CartVM>> UpdateCart([FromRoute] Guid id, [FromBody] CartVM cartVM)
        {
			// Validation
            if (!ModelState.IsValid || id != cartVM.Id)
            {
                return BadRequest(ModelState);
            }

			// Mapping
            Cart cart = this.mapper.Map<CartVM, Cart>(cartVM);

            cart = await this.bll.UpdateCartAsync(cart);

			// Mapping
			return Ok(this.mapper.Map<Cart, CartVM>(cart));
        }

        // PUT: api/carts/products/link
		/// <summary>
		/// Links a specific product to cart.
		/// </summary>
		/// <param name="cartProduct"></param>
        [HttpPut("Products/Link")]
        public async Task<ActionResult<CartVM>> LinkProductToCart([FromBody] CartProduct cartProduct)
        {
			// Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Cart cart = await this.bll.LinkProductToCartAsync(cartProduct);

            // Mapping
            return Ok(this.mapper.Map<Cart, CartVM>(cart));
        }

        // PUT: api/carts/products/unlink
		/// <summary>
		/// Unlinks a specific product from cart.
		/// </summary>
		/// <param name="cartProduct"></param>
        [HttpPut("Products/Unlink")]
        public async Task<ActionResult<CartVM>> UnlinkProductFromCart([FromBody] CartProduct cartProduct)
        {
			// Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Cart cart = await this.bll.UnlinkProductFromCartAsync(cartProduct);

            // Mapping
            return Ok(this.mapper.Map<Cart, CartVM>(cart));
        }

        // DELETE: api/carts/{id}
		/// <summary>
		/// Deletes a specific cart.
		/// </summary>
		/// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<CartVM>> DeleteCart([FromRoute] Guid id)
        {
            // Retrieve existing cart
            Cart cart = await this.bll.GetCartByIdAsync(id);
            if (cart == null)
            {
                return NotFound();
            }

            await this.bll.DeleteCartAsync(cart);

            // Mapping
            return Ok(this.mapper.Map<Cart, CartVM>(cart));
        }
    }
}
