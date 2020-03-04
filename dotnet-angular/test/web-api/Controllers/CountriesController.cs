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
	/// The Countries controller.
	/// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
	[Produces("application/json")]
    public class CountriesController : ControllerBase
    {
        private readonly ILogger<CountriesController> logger;
        private readonly IMapper mapper;
        private readonly CountryBLL bll;

		/// <summary>
		/// The constructor of the Countries controller.
		/// </summary>
        public CountriesController(
            ILogger<CountriesController> logger,
			IMapper mapper,
            CountryBLL bll
        )
        {
            this.logger = logger;
			this.mapper = mapper;
            this.bll = bll;
        }

        // GET: api/countries
		/// <summary>
		/// Retrieves all countries.
		/// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryVM>>> GetCountries()
        {
            IEnumerable<Country> countries = await this.bll.GetAllCountriesAsync();

			// Mapping
            return Ok(this.mapper.Map<IEnumerable<Country>, List<CountryVM>>(countries));
        }

        // GET: api/countries/{id}
		/// <summary>
		/// Retrieves a specific country.
		/// </summary>
		/// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryVM>> GetCountry([FromRoute] Guid id)
        {
            Country country = await this.bll.GetCountryByIdAsync(id);
            if (country == null)
            {
                return NotFound();
            }

			// Mapping
            return Ok(this.mapper.Map<Country, CountryVM>(country));
        }

        // POST: api/countries
		/// <summary>
		/// Creates a new country.
		/// </summary>
		/// <param name="countryVM"></param>
        [HttpPost]
        public async Task<ActionResult<CountryVM>> CreateCountry([FromBody] CountryVM countryVM)
        {
			// Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Mapping
            Country country = this.mapper.Map<CountryVM, Country>(countryVM);

            country = await this.bll.CreateCountryAsync(country);

			// Mapping
            return CreatedAtAction(
				"GetCountry",
				new { id = country.Id },
				this.mapper.Map<Country, CountryVM>(country)
			);
        }

		// PUT: api/countries/{id}
		/// <summary>
		/// Updates a specific country.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="countryVM"></param>
        [HttpPut("{id}")]
        public async Task<ActionResult<CountryVM>> UpdateCountry([FromRoute] Guid id, [FromBody] CountryVM countryVM)
        {
			// Validation
            if (!ModelState.IsValid || id != countryVM.Id)
            {
                return BadRequest(ModelState);
            }

			// Mapping
            Country country = this.mapper.Map<CountryVM, Country>(countryVM);

            country = await this.bll.UpdateCountryAsync(country);

			// Mapping
			return Ok(this.mapper.Map<Country, CountryVM>(country));
        }

        // DELETE: api/countries/{id}
		/// <summary>
		/// Deletes a specific country.
		/// </summary>
		/// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<CountryVM>> DeleteCountry([FromRoute] Guid id)
        {
            // Retrieve existing country
            Country country = await this.bll.GetCountryByIdAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            await this.bll.DeleteCountryAsync(country);

            // Mapping
            return Ok(this.mapper.Map<Country, CountryVM>(country));
        }
    }
}
