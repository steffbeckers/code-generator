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
	/// The repository for Suppliers in the data access layer.
	/// </summary>
    public class SupplierRepository : Repository<Supplier>
    {
        private new readonly TestContext context;

		/// <summary>
		/// The constructor of the Supplier repository.
		/// </summary>
        public SupplierRepository(TestContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides

		public async Task<IEnumerable<Supplier>> GetWithLinkedEntitiesAsync()
        {
            return await this.context.Suppliers
                .Include(x => x.ProductSupplier)
                    .ThenInclude(x => x.Product)
                .ToListAsync();
        }

		public async Task<Supplier> GetWithLinkedEntitiesByIdAsync(Guid id)
        {
            return await this.context.Suppliers
                .Include(x => x.ProductSupplier)
                    .ThenInclude(x => x.Product)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public IEnumerable<Supplier> GetByProductId(Guid productId)
        {
            return this.context.ProductSupplier
                .Include(x => x.Supplier)
                .Where(x => x.ProductId == productId)
                .Select(x => x.Supplier)
                .ToList();
        }
        
        //// Async test
        //public async Task<IEnumerable<Supplier>> GetByProductIdAsync(Guid productId)
        //{
        //    return await this.context.ProductSupplier
        //        .Include(x => x.Supplier)
        //        .Where(x => x.ProductId == productId)
        //        .Select(x => x.Supplier)
        //        .ToListAsync();
        //}
    }
}
