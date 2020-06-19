using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RJM.API.Framework;
using RJM.API.Models;

namespace RJM.API.DAL.Repositories
{
	/// <summary>
	/// The repository for JobStates in the data access layer.
	/// </summary>
    public class JobStateRepository : Repository<JobState>
    {
        private new readonly RJMContext context;

		/// <summary>
		/// The constructor of the JobState repository.
		/// </summary>
        public JobStateRepository(RJMContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides

		public async Task<IEnumerable<JobState>> GetWithLinkedEntitiesAsync()
        {
            return await this.context.JobStates
                .Include(x => x.Jobs)
                .Include(x => x.CreatedByUser)
                .Include(x => x.ModifiedByUser)
                .ToListAsync();
        }

		public async Task<JobState> GetWithLinkedEntitiesByIdAsync(Guid id)
        {
            return await this.context.JobStates
                .Include(x => x.Jobs)
                .Include(x => x.CreatedByUser)
                .Include(x => x.ModifiedByUser)
                .SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
