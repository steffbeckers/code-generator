using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RJM.API.DAL.Repositories;
using RJM.API.Models;

namespace RJM.API.BLL
{
	/// <summary>
	/// The business logic layer for JobStates.
	/// </summary>
    public class JobStateBLL
    {
        private readonly JobStateRepository jobStateRepository;

		/// <summary>
		/// The constructor of the JobState business logic layer.
		/// </summary>
        public JobStateBLL(
			JobStateRepository jobStateRepository
		)
        {
            this.jobStateRepository = jobStateRepository;
        }

		/// <summary>
		/// Retrieves all jobstates.
		/// </summary>
		public async Task<IEnumerable<JobState>> GetAllJobStatesAsync()
        {
			// #-#-# {83B8AA9F-713A-42FB-ADE1-8A4AA43886C8}
			// Before retrieval
			// #-#-#

            return await this.jobStateRepository.GetWithLinkedEntitiesAsync();
        }

		/// <summary>
		/// Retrieves one jobstate by Id.
		/// </summary>
		public async Task<JobState> GetJobStateByIdAsync(Guid id)
        {
			// #-#-# {F838CE2A-D0FB-4F8A-A826-0D653DEECB2B}
			// Before retrieval
			// #-#-#

            return await this.jobStateRepository.GetWithLinkedEntitiesByIdAsync(id);
        }

		/// <summary>
		/// Creates a new jobstate record.
		/// </summary>
        public async Task<JobState> CreateJobStateAsync(JobState jobState)
        {
            // Validation
            if (jobState == null) { return null; }

			// Trimming strings
            if (!string.IsNullOrEmpty(jobState.Name))
                jobState.Name = jobState.Name.Trim();
            if (!string.IsNullOrEmpty(jobState.DisplayName))
                jobState.DisplayName = jobState.DisplayName.Trim();

			// #-#-# {D4775AF3-4BFA-496A-AA82-001028A22DD6}
			// Before creation
			// #-#-#

			jobState = await this.jobStateRepository.InsertAsync(jobState);

			// #-#-# {1972C619-D2F2-48FD-8474-3A69621B1F78}
			// After creation
			// #-#-#

            return jobState;
        }

		/// <summary>
		/// Updates an existing jobstate record by Id.
		/// </summary>
        public async Task<JobState> UpdateJobStateAsync(JobState jobStateUpdate)
        {
            // Validation
            if (jobStateUpdate == null) { return null; }

            // Retrieve existing
            JobState jobState = await this.jobStateRepository.GetByIdAsync(jobStateUpdate.Id);
            if (jobState == null)
            {
                return null;
            }

			// Trimming strings
            if (!string.IsNullOrEmpty(jobStateUpdate.Name))
                jobStateUpdate.Name = jobStateUpdate.Name.Trim();
            if (!string.IsNullOrEmpty(jobStateUpdate.DisplayName))
                jobStateUpdate.DisplayName = jobStateUpdate.DisplayName.Trim();

            // Mapping
            jobState.Name = jobStateUpdate.Name;
            jobState.DisplayName = jobStateUpdate.DisplayName;

			// #-#-# {B5914243-E57E-41AE-A7C8-553F2F93267B}
			// Before update
			// #-#-#

			jobState = await this.jobStateRepository.UpdateAsync(jobState);

			// #-#-# {983B1B6C-14A7-4925-8571-D77447DF0ADA}
			// After update
			// #-#-#

            return jobState;
        }

		/// <summary>
		/// Deletes an existing jobstate record by Id.
		/// </summary>
        public async Task<JobState> DeleteJobStateByIdAsync(Guid jobStateId)
        {
            JobState jobState = await this.jobStateRepository.GetByIdAsync(jobStateId);

            return await this.DeleteJobStateAsync(jobState);
        }

		/// <summary>
		/// Deletes an existing jobstate record.
		/// </summary>
        public async Task<JobState> DeleteJobStateAsync(JobState jobState)
        {
            // Validation
            if (jobState == null) { return null; }

			// #-#-# {FE1A99E0-482D-455B-A8C1-3C2C11FACA58}
			// Before deletion
			// #-#-#

            await this.jobStateRepository.DeleteAsync(jobState);

			// #-#-# {F09857C0-44E7-4E6C-B3E6-883C0D28E1A6}
			// After deletion
			// #-#-#

            return jobState;
        }
    }
}
