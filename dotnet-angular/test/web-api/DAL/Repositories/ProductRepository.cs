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
                .Include(x => x.CartProduct)
                    .ThenInclude(x => x.Cart)
                .Include(x => x.CreatedByUser)
                .Include(x => x.ModifiedByUser)
                .ToListAsync();
        }

		public async Task<Product> GetWithLinkedEntitiesByIdAsync(Guid id)
        {
            return await this.context.Products
                .Include(x => x.CartProduct)
                    .ThenInclude(x => x.Cart)
                .Include(x => x.CreatedByUser)
                .Include(x => x.ModifiedByUser)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public IEnumerable<Product> GetByCartId(Guid cartId)
        {
            return this.context.CartProduct
                .Include(x => x.Product)
                .Where(x => x.CartId == cartId)
                .Select(x => x.Product)
                .ToList();
        }
        
        //// Async test
        //public async Task<IEnumerable<Product>> GetByCartIdAsync(Guid cartId)
        //{
        //    return await this.context.CartProduct
        //        .Include(x => x.Product)
        //        .Where(x => x.CartId == cartId)
        //        .Select(x => x.Product)
        //        .ToListAsync();
        //}
    }
}
