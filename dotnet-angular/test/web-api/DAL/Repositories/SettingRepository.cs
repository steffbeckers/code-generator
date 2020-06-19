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
	/// The repository for Settings in the data access layer.
	/// </summary>
    public class SettingRepository : Repository<Setting>
    {
        private new readonly RJMContext context;

		/// <summary>
		/// The constructor of the Setting repository.
		/// </summary>
        public SettingRepository(RJMContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides

		public async Task<IEnumerable<Setting>> GetWithLinkedEntitiesAsync()
        {
            return await this.context.Settings
                .Include(x => x.CreatedByUser)
                .Include(x => x.ModifiedByUser)
                .ToListAsync();
        }

		public async Task<Setting> GetWithLinkedEntitiesByIdAsync(Guid id)
        {
            return await this.context.Settings
                .Include(x => x.CreatedByUser)
                .Include(x => x.ModifiedByUser)
                .SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
