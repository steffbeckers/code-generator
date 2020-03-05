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
	/// The ProductDetails controller.
	/// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
	[Produces("application/json")]
    public class ProductDetailsController : ControllerBase
    {
        private readonly ILogger<ProductDetailsController> logger;
        private readonly IMapper mapper;
        private readonly ProductDetailBLL bll;

		/// <summary>
		/// The constructor of the ProductDetails controller.
		/// </summary>
        public ProductDetailsController(
            ILogger<ProductDetailsController> logger,
			IMapper mapper,
            ProductDetailBLL bll
        )
        {
            this.logger = logger;
			this.mapper = mapper;
            this.bll = bll;
        }

        // GET: api/productdetails
		/// <summary>
		/// Retrieves all productdetails.
		/// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDetailVM>>> GetProductDetails()
        {
            IEnumerable<ProductDetail> productdetails = await this.bll.GetAllProductDetailsAsync();

			// Mapping
            return Ok(this.mapper.Map<IEnumerable<ProductDetail>, List<ProductDetailVM>>(productdetails));
        }

        // GET: api/productdetails/{id}
		/// <summary>
		/// Retrieves a specific productdetail.
		/// </summary>
		/// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDetailVM>> GetProductDetail([FromRoute] Guid id)
        {
            ProductDetail productdetail = await this.bll.GetProductDetailByIdAsync(id);
            if (productdetail == null)
            {
                return NotFound();
            }

			// Mapping
            return Ok(this.mapper.Map<ProductDetail, ProductDetailVM>(productdetail));
        }

        // POST: api/productdetails
		/// <summary>
		/// Creates a new productdetail.
		/// </summary>
		/// <param name="productdetailVM"></param>
        [HttpPost]
        public async Task<ActionResult<ProductDetailVM>> CreateProductDetail([FromBody] ProductDetailVM productdetailVM)
        {
			// Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Mapping
            ProductDetail productdetail = this.mapper.Map<ProductDetailVM, ProductDetail>(productdetailVM);

            productdetail = await this.bll.CreateProductDetailAsync(productdetail);

			// Mapping
            return CreatedAtAction(
				"GetProductDetail",
				new { id = productdetail.Id },
				this.mapper.Map<ProductDetail, ProductDetailVM>(productdetail)
			);
        }

		// PUT: api/productdetails/{id}
		/// <summary>
		/// Updates a specific productdetail.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="productDetailVM"></param>
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductDetailVM>> UpdateProductDetail([FromRoute] Guid id, [FromBody] ProductDetailVM productDetailVM)
        {
			// Validation
            if (!ModelState.IsValid || id != productDetailVM.Id)
            {
                return BadRequest(ModelState);
            }

			// Mapping
            ProductDetail productDetail = this.mapper.Map<ProductDetailVM, ProductDetail>(productDetailVM);

            productDetail = await this.bll.UpdateProductDetailAsync(productDetail);

			// Mapping
			return Ok(this.mapper.Map<ProductDetail, ProductDetailVM>(productDetail));
        }

        // DELETE: api/productdetails/{id}
		/// <summary>
		/// Deletes a specific productdetail.
		/// </summary>
		/// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductDetailVM>> DeleteProductDetail([FromRoute] Guid id)
        {
            // Retrieve existing productdetail
            ProductDetail productdetail = await this.bll.GetProductDetailByIdAsync(id);
            if (productdetail == null)
            {
                return NotFound();
            }

            await this.bll.DeleteProductDetailAsync(productdetail);

            // Mapping
            return Ok(this.mapper.Map<ProductDetail, ProductDetailVM>(productdetail));
        }
    }
}
