using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.API.Models;

namespace Test.API.DAL.Repositories
{
	/// <summary>
	/// The repository for ProductDetails in the data access layer.
	/// </summary>
    public class ProductDetailRepository : Repository<ProductDetail>
    {
        private new readonly TestContext context;

		/// <summary>
		/// The constructor of the ProductDetail repository.
		/// </summary>
        public ProductDetailRepository(TestContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides

		public async Task<IEnumerable<ProductDetail>> GetWithLinkedEntitiesAsync()
        {
            return await this.context.ProductDetails
                .Include(x => x.Product)
                .ToListAsync();
        }

		public async Task<ProductDetail> GetWithLinkedEntitiesByIdAsync(Guid id)
        {
            return await this.context.ProductDetails
                .Include(x => x.Product)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public IEnumerable<ProductDetail> GetByProductId(Guid productId)
        {
            return this.context.ProductDetails
                .Where(t => t.ProductId == productId)
                .ToList();
        }
        
        //// Async test
        //public async Task<IEnumerable<ProductDetail>> GetByProductIdAsync(Guid productId)
        //{
        //    return await this.context.ProductDetails
        //        .Where(t => t.ProductId == productId)
        //        .ToListAsync();
        //}
    }
}
