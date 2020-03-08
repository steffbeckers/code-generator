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
	/// The repository for Addresses in the data access layer.
	/// </summary>
    public class AddressRepository : Repository<Address>
    {
        private new readonly TestContext context;

		/// <summary>
		/// The constructor of the Address repository.
		/// </summary>
        public AddressRepository(TestContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides

		public async Task<IEnumerable<Address>> GetWithLinkedEntitiesAsync()
        {
            return await this.context.Addresses
                .Include(x => x.CreatedByUser)
                .Include(x => x.ModifiedByUser)
                .ToListAsync();
        }

		public async Task<Address> GetWithLinkedEntitiesByIdAsync(Guid id)
        {
            return await this.context.Addresses
                .Include(x => x.CreatedByUser)
                .Include(x => x.ModifiedByUser)
                .SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
