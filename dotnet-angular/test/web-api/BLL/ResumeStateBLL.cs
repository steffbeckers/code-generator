using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RJM.API.DAL.Repositories;
using RJM.API.Models;

namespace RJM.API.BLL
{
	/// <summary>
	/// The business logic layer for ResumeStates.
	/// </summary>
    public class ResumeStateBLL
    {
        private readonly ResumeStateRepository resumeStateRepository;

		/// <summary>
		/// The constructor of the ResumeState business logic layer.
		/// </summary>
        public ResumeStateBLL(
			ResumeStateRepository resumeStateRepository
		)
        {
            this.resumeStateRepository = resumeStateRepository;
        }

		/// <summary>
		/// Retrieves all resumestates.
		/// </summary>
		public async Task<IEnumerable<ResumeState>> GetAllResumeStatesAsync()
        {
			// #-#-# {83B8AA9F-713A-42FB-ADE1-8A4AA43886C8}
			// Before retrieval
			// #-#-#

            return await this.resumeStateRepository.GetWithLinkedEntitiesAsync();
        }

		/// <summary>
		/// Retrieves one resumestate by Id.
		/// </summary>
		public async Task<ResumeState> GetResumeStateByIdAsync(Guid id)
        {
			// #-#-# {F838CE2A-D0FB-4F8A-A826-0D653DEECB2B}
			// Before retrieval
			// #-#-#

            return await this.resumeStateRepository.GetWithLinkedEntitiesByIdAsync(id);
        }

		/// <summary>
		/// Creates a new resumestate record.
		/// </summary>
        public async Task<ResumeState> CreateResumeStateAsync(ResumeState resumeState)
        {
            // Validation
            if (resumeState == null) { return null; }

			// Trimming strings
            if (!string.IsNullOrEmpty(resumeState.Name))
                resumeState.Name = resumeState.Name.Trim();
            if (!string.IsNullOrEmpty(resumeState.DisplayName))
                resumeState.DisplayName = resumeState.DisplayName.Trim();

			// #-#-# {D4775AF3-4BFA-496A-AA82-001028A22DD6}
			// Before creation
			// #-#-#

			resumeState = await this.resumeStateRepository.InsertAsync(resumeState);

			// #-#-# {1972C619-D2F2-48FD-8474-3A69621B1F78}
			// After creation
			// #-#-#

            return resumeState;
        }

		/// <summary>
		/// Updates an existing resumestate record by Id.
		/// </summary>
        public async Task<ResumeState> UpdateResumeStateAsync(ResumeState resumeStateUpdate)
        {
            // Validation
            if (resumeStateUpdate == null) { return null; }

            // Retrieve existing
            ResumeState resumeState = await this.resumeStateRepository.GetByIdAsync(resumeStateUpdate.Id);
            if (resumeState == null)
            {
                return null;
            }

			// Trimming strings
            if (!string.IsNullOrEmpty(resumeStateUpdate.Name))
                resumeStateUpdate.Name = resumeStateUpdate.Name.Trim();
            if (!string.IsNullOrEmpty(resumeStateUpdate.DisplayName))
                resumeStateUpdate.DisplayName = resumeStateUpdate.DisplayName.Trim();

            // Mapping
            resumeState.Name = resumeStateUpdate.Name;
            resumeState.DisplayName = resumeStateUpdate.DisplayName;

			// #-#-# {B5914243-E57E-41AE-A7C8-553F2F93267B}
			// Before update
			// #-#-#

			resumeState = await this.resumeStateRepository.UpdateAsync(resumeState);

			// #-#-# {983B1B6C-14A7-4925-8571-D77447DF0ADA}
			// After update
			// #-#-#

            return resumeState;
        }

		/// <summary>
		/// Deletes an existing resumestate record by Id.
		/// </summary>
        public async Task<ResumeState> DeleteResumeStateByIdAsync(Guid resumeStateId)
        {
            ResumeState resumeState = await this.resumeStateRepository.GetByIdAsync(resumeStateId);

            return await this.DeleteResumeStateAsync(resumeState);
        }

		/// <summary>
		/// Deletes an existing resumestate record.
		/// </summary>
        public async Task<ResumeState> DeleteResumeStateAsync(ResumeState resumeState)
        {
            // Validation
            if (resumeState == null) { return null; }

			// #-#-# {FE1A99E0-482D-455B-A8C1-3C2C11FACA58}
			// Before deletion
			// #-#-#

            await this.resumeStateRepository.DeleteAsync(resumeState);

			// #-#-# {F09857C0-44E7-4E6C-B3E6-883C0D28E1A6}
			// After deletion
			// #-#-#

            return resumeState;
        }
    }
}
