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
	/// The repository for Carts in the data access layer.
	/// </summary>
    public class CartRepository : Repository<Cart>
    {
        private new readonly TestContext context;

		/// <summary>
		/// The constructor of the Cart repository.
		/// </summary>
        public CartRepository(TestContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides

		public async Task<IEnumerable<Cart>> GetWithLinkedEntitiesAsync()
        {
            return await this.context.Carts
                .Include(x => x.CartProduct)
                    .ThenInclude(x => x.Product)
                .Include(x => x.CreatedByUser)
                .Include(x => x.ModifiedByUser)
                .ToListAsync();
        }

		public async Task<Cart> GetWithLinkedEntitiesByIdAsync(Guid id)
        {
            return await this.context.Carts
                .Include(x => x.CartProduct)
                    .ThenInclude(x => x.Product)
                .Include(x => x.CreatedByUser)
                .Include(x => x.ModifiedByUser)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public IEnumerable<Cart> GetByProductId(Guid productId)
        {
            return this.context.CartProduct
                .Include(x => x.Cart)
                .Where(x => x.ProductId == productId)
                .Select(x => x.Cart)
                .ToList();
        }
        
        //// Async test
        //public async Task<IEnumerable<Cart>> GetByProductIdAsync(Guid productId)
        //{
        //    return await this.context.CartProduct
        //        .Include(x => x.Cart)
        //        .Where(x => x.ProductId == productId)
        //        .Select(x => x.Cart)
        //        .ToListAsync();
        //}
    }
}
