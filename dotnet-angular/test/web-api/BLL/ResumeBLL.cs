using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RJM.API.DAL.Repositories;
using RJM.API.Models;

namespace RJM.API.BLL
{
	/// <summary>
	/// The business logic layer for Resumes.
	/// </summary>
    public class ResumeBLL
    {
        private readonly ResumeRepository resumeRepository;
        private readonly DocumentRepository documentRepository;
        private readonly DocumentResumeRepository documentResumeRepository;
        private readonly SkillRepository skillRepository;
        private readonly ResumeSkillRepository resumeSkillRepository;

		/// <summary>
		/// The constructor of the Resume business logic layer.
		/// </summary>
        public ResumeBLL(
			ResumeRepository resumeRepository,
            DocumentRepository documentRepository,
			DocumentResumeRepository documentResumeRepository,
            SkillRepository skillRepository,
			ResumeSkillRepository resumeSkillRepository
		)
        {
            this.resumeRepository = resumeRepository;
            this.documentRepository = documentRepository;
			this.documentResumeRepository = documentResumeRepository;
            this.skillRepository = skillRepository;
			this.resumeSkillRepository = resumeSkillRepository;
        }

		/// <summary>
		/// Retrieves all resumes.
		/// </summary>
		public async Task<IEnumerable<Resume>> GetAllResumesAsync()
        {
			// #-#-# {83B8AA9F-713A-42FB-ADE1-8A4AA43886C8}
			// Before retrieval
			// #-#-#

            return await this.resumeRepository.GetWithLinkedEntitiesAsync();
        }

		/// <summary>
		/// Retrieves one resume by Id.
		/// </summary>
		public async Task<Resume> GetResumeByIdAsync(Guid id)
        {
			// #-#-# {F838CE2A-D0FB-4F8A-A826-0D653DEECB2B}
			// Before retrieval
			// #-#-#

            return await this.resumeRepository.GetWithLinkedEntitiesByIdAsync(id);
        }

		/// <summary>
		/// Creates a new resume record.
		/// </summary>
        public async Task<Resume> CreateResumeAsync(Resume resume)
        {
            // Validation
            if (resume == null) { return null; }

			// Trimming strings
            if (!string.IsNullOrEmpty(resume.Name))
                resume.Name = resume.Name.Trim();
            if (!string.IsNullOrEmpty(resume.DisplayName))
                resume.DisplayName = resume.DisplayName.Trim();
            if (!string.IsNullOrEmpty(resume.JobTitle))
                resume.JobTitle = resume.JobTitle.Trim();
            if (!string.IsNullOrEmpty(resume.Description))
                resume.Description = resume.Description.Trim();

			// #-#-# {D4775AF3-4BFA-496A-AA82-001028A22DD6}
			// Before creation
			// #-#-#

			resume = await this.resumeRepository.InsertAsync(resume);

			// #-#-# {1972C619-D2F2-48FD-8474-3A69621B1F78}
			// After creation
			// #-#-#

            return resume;
        }

		/// <summary>
		/// Updates an existing resume record by Id.
		/// </summary>
        public async Task<Resume> UpdateResumeAsync(Resume resumeUpdate)
        {
            // Validation
            if (resumeUpdate == null) { return null; }

            // Retrieve existing
            Resume resume = await this.resumeRepository.GetByIdAsync(resumeUpdate.Id);
            if (resume == null)
            {
                return null;
            }

			// Trimming strings
            if (!string.IsNullOrEmpty(resumeUpdate.Name))
                resumeUpdate.Name = resumeUpdate.Name.Trim();
            if (!string.IsNullOrEmpty(resumeUpdate.DisplayName))
                resumeUpdate.DisplayName = resumeUpdate.DisplayName.Trim();
            if (!string.IsNullOrEmpty(resumeUpdate.JobTitle))
                resumeUpdate.JobTitle = resumeUpdate.JobTitle.Trim();
            if (!string.IsNullOrEmpty(resumeUpdate.Description))
                resumeUpdate.Description = resumeUpdate.Description.Trim();

            // Mapping
            resume.Name = resumeUpdate.Name;
            resume.DisplayName = resumeUpdate.DisplayName;
            resume.JobTitle = resumeUpdate.JobTitle;
            resume.Description = resumeUpdate.Description;
            resume.ResumeStateId = resumeUpdate.ResumeStateId;

			// #-#-# {B5914243-E57E-41AE-A7C8-553F2F93267B}
			// Before update
			// #-#-#

			resume = await this.resumeRepository.UpdateAsync(resume);

			// #-#-# {983B1B6C-14A7-4925-8571-D77447DF0ADA}
			// After update
			// #-#-#

            return resume;
        }

