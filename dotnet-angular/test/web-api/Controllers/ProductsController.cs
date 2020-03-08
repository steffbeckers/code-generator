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
	/// The Products controller.
	/// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
	[Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> logger;
        private readonly IMapper mapper;
        private readonly ProductBLL bll;

		/// <summary>
		/// The constructor of the Products controller.
		/// </summary>
        public ProductsController(
            ILogger<ProductsController> logger,
			IMapper mapper,
            ProductBLL bll
        )
        {
            this.logger = logger;
			this.mapper = mapper;
            this.bll = bll;
        }

        // GET: api/products
		/// <summary>
		/// Retrieves all products.
		/// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductVM>>> GetProducts()
        {
            IEnumerable<Product> products = await this.bll.GetAllProductsAsync();

			// Mapping
            return Ok(this.mapper.Map<IEnumerable<Product>, List<ProductVM>>(products));
        }

        // GET: api/products/{id}
		/// <summary>
		/// Retrieves a specific product.
		/// </summary>
		/// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductVM>> GetProduct([FromRoute] Guid id)
        {
            Product product = await this.bll.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

			// Mapping
            return Ok(this.mapper.Map<Product, ProductVM>(product));
        }

        // POST: api/products
		/// <summary>
		/// Creates a new product.
		/// </summary>
		/// <param name="productVM"></param>
        [HttpPost]
        public async Task<ActionResult<ProductVM>> CreateProduct([FromBody] ProductVM productVM)
        {
			// Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Mapping
            Product product = this.mapper.Map<ProductVM, Product>(productVM);

            product = await this.bll.CreateProductAsync(product);

			// Mapping
            return CreatedAtAction(
				"GetProduct",
				new { id = product.Id },
				this.mapper.Map<Product, ProductVM>(product)
			);
        }

		// PUT: api/products/{id}
		/// <summary>
		/// Updates a specific product.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="productVM"></param>
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductVM>> UpdateProduct([FromRoute] Guid id, [FromBody] ProductVM productVM)
        {
			// Validation
            if (!ModelState.IsValid || id != productVM.Id)
            {
                return BadRequest(ModelState);
            }

			// Mapping
            Product product = this.mapper.Map<ProductVM, Product>(productVM);

            product = await this.bll.UpdateProductAsync(product);

			// Mapping
			return Ok(this.mapper.Map<Product, ProductVM>(product));
        }

        // DELETE: api/products/{id}
		/// <summary>
		/// Deletes a specific product.
		/// </summary>
		/// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductVM>> DeleteProduct([FromRoute] Guid id)
        {
            // Retrieve existing product
            Product product = await this.bll.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            await this.bll.DeleteProductAsync(product);

            // Mapping
            return Ok(this.mapper.Map<Product, ProductVM>(product));
        }
    }
}
