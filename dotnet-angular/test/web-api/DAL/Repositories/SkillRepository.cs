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
	/// The repository for Skills in the data access layer.
	/// </summary>
    public class SkillRepository : Repository<Skill>
    {
        private new readonly RJMContext context;

		/// <summary>
		/// The constructor of the Skill repository.
		/// </summary>
        public SkillRepository(RJMContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides

		public async Task<IEnumerable<Skill>> GetWithLinkedEntitiesAsync()
        {
            return await this.context.Skills
                .Include(x => x.Aliases)
                .Include(x => x.ResumeSkill)
                    .ThenInclude(x => x.Resume)
                .Include(x => x.JobSkill)
                    .ThenInclude(x => x.Job)
                .Include(x => x.CreatedByUser)
                .Include(x => x.ModifiedByUser)
                .ToListAsync();
        }

		public async Task<Skill> GetWithLinkedEntitiesByIdAsync(Guid id)
        {
            return await this.context.Skills
                .Include(x => x.Aliases)
                .Include(x => x.ResumeSkill)
                    .ThenInclude(x => x.Resume)
                .Include(x => x.JobSkill)
                    .ThenInclude(x => x.Job)
                .Include(x => x.CreatedByUser)
                .Include(x => x.ModifiedByUser)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public IEnumerable<Skill> GetByResumeId(Guid resumeId)
        {
            return this.context.ResumeSkill
                .Include(x => x.Skill)
                .Where(x => x.ResumeId == resumeId)
                .Select(x => x.Skill)
                .ToList();
        }
        
        //// Async test
        //public async Task<IEnumerable<Skill>> GetByResumeIdAsync(Guid resumeId)
        //{
        //    return await this.context.ResumeSkill
        //        .Include(x => x.Skill)
        //        .Where(x => x.ResumeId == resumeId)
        //        .Select(x => x.Skill)
        //        .ToListAsync();
        //}

        public IEnumerable<Skill> GetByJobId(Guid jobId)
        {
            return this.context.JobSkill
                .Include(x => x.Skill)
                .Where(x => x.JobId == jobId)
                .Select(x => x.Skill)
                .ToList();
        }
        
        //// Async test
        //public async Task<IEnumerable<Skill>> GetByJobIdAsync(Guid jobId)
        //{
        //    return await this.context.JobSkill
        //        .Include(x => x.Skill)
        //        .Where(x => x.JobId == jobId)
        //        .Select(x => x.Skill)
        //        .ToListAsync();
        //}
    }
}
