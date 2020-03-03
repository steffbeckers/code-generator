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
	/// The repository for ProductSuppliers in the data access layer.
	/// </summary>
    public class ProductSupplierRepository : Repository<ProductSupplier>
    {
        private new readonly TestContext context;

		/// <summary>
		/// The constructor of the ProductSupplier repository.
		/// </summary>
        public ProductSupplierRepository(TestContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides

        public ProductSupplier GetByProductAndSupplierId(Guid productId, Guid supplierId)
        {
            return this.context.ProductSupplier
                .Where(x => x.ProductId == productId && x.SupplierId == supplierId)
                .SingleOrDefault();
        }

		public ProductSupplier GetBySupplierAndProductId(Guid supplierId, Guid productId)
        {
            return this.context.ProductSupplier
                .Where(x => x.SupplierId == supplierId && x.ProductId == productId)
                .SingleOrDefault();
        }
    }
}
