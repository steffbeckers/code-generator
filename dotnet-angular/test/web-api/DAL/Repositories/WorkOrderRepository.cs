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
	/// The repository for WorkOrders in the data access layer.
	/// </summary>
    public class WorkOrderRepository : Repository<WorkOrder>
    {
        private new readonly TestContext context;

		/// <summary>
		/// The constructor of the WorkOrder repository.
		/// </summary>
        public WorkOrderRepository(TestContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides

		public async Task<IEnumerable<WorkOrder>> GetWithLinkedEntitiesAsync()
        {
            return await this.context.WorkOrders
                .Include(x => x.CreatedByUser)
                .Include(x => x.ModifiedByUser)
                .ToListAsync();
        }

		public async Task<WorkOrder> GetWithLinkedEntitiesByIdAsync(Guid id)
        {
            return await this.context.WorkOrders
                .Include(x => x.CreatedByUser)
                .Include(x => x.ModifiedByUser)
                .SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
