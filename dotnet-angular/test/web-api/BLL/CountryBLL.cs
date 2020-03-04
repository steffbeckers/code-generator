using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.API.DAL.Repositories;
using Test.API.Models;

namespace Test.API.BLL
{
	/// <summary>
	/// The business logic layer for Countries.
	/// </summary>
    public class CountryBLL
    {
        private readonly CountryRepository countryRepository;

		/// <summary>
		/// The constructor of the Country business logic layer.
		/// </summary>
        public CountryBLL(
			CountryRepository countryRepository
		)
        {
            this.countryRepository = countryRepository;
        }

		/// <summary>
		/// Retrieves all countries.
		/// </summary>
		public async Task<IEnumerable<Country>> GetAllCountriesAsync()
        {
			// #-#-# {83B8AA9F-713A-42FB-ADE1-8A4AA43886C8}
			// Before retrieval
			// #-#-#

            return await this.countryRepository.GetWithLinkedEntitiesAsync();
        }

		/// <summary>
		/// Retrieves one country by Id.
		/// </summary>
		public async Task<Country> GetCountryByIdAsync(Guid id)
        {
			// #-#-# {F838CE2A-D0FB-4F8A-A826-0D653DEECB2B}
			// Before retrieval
			// #-#-#

            return await this.countryRepository.GetWithLinkedEntitiesByIdAsync(id);
        }

		/// <summary>
		/// Creates a new country record.
		/// </summary>
        public async Task<Country> CreateCountryAsync(Country country)
        {
            // Validation
            if (country == null) { return null; }

			// Trimming strings
            if (!string.IsNullOrEmpty(country.Name))
                country.Name = country.Name.Trim();

			// #-#-# {D4775AF3-4BFA-496A-AA82-001028A22DD6}
			// Before creation
			// #-#-#

			country = await this.countryRepository.InsertAsync(country);

			// #-#-# {1972C619-D2F2-48FD-8474-3A69621B1F78}
			// After creation
			// #-#-#

            return country;
        }

		/// <summary>
		/// Updates an existing country record by Id.
		/// </summary>
        public async Task<Country> UpdateCountryAsync(Country countryUpdate)
        {
            // Validation
            if (countryUpdate == null) { return null; }

            // Retrieve existing
            Country country = await this.countryRepository.GetByIdAsync(countryUpdate.Id);
            if (country == null)
            {
                return null;
            }

			// Trimming strings
            if (!string.IsNullOrEmpty(countryUpdate.Name))
                countryUpdate.Name = countryUpdate.Name.Trim();

            // Mapping
            country.Id = countryUpdate.Id;
            country.Name = countryUpdate.Name;

			// #-#-# {B5914243-E57E-41AE-A7C8-553F2F93267B}
			// Before update
			// #-#-#

			country = await this.countryRepository.UpdateAsync(country);

			// #-#-# {983B1B6C-14A7-4925-8571-D77447DF0ADA}
			// After update
			// #-#-#

            return country;
        }

		/// <summary>
		/// Deletes an existing country record by Id.
		/// </summary>
        public async Task<Country> DeleteCountryByIdAsync(Guid countryId)
        {
            Country country = await this.countryRepository.GetByIdAsync(countryId);

            return await this.DeleteCountryAsync(country);
        }

		/// <summary>
		/// Deletes an existing country record.
		/// </summary>
        public async Task<Country> DeleteCountryAsync(Country country)
        {
            // Validation
            if (country == null) { return null; }

			// #-#-# {FE1A99E0-482D-455B-A8C1-3C2C11FACA58}
			// Before deletion
			// #-#-#

            await this.countryRepository.DeleteAsync(country);

			// #-#-# {F09857C0-44E7-4E6C-B3E6-883C0D28E1A6}
			// After deletion
			// #-#-#

            return country;
        }
    }
}
