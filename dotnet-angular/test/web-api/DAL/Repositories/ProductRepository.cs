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
	/// The repository for Products in the data access layer.
	/// </summary>
    public class ProductRepository : Repository<Product>
    {
        private new readonly TestContext context;

		/// <summary>
		/// The constructor of the Product repository.
		/// </summary>
        public ProductRepository(TestContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides

		public async Task<IEnumerable<Product>> GetWithLinkedEntitiesAsync()
        {
            return await this.context.Products
                .Include(x => x.CreatedByUser)
                .Include(x => x.ModifiedByUser)
                .ToListAsync();
        }

		public async Task<Product> GetWithLinkedEntitiesByIdAsync(Guid id)
        {
            return await this.context.Products
                .Include(x => x.CreatedByUser)
                .Include(x => x.ModifiedByUser)
                .SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
