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
	/// The repository for ResumeSkills in the data access layer.
	/// </summary>
    public class ResumeSkillRepository : Repository<ResumeSkill>
    {
        private new readonly RJMContext context;

		/// <summary>
		/// The constructor of the ResumeSkill repository.
		/// </summary>
        public ResumeSkillRepository(RJMContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides

        public ResumeSkill GetByResumeAndSkillId(Guid resumeId, Guid skillId)
        {
            return this.context.ResumeSkill
                .Where(x => x.ResumeId == resumeId && x.SkillId == skillId)
                .SingleOrDefault();
        }

		public ResumeSkill GetBySkillAndResumeId(Guid skillId, Guid resumeId)
        {
            return this.context.ResumeSkill
                .Where(x => x.SkillId == skillId && x.ResumeId == resumeId)
                .SingleOrDefault();
        }
    }
}
