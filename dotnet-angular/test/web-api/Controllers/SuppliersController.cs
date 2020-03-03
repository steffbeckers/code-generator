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
	/// The Suppliers controller.
	/// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
	[Produces("application/json")]
    public class SuppliersController : ControllerBase
    {
        private readonly ILogger<SuppliersController> logger;
        private readonly IMapper mapper;
        private readonly SupplierBLL bll;

		/// <summary>
		/// The constructor of the Suppliers controller.
		/// </summary>
        public SuppliersController(
            ILogger<SuppliersController> logger,
			IMapper mapper,
            SupplierBLL bll
        )
        {
            this.logger = logger;
			this.mapper = mapper;
            this.bll = bll;
        }

        // GET: api/suppliers
		/// <summary>
		/// Retrieves all suppliers.
		/// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierVM>>> GetSuppliers()
        {
            IEnumerable<Supplier> suppliers = await this.bll.GetAllSuppliersAsync();

			// Mapping
            return Ok(this.mapper.Map<IEnumerable<Supplier>, List<SupplierVM>>(suppliers));
        }

        // GET: api/suppliers/{id}
		/// <summary>
		/// Retrieves a specific supplier.
		/// </summary>
		/// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierVM>> GetSupplier([FromRoute] Guid id)
        {
            Supplier supplier = await this.bll.GetSupplierByIdAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }

			// Mapping
            return Ok(this.mapper.Map<Supplier, SupplierVM>(supplier));
        }

        // POST: api/suppliers
		/// <summary>
		/// Creates a new supplier.
		/// </summary>
		/// <param name="supplierVM"></param>
        [HttpPost]
        public async Task<ActionResult<SupplierVM>> CreateSupplier([FromBody] SupplierVM supplierVM)
        {
			// Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Mapping
            Supplier supplier = this.mapper.Map<SupplierVM, Supplier>(supplierVM);

            supplier = await this.bll.CreateSupplierAsync(supplier);

			// Mapping
            return CreatedAtAction(
				"GetSupplier",
				new { id = supplier.Id },
				this.mapper.Map<Supplier, SupplierVM>(supplier)
			);
        }

		// PUT: api/suppliers/{id}
		/// <summary>
		/// Updates a specific supplier.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="supplierVM"></param>
        [HttpPut("{id}")]
        public async Task<ActionResult<SupplierVM>> UpdateSupplier([FromRoute] Guid id, [FromBody] SupplierVM supplierVM)
        {
			// Validation
            if (!ModelState.IsValid || id != supplierVM.Id)
            {
                return BadRequest(ModelState);
            }

			// Mapping
            Supplier supplier = this.mapper.Map<SupplierVM, Supplier>(supplierVM);

            supplier = await this.bll.UpdateSupplierAsync(supplier);

			// Mapping
			return Ok(this.mapper.Map<Supplier, SupplierVM>(supplier));
        }

        // PUT: api/suppliers/products/link
		/// <summary>
		/// Links a specific product to supplier.
		/// </summary>
		/// <param name="productSupplier"></param>
        [HttpPut("Products/Link")]
        public async Task<ActionResult<SupplierVM>> LinkProductToSupplier([FromBody] ProductSupplier productSupplier)
        {
			// Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Supplier supplier = await this.bll.LinkProductToSupplierAsync(productSupplier);

            // Mapping
            return Ok(this.mapper.Map<Supplier, SupplierVM>(supplier));
        }

        // PUT: api/suppliers/products/unlink
		/// <summary>
		/// Unlinks a specific product from supplier.
		/// </summary>
		/// <param name="productSupplier"></param>
        [HttpPut("Products/Unlink")]
        public async Task<ActionResult<SupplierVM>> UnlinkProductFromSupplier([FromBody] ProductSupplier productSupplier)
        {
			// Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Supplier supplier = await this.bll.UnlinkProductFromSupplierAsync(productSupplier);

            // Mapping
            return Ok(this.mapper.Map<Supplier, SupplierVM>(supplier));
        }

        // DELETE: api/suppliers/{id}
		/// <summary>
		/// Deletes a specific supplier.
		/// </summary>
		/// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<SupplierVM>> DeleteSupplier([FromRoute] Guid id)
        {
            // Retrieve existing supplier
            Supplier supplier = await this.bll.GetSupplierByIdAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }

            await this.bll.DeleteSupplierAsync(supplier);

            // Mapping
            return Ok(this.mapper.Map<Supplier, SupplierVM>(supplier));
        }
    }
}
