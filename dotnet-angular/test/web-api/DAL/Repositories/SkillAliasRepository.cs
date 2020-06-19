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
	/// The repository for SkillAliases in the data access layer.
	/// </summary>
    public class SkillAliasRepository : Repository<SkillAlias>
    {
        private new readonly RJMContext context;

		/// <summary>
		/// The constructor of the SkillAlias repository.
		/// </summary>
        public SkillAliasRepository(RJMContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides

		public async Task<IEnumerable<SkillAlias>> GetWithLinkedEntitiesAsync()
        {
            return await this.context.SkillAliases
                .Include(x => x.Skill)
                .Include(x => x.CreatedByUser)
                .Include(x => x.ModifiedByUser)
                .ToListAsync();
        }

		public async Task<SkillAlias> GetWithLinkedEntitiesByIdAsync(Guid id)
        {
            return await this.context.SkillAliases
                .Include(x => x.Skill)
                .Include(x => x.CreatedByUser)
                .Include(x => x.ModifiedByUser)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public IEnumerable<SkillAlias> GetBySkillId(Guid skillId)
        {
            return this.context.SkillAliases
                .Where(t => t.SkillId == skillId)
                .ToList();
        }
        
        //// Async test
        //public async Task<IEnumerable<SkillAlias>> GetBySkillIdAsync(Guid skillId)
        //{
        //    return await this.context.SkillAliases
        //        .Where(t => t.SkillId == skillId)
        //        .ToListAsync();
        //}
    }
}
