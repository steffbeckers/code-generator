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
	/// The repository for RelationTypes in the data access layer.
	/// </summary>
    public class RelationTypeRepository : Repository<RelationType>
    {
        private new readonly TestContext context;

		/// <summary>
		/// The constructor of the RelationType repository.
		/// </summary>
        public RelationTypeRepository(TestContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides

		public async Task<IEnumerable<RelationType>> GetWithLinkedEntitiesAsync()
        {
            return await this.context.RelationTypes
                .Include(x => x.CreatedByUser)
                .Include(x => x.ModifiedByUser)
                .ToListAsync();
        }

		public async Task<RelationType> GetWithLinkedEntitiesByIdAsync(Guid id)
        {
            return await this.context.RelationTypes
                .Include(x => x.CreatedByUser)
                .Include(x => x.ModifiedByUser)
                .SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
