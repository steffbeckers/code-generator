using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RJM.API.DAL.Repositories;
using RJM.API.Models;

namespace RJM.API.BLL
{
	/// <summary>
	/// The business logic layer for Skills.
	/// </summary>
    public class SkillBLL
    {
        private readonly SkillRepository skillRepository;
        private readonly ResumeRepository resumeRepository;
        private readonly ResumeSkillRepository resumeSkillRepository;

		/// <summary>
		/// The constructor of the Skill business logic layer.
		/// </summary>
        public SkillBLL(
			SkillRepository skillRepository,
            ResumeRepository resumeRepository,
			ResumeSkillRepository resumeSkillRepository
		)
        {
            this.skillRepository = skillRepository;
            this.resumeRepository = resumeRepository;
			this.resumeSkillRepository = resumeSkillRepository;
        }

		/// <summary>
		/// Retrieves all skills.
		/// </summary>
		public async Task<IEnumerable<Skill>> GetAllSkillsAsync()
        {
			// #-#-# {83B8AA9F-713A-42FB-ADE1-8A4AA43886C8}
			// Before retrieval
			// #-#-#

            return await this.skillRepository.GetWithLinkedEntitiesAsync();
        }

		/// <summary>
		/// Retrieves one skill by Id.
		/// </summary>
		public async Task<Skill> GetSkillByIdAsync(Guid id)
        {
			// #-#-# {F838CE2A-D0FB-4F8A-A826-0D653DEECB2B}
			// Before retrieval
			// #-#-#

            return await this.skillRepository.GetWithLinkedEntitiesByIdAsync(id);
        }

		/// <summary>
		/// Creates a new skill record.
		/// </summary>
        public async Task<Skill> CreateSkillAsync(Skill skill)
        {
            // Validation
            if (skill == null) { return null; }

			// Trimming strings
            if (!string.IsNullOrEmpty(skill.Name))
                skill.Name = skill.Name.Trim();
            if (!string.IsNullOrEmpty(skill.Description))
                skill.Description = skill.Description.Trim();

			// #-#-# {D4775AF3-4BFA-496A-AA82-001028A22DD6}
			// Before creation
			// #-#-#

			skill = await this.skillRepository.InsertAsync(skill);

			// #-#-# {1972C619-D2F2-48FD-8474-3A69621B1F78}
			// After creation
			// #-#-#

            return skill;
        }

		/// <summary>
		/// Updates an existing skill record by Id.
		/// </summary>
        public async Task<Skill> UpdateSkillAsync(Skill skillUpdate)
        {
            // Validation
            if (skillUpdate == null) { return null; }

            // Retrieve existing
            Skill skill = await this.skillRepository.GetByIdAsync(skillUpdate.Id);
            if (skill == null)
            {
                return null;
            }

			// Trimming strings
            if (!string.IsNullOrEmpty(skillUpdate.Name))
                skillUpdate.Name = skillUpdate.Name.Trim();
            if (!string.IsNullOrEmpty(skillUpdate.Description))
                skillUpdate.Description = skillUpdate.Description.Trim();

            // Mapping
            skill.Name = skillUpdate.Name;
            skill.Description = skillUpdate.Description;
            skill.AliasesId = skillUpdate.AliasesId;

			// #-#-# {B5914243-E57E-41AE-A7C8-553F2F93267B}
			// Before update
			// #-#-#

			skill = await this.skillRepository.UpdateAsync(skill);

			// #-#-# {983B1B6C-14A7-4925-8571-D77447DF0ADA}
			// After update
			// #-#-#

            return skill;
        }

        public async Task<Skill> LinkResumeToSkillAsync(ResumeSkill resumeSkill)
        {
            // Validation
            if (resumeSkill == null) { return null; }

            // Check if skill exists
            Skill skill = await this.skillRepository.GetByIdAsync(resumeSkill.SkillId);
            if (skill == null)
            {
                return null;
            }

            // Check if resume exists
            Resume resume = await this.resumeRepository.GetByIdAsync(resumeSkill.ResumeId);
            if (resume == null)
            {
                return null;
            }

            // Retrieve existing link
            ResumeSkill resumeSkillLink = this.resumeSkillRepository.GetBySkillAndResumeId(resumeSkill.SkillId, resumeSkill.ResumeId);

            if (resumeSkillLink == null)
            {
                await this.resumeSkillRepository.InsertAsync(resumeSkill);
            }
            else
            {
                // Mapping of fields on many-to-many
                resumeSkillLink.Rating = resumeSkill.Rating;
                resumeSkillLink.Description = resumeSkill.Description;

                await this.resumeSkillRepository.UpdateAsync(resumeSkillLink);
            }

            return await this.GetSkillByIdAsync(resumeSkill.SkillId);
        }

        public async Task<Skill> UnlinkResumeFromSkillAsync(ResumeSkill resumeSkill)
        {
            // Validation
            if (resumeSkill == null) { return null; }

            // Retrieve existing link
            ResumeSkill resumeSkillLink = this.resumeSkillRepository.GetBySkillAndResumeId(resumeSkill.SkillId, resumeSkill.ResumeId);
		
            if (resumeSkillLink != null)
            {
                await this.resumeSkillRepository.DeleteAsync(resumeSkillLink);
            }

            return await this.GetSkillByIdAsync(resumeSkill.SkillId);
        }

		/// <summary>
		/// Deletes an existing skill record by Id.
		/// </summary>
        public async Task<Skill> DeleteSkillByIdAsync(Guid skillId)
        {
            Skill skill = await this.skillRepository.GetByIdAsync(skillId);

            return await this.DeleteSkillAsync(skill);
        }

		/// <summary>
		/// Deletes an existing skill record.
		/// </summary>
        public async Task<Skill> DeleteSkillAsync(Skill skill)
        {
            // Validation
            if (skill == null) { return null; }

			// #-#-# {FE1A99E0-482D-455B-A8C1-3C2C11FACA58}
			// Before deletion
			// #-#-#

            await this.skillRepository.DeleteAsync(skill);

			// #-#-# {F09857C0-44E7-4E6C-B3E6-883C0D28E1A6}
			// After deletion
			// #-#-#

            return skill;
        }
    }
}
