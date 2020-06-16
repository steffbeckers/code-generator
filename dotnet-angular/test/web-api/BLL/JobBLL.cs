using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RJM.API.DAL.Repositories;
using RJM.API.Models;

namespace RJM.API.BLL
{
	/// <summary>
	/// The business logic layer for Jobs.
	/// </summary>
    public class JobBLL
    {
        private readonly JobRepository jobRepository;
        private readonly SkillRepository skillRepository;
        private readonly JobSkillRepository jobSkillRepository;

		/// <summary>
		/// The constructor of the Job business logic layer.
		/// </summary>
        public JobBLL(
			JobRepository jobRepository,
            SkillRepository skillRepository,
			JobSkillRepository jobSkillRepository
		)
        {
            this.jobRepository = jobRepository;
            this.skillRepository = skillRepository;
			this.jobSkillRepository = jobSkillRepository;
        }

		/// <summary>
		/// Retrieves all jobs.
		/// </summary>
		public async Task<IEnumerable<Job>> GetAllJobsAsync()
        {
			// #-#-# {83B8AA9F-713A-42FB-ADE1-8A4AA43886C8}
			// Before retrieval
			// #-#-#

            return await this.jobRepository.GetWithLinkedEntitiesAsync();
        }

		/// <summary>
		/// Retrieves one job by Id.
		/// </summary>
		public async Task<Job> GetJobByIdAsync(Guid id)
        {
			// #-#-# {F838CE2A-D0FB-4F8A-A826-0D653DEECB2B}
			// Before retrieval
			// #-#-#

            return await this.jobRepository.GetWithLinkedEntitiesByIdAsync(id);
        }

		/// <summary>
		/// Creates a new job record.
		/// </summary>
        public async Task<Job> CreateJobAsync(Job job)
        {
            // Validation
            if (job == null) { return null; }

			// Trimming strings
            if (!string.IsNullOrEmpty(job.Title))
                job.Title = job.Title.Trim();
            if (!string.IsNullOrEmpty(job.Description))
                job.Description = job.Description.Trim();

			// #-#-# {D4775AF3-4BFA-496A-AA82-001028A22DD6}
			// Before creation
			// #-#-#

			job = await this.jobRepository.InsertAsync(job);

			// #-#-# {1972C619-D2F2-48FD-8474-3A69621B1F78}
			// After creation
			// #-#-#

            return job;
        }

		/// <summary>
		/// Updates an existing job record by Id.
		/// </summary>
        public async Task<Job> UpdateJobAsync(Job jobUpdate)
        {
            // Validation
            if (jobUpdate == null) { return null; }

            // Retrieve existing
            Job job = await this.jobRepository.GetByIdAsync(jobUpdate.Id);
            if (job == null)
            {
                return null;
            }

			// Trimming strings
            if (!string.IsNullOrEmpty(jobUpdate.Title))
                jobUpdate.Title = jobUpdate.Title.Trim();
            if (!string.IsNullOrEmpty(jobUpdate.Description))
                jobUpdate.Description = jobUpdate.Description.Trim();

            // Mapping
            job.Title = jobUpdate.Title;
            job.Description = jobUpdate.Description;
            job.JobStateId = jobUpdate.JobStateId;

			// #-#-# {B5914243-E57E-41AE-A7C8-553F2F93267B}
			// Before update
			// #-#-#

			job = await this.jobRepository.UpdateAsync(job);

			// #-#-# {983B1B6C-14A7-4925-8571-D77447DF0ADA}
			// After update
			// #-#-#

            return job;
        }

        public async Task<Job> LinkSkillToJobAsync(JobSkill jobSkill)
        {
            // Validation
            if (jobSkill == null) { return null; }

            // Check if job exists
            Job job = await this.jobRepository.GetByIdAsync(jobSkill.JobId);
            if (job == null)
            {
                return null;
            }

            // Check if skill exists
            Skill skill = await this.skillRepository.GetByIdAsync(jobSkill.SkillId);
            if (skill == null)
            {
                return null;
            }

            // Retrieve existing link
            JobSkill jobSkillLink = this.jobSkillRepository.GetByJobAndSkillId(jobSkill.JobId, jobSkill.SkillId);

            if (jobSkillLink == null)
            {
                await this.jobSkillRepository.InsertAsync(jobSkill);
            }
            else
            {
                // Mapping of fields on many-to-many
                jobSkillLink.Level = jobSkill.Level;
                jobSkillLink.Description = jobSkill.Description;

                await this.jobSkillRepository.UpdateAsync(jobSkillLink);
            }

            return await this.GetJobByIdAsync(jobSkill.JobId);
        }

        public async Task<Job> UnlinkSkillFromJobAsync(JobSkill jobSkill)
        {
            // Validation
            if (jobSkill == null) { return null; }

            // Retrieve existing link
            JobSkill jobSkillLink = this.jobSkillRepository.GetByJobAndSkillId(jobSkill.JobId, jobSkill.SkillId);
		
            if (jobSkillLink != null)
            {
                await this.jobSkillRepository.DeleteAsync(jobSkillLink);
            }

            return await this.GetJobByIdAsync(jobSkill.JobId);
        }

		/// <summary>
		/// Deletes an existing job record by Id.
		/// </summary>
        public async Task<Job> DeleteJobByIdAsync(Guid jobId)
        {
            Job job = await this.jobRepository.GetByIdAsync(jobId);

            return await this.DeleteJobAsync(job);
        }

		/// <summary>
		/// Deletes an existing job record.
		/// </summary>
        public async Task<Job> DeleteJobAsync(Job job)
        {
            // Validation
            if (job == null) { return null; }

			// #-#-# {FE1A99E0-482D-455B-A8C1-3C2C11FACA58}
			// Before deletion
			// #-#-#

            await this.jobRepository.DeleteAsync(job);

			// #-#-# {F09857C0-44E7-4E6C-B3E6-883C0D28E1A6}
			// After deletion
			// #-#-#

            return job;
        }
    }
}
