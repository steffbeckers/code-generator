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
	/// The repository for Resumes in the data access layer.
	/// </summary>
    public class ResumeRepository : Repository<Resume>
    {
        private new readonly RJMContext context;

		/// <summary>
		/// The constructor of the Resume repository.
		/// </summary>
        public ResumeRepository(RJMContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides

		public async Task<IEnumerable<Resume>> GetWithLinkedEntitiesAsync()
        {
            return await this.context.Resumes
                .Include(x => x.State)
                .Include(x => x.ResumeSkill)
                    .ThenInclude(x => x.Skill)
                .Include(x => x.CreatedByUser)
                .Include(x => x.ModifiedByUser)
                .ToListAsync();
        }

		public async Task<Resume> GetWithLinkedEntitiesByIdAsync(Guid id)
        {
            return await this.context.Resumes
                .Include(x => x.State)
                .Include(x => x.ResumeSkill)
                    .ThenInclude(x => x.Skill)
                .Include(x => x.CreatedByUser)
                .Include(x => x.ModifiedByUser)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public IEnumerable<Resume> GetByStateId(Guid stateId)
        {
            return this.context.Resumes
                .Where(t => t.StateId == stateId)
                .ToList();
        }
        
        //// Async test
        //public async Task<IEnumerable<Resume>> GetByStateIdAsync(Guid stateId)
        //{
        //    return await this.context.Resumes
        //        .Where(t => t.StateId == stateId)
        //        .ToListAsync();
        //}

        public IEnumerable<Resume> GetBySkillId(Guid skillId)
        {
            return this.context.ResumeSkill
                .Include(x => x.Resume)
                .Where(x => x.SkillId == skillId)
                .Select(x => x.Resume)
                .ToList();
        }
        
        //// Async test
        //public async Task<IEnumerable<Resume>> GetBySkillIdAsync(Guid skillId)
        //{
        //    return await this.context.ResumeSkill
        //        .Include(x => x.Resume)
        //        .Where(x => x.SkillId == skillId)
        //        .Select(x => x.Resume)
        //        .ToListAsync();
        //}
    }
}
