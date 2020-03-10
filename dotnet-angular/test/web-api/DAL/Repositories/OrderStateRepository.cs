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
	/// The repository for OrderStates in the data access layer.
	/// </summary>
    public class OrderStateRepository : Repository<OrderState>
    {
        private new readonly TestContext context;

		/// <summary>
		/// The constructor of the OrderState repository.
		/// </summary>
        public OrderStateRepository(TestContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides

		public async Task<IEnumerable<OrderState>> GetWithLinkedEntitiesAsync()
        {
            return await this.context.OrderStates
                .Include(x => x.Orders)
                .Include(x => x.CreatedByUser)
                .Include(x => x.ModifiedByUser)
                .ToListAsync();
        }

		public async Task<OrderState> GetWithLinkedEntitiesByIdAsync(Guid id)
        {
            return await this.context.OrderStates
                .Include(x => x.Orders)
                .Include(x => x.CreatedByUser)
                .Include(x => x.ModifiedByUser)
                .SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
