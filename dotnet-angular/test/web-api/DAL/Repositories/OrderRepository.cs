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
	/// The repository for Orders in the data access layer.
	/// </summary>
    public class OrderRepository : Repository<Order>
    {
        private new readonly TestContext context;

		/// <summary>
		/// The constructor of the Order repository.
		/// </summary>
        public OrderRepository(TestContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides

		public async Task<IEnumerable<Order>> GetWithLinkedEntitiesAsync()
        {
            return await this.context.Orders
                .Include(x => x.OrderState)
                .Include(x => x.CreatedByUser)
                .Include(x => x.ModifiedByUser)
                .ToListAsync();
        }

		public async Task<Order> GetWithLinkedEntitiesByIdAsync(Guid id)
        {
            return await this.context.Orders
                .Include(x => x.OrderState)
                .Include(x => x.CreatedByUser)
                .Include(x => x.ModifiedByUser)
                .SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