        public async Task<Resume> LinkDocumentToResumeAsync(DocumentResume documentResume)
        {
            // Validation
            if (documentResume == null) { return null; }

            // Check if resume exists
            Resume resume = await this.resumeRepository.GetByIdAsync(documentResume.ResumeId);
            if (resume == null)
            {
                return null;
            }

            // Check if document exists
            Document document = await this.documentRepository.GetByIdAsync(documentResume.DocumentId);
            if (document == null)
            {
                return null;
            }

            // Retrieve existing link
            DocumentResume documentResumeLink = this.documentResumeRepository.GetByResumeAndDocumentId(documentResume.ResumeId, documentResume.DocumentId);

            if (documentResumeLink == null)
            {
                await this.documentResumeRepository.InsertAsync(documentResume);
            }
            else
            {
                // Mapping of fields on many-to-many

                await this.documentResumeRepository.UpdateAsync(documentResumeLink);
            }

            return await this.GetResumeByIdAsync(documentResume.ResumeId);
        }

        public async Task<Resume> LinkSkillToResumeAsync(ResumeSkill resumeSkill)
        {
            // Validation
            if (resumeSkill == null) { return null; }

            // Check if resume exists
            Resume resume = await this.resumeRepository.GetByIdAsync(resumeSkill.ResumeId);
            if (resume == null)
            {
                return null;
            }

            // Check if skill exists
            Skill skill = await this.skillRepository.GetByIdAsync(resumeSkill.SkillId);
            if (skill == null)
            {
                return null;
            }

            // Retrieve existing link
            ResumeSkill resumeSkillLink = this.resumeSkillRepository.GetByResumeAndSkillId(resumeSkill.ResumeId, resumeSkill.SkillId);

            if (resumeSkillLink == null)
            {
                await this.resumeSkillRepository.InsertAsync(resumeSkill);
            }
            else
            {
                // Mapping of fields on many-to-many
                resumeSkillLink.Level = resumeSkill.Level;
                resumeSkillLink.Description = resumeSkill.Description;

                await this.resumeSkillRepository.UpdateAsync(resumeSkillLink);
            }

            return await this.GetResumeByIdAsync(resumeSkill.ResumeId);
        }

        public async Task<Resume> UnlinkDocumentFromResumeAsync(DocumentResume documentResume)
        {
            // Validation
            if (documentResume == null) { return null; }

            // Retrieve existing link
            DocumentResume documentResumeLink = this.documentResumeRepository.GetByResumeAndDocumentId(documentResume.ResumeId, documentResume.DocumentId);
		
            if (documentResumeLink != null)
            {
                await this.documentResumeRepository.DeleteAsync(documentResumeLink);
            }

            return await this.GetResumeByIdAsync(documentResume.ResumeId);
        }

        public async Task<Resume> UnlinkSkillFromResumeAsync(ResumeSkill resumeSkill)
        {
            // Validation
            if (resumeSkill == null) { return null; }

            // Retrieve existing link
            ResumeSkill resumeSkillLink = this.resumeSkillRepository.GetByResumeAndSkillId(resumeSkill.ResumeId, resumeSkill.SkillId);
		
            if (resumeSkillLink != null)
            {
                await this.resumeSkillRepository.DeleteAsync(resumeSkillLink);
            }

            return await this.GetResumeByIdAsync(resumeSkill.ResumeId);
        }

		/// <summary>
		/// Deletes an existing resume record by Id.
		/// </summary>
        public async Task<Resume> DeleteResumeByIdAsync(Guid resumeId)
        {
            Resume resume = await this.resumeRepository.GetByIdAsync(resumeId);

            return await this.DeleteResumeAsync(resume);
        }

		/// <summary>
		/// Deletes an existing resume record.
		/// </summary>
        public async Task<Resume> DeleteResumeAsync(Resume resume)
        {
            // Validation
            if (resume == null) { return null; }

			// #-#-# {FE1A99E0-482D-455B-A8C1-3C2C11FACA58}
			// Before deletion
			// #-#-#

            await this.resumeRepository.DeleteAsync(resume);

			// #-#-# {F09857C0-44E7-4E6C-B3E6-883C0D28E1A6}
			// After deletion
			// #-#-#

            return resume;
        }
    }
}
