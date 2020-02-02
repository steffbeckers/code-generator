using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                .Include(x => x.Details)
                .Include(x => x.ProductSupplier)
                    .ThenInclude(x => x.Supplier)
                .ToListAsync();
        }

		public async Task<Product> GetWithLinkedEntitiesByIdAsync(Guid id)
        {
            return await this.context.Products
                .Include(x => x.Details)
                .Include(x => x.ProductSupplier)
                    .ThenInclude(x => x.Supplier)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public IEnumerable<Product> GetBySupplierId(Guid supplierId)
        {
            return this.context.ProductSupplier
                .Include(x => x.Product)
                .Where(x => x.SupplierId == supplierId)
                .Select(x => x.Product)
                .ToList();
        }
        
        //// Async test
        //public async Task<IEnumerable<Product>> GetBySupplierIdAsync(Guid supplierId)
        //{
        //    return await this.context.ProductSupplier
        //        .Include(x => x.Product)
        //        .Where(x => x.SupplierId == supplierId)
        //        .Select(x => x.Product)
        //        .ToListAsync();
        //}
    }
}
