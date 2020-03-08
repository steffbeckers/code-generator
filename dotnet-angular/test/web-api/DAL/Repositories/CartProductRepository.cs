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
	/// The repository for CartProducts in the data access layer.
	/// </summary>
    public class CartProductRepository : Repository<CartProduct>
    {
        private new readonly TestContext context;

		/// <summary>
		/// The constructor of the CartProduct repository.
		/// </summary>
        public CartProductRepository(TestContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides

        public CartProduct GetByCartAndProductId(Guid cartId, Guid productId)
        {
            return this.context.CartProduct
                .Where(x => x.CartId == cartId && x.ProductId == productId)
                .SingleOrDefault();
        }

		public CartProduct GetByProductAndCartId(Guid productId, Guid cartId)
        {
            return this.context.CartProduct
                .Where(x => x.ProductId == productId && x.CartId == cartId)
                .SingleOrDefault();
        }
    }
}
