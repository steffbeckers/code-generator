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
	/// The Addresses controller.
	/// </summary>
    [Route("api/[controller]")]
    [ApiController]
	[Produces("application/json")]
    public class AddressesController : ControllerBase
    {
        private readonly ILogger<AddressesController> logger;
        private readonly IMapper mapper;
        private readonly AddressBLL bll;

		/// <summary>
		/// The constructor of the Addresses controller.
		/// </summary>
        public AddressesController(
            ILogger<AddressesController> logger,
			IMapper mapper,
            AddressBLL bll
        )
        {
            this.logger = logger;
			this.mapper = mapper;
            this.bll = bll;
        }

        // GET: api/Addresses
		/// <summary>
		/// Retrieves all addresses.
		/// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressVM>>> GetAddresses()
        {
            IEnumerable<Address> addresses = await this.bll.GetAllAddressesAsync();

            return this.mapper.Map<IEnumerable<Address>, List<AddressVM>>(addresses);
        }

        // GET: api/Addresses/{id}
		/// <summary>
		/// Retrieves a specific address.
		/// </summary>
		/// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<ActionResult<AddressVM>> GetAddress([FromRoute] Guid id)
        {
            Address address = await this.bll.GetAddressByIdAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            return this.mapper.Map<Address, AddressVM>(address);
        }

        // POST: api/Addresses
		/// <summary>
		/// Creates a new address.
		/// </summary>
		/// <param name="addressVM"></param>
        [HttpPost]
        public async Task<ActionResult<AddressVM>> CreateAddress([FromBody] AddressVM addressVM)
        {
			// Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Mapping
            Address address = this.mapper.Map<AddressVM, Address>(addressVM);

            address = await this.bll.CreateAddressAsync(address);

            return CreatedAtAction(
				"GetAddress",
				new { id = address.Id },
				this.mapper.Map<Address, AddressVM>(address)
			);
        }

		// PUT: api/Addresses/{id}
		/// <summary>
		/// Updates a specific address.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="addressVM"></param>
        [HttpPut("{id}")]
        public async Task<ActionResult<AddressVM>> UpdateAddress([FromRoute] Guid id, [FromBody] AddressVM addressVM)
        {
			// Validation
            if (!ModelState.IsValid || id != addressVM.Id)
            {
                return BadRequest(ModelState);
            }

            // Retrieve existing address
            Address address = await this.bll.GetAddressByIdAsync(id);
            if (address == null)
            {
                return NotFound();
            }

			// Mapping
            Address addressUpdate = this.mapper.Map<AddressVM, Address>(addressVM);

			// Update fields
            address.Street = addressUpdate.Street;
            address.Number = addressUpdate.Number;
            address.PostalCode = addressUpdate.PostalCode;
            address.City = addressUpdate.City;
            address.Primary = addressUpdate.Primary;
			
            address = await this.bll.UpdateAddressAsync(id, address);

			return this.mapper.Map<Address, AddressVM>(address);
        }

        // DELETE: api/Addresses/{id}
		/// <summary>
		/// Deletes a specific address.
		/// </summary>
		/// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<AddressVM>> DeleteAddress([FromRoute] Guid id)
        {
            // Retrieve existing address
            Address address = await this.bll.GetAddressByIdAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            await this.bll.DeleteAddressAsync(address);

            return this.mapper.Map<Address, AddressVM>(address);
        }
    }
}
