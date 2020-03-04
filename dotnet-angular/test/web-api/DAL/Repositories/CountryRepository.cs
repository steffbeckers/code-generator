using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.API.Framework;
using Test.API.Models;

namespace Test.API.DAL.Repositories
{
	/// <summary>
	/// The repository for Countries in the data access layer.
	/// </summary>
    public class CountryRepository : Repository<Country>
    {
        private new readonly TestContext context;

		/// <summary>
		/// The constructor of the Country repository.
		/// </summary>
        public CountryRepository(TestContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides

		public async Task<IEnumerable<Country>> GetWithLinkedEntitiesAsync()
        {
            return await this.context.Countries
                .Include(x => x.CreatedByUser)
                .Include(x => x.ModifiedByUser)
                .ToListAsync();
        }

		public async Task<Country> GetWithLinkedEntitiesByIdAsync(Guid id)
        {
            return await this.context.Countries
                .Include(x => x.CreatedByUser)
                .Include(x => x.ModifiedByUser)
                .SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
