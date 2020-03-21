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
	/// The repository for Jobs in the data access layer.
	/// </summary>
    public class JobRepository : Repository<Job>
    {
        private new readonly RJMContext context;

		/// <summary>
		/// The constructor of the Job repository.
		/// </summary>
        public JobRepository(RJMContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides

		public async Task<IEnumerable<Job>> GetWithLinkedEntitiesAsync()
        {
            return await this.context.Jobs
                .Include(x => x.JobState)
                .Include(x => x.JobSkill)
                    .ThenInclude(x => x.Skill)
                .Include(x => x.CreatedByUser)
                .Include(x => x.ModifiedByUser)
                .ToListAsync();
        }

		public async Task<Job> GetWithLinkedEntitiesByIdAsync(Guid id)
        {
            return await this.context.Jobs
                .Include(x => x.JobState)
                .Include(x => x.JobSkill)
                    .ThenInclude(x => x.Skill)
                .Include(x => x.CreatedByUser)
                .Include(x => x.ModifiedByUser)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public IEnumerable<Job> GetByJobStateId(Guid jobStateId)
        {
            return this.context.Jobs
                .Where(t => t.JobStateId == jobStateId)
                .ToList();
        }
        
        //// Async test
        //public async Task<IEnumerable<Job>> GetByJobStateIdAsync(Guid jobStateId)
        //{
        //    return await this.context.Jobs
        //        .Where(t => t.JobStateId == jobStateId)
        //        .ToListAsync();
        //}

        public IEnumerable<Job> GetBySkillId(Guid skillId)
        {
            return this.context.JobSkill
                .Include(x => x.Job)
                .Where(x => x.SkillId == skillId)
                .Select(x => x.Job)
                .ToList();
        }
        
        //// Async test
        //public async Task<IEnumerable<Job>> GetBySkillIdAsync(Guid skillId)
        //{
        //    return await this.context.JobSkill
        //        .Include(x => x.Job)
        //        .Where(x => x.SkillId == skillId)
        //        .Select(x => x.Job)
        //        .ToListAsync();
        //}
    }
}
