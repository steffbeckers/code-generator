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
	/// The repository for Documents in the data access layer.
	/// </summary>
    public class DocumentRepository : Repository<Document>
    {
        private new readonly RJMContext context;

		/// <summary>
		/// The constructor of the Document repository.
		/// </summary>
        public DocumentRepository(RJMContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides

		public async Task<IEnumerable<Document>> GetWithLinkedEntitiesAsync()
        {
            return await this.context.Documents
                .Include(x => x.ResumeState)
                .Include(x => x.ResumeSkill)
                    .ThenInclude(x => x.Skill)
                .Include(x => x.CreatedByUser)
                .Include(x => x.ModifiedByUser)
                .ToListAsync();
        }

		public async Task<Document> GetWithLinkedEntitiesByIdAsync(Guid id)
        {
            return await this.context.Documents
                .Include(x => x.ResumeState)
                .Include(x => x.ResumeSkill)
                    .ThenInclude(x => x.Skill)
                .Include(x => x.CreatedByUser)
                .Include(x => x.ModifiedByUser)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public IEnumerable<Document> GetByResumeStateId(Guid resumeStateId)
        {
            return this.context.Documents
                .Where(t => t.ResumeStateId == resumeStateId)
                .ToList();
        }
        
        //// Async test
        //public async Task<IEnumerable<Document>> GetByResumeStateIdAsync(Guid resumeStateId)
        //{
        //    return await this.context.Documents
        //        .Where(t => t.ResumeStateId == resumeStateId)
        //        .ToListAsync();
        //}

        public IEnumerable<Document> GetBySkillId(Guid skillId)
        {
            return this.context.ResumeSkill
                .Include(x => x.Document)
                .Where(x => x.SkillId == skillId)
                .Select(x => x.Document)
                .ToList();
        }
        
        //// Async test
        //public async Task<IEnumerable<Document>> GetBySkillIdAsync(Guid skillId)
        //{
        //    return await this.context.ResumeSkill
        //        .Include(x => x.Document)
        //        .Where(x => x.SkillId == skillId)
        //        .Select(x => x.Document)
        //        .ToListAsync();
        //}
    }
}
