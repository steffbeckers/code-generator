using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RJM.API.DAL.Repositories;
using RJM.API.Models;

namespace RJM.API.BLL
{
	/// <summary>
	/// The business logic layer for SkillAliases.
	/// </summary>
    public class SkillAliasBLL
    {
        private readonly SkillAliasRepository skillAliasRepository;

		/// <summary>
		/// The constructor of the SkillAlias business logic layer.
		/// </summary>
        public SkillAliasBLL(
			SkillAliasRepository skillAliasRepository
		)
        {
            this.skillAliasRepository = skillAliasRepository;
        }

		/// <summary>
		/// Retrieves all skillaliases.
		/// </summary>
		public async Task<IEnumerable<SkillAlias>> GetAllSkillAliasesAsync()
        {
			// #-#-# {83B8AA9F-713A-42FB-ADE1-8A4AA43886C8}
			// Before retrieval
			// #-#-#

            return await this.skillAliasRepository.GetWithLinkedEntitiesAsync();
        }

		/// <summary>
		/// Retrieves one skillalias by Id.
		/// </summary>
		public async Task<SkillAlias> GetSkillAliasByIdAsync(Guid id)
        {
			// #-#-# {F838CE2A-D0FB-4F8A-A826-0D653DEECB2B}
			// Before retrieval
			// #-#-#

            return await this.skillAliasRepository.GetWithLinkedEntitiesByIdAsync(id);
        }

		/// <summary>
		/// Creates a new skillalias record.
		/// </summary>
        public async Task<SkillAlias> CreateSkillAliasAsync(SkillAlias skillAlias)
        {
            // Validation
            if (skillAlias == null) { return null; }

			// Trimming strings
            if (!string.IsNullOrEmpty(skillAlias.Name))
                skillAlias.Name = skillAlias.Name.Trim();
            if (!string.IsNullOrEmpty(skillAlias.Description))
                skillAlias.Description = skillAlias.Description.Trim();

			// #-#-# {D4775AF3-4BFA-496A-AA82-001028A22DD6}
			// Before creation
			// #-#-#

			skillAlias = await this.skillAliasRepository.InsertAsync(skillAlias);

			// #-#-# {1972C619-D2F2-48FD-8474-3A69621B1F78}
			// After creation
			// #-#-#

            return skillAlias;
        }

		/// <summary>
		/// Updates an existing skillalias record by Id.
		/// </summary>
        public async Task<SkillAlias> UpdateSkillAliasAsync(SkillAlias skillAliasUpdate)
        {
            // Validation
            if (skillAliasUpdate == null) { return null; }

            // Retrieve existing
            SkillAlias skillAlias = await this.skillAliasRepository.GetByIdAsync(skillAliasUpdate.Id);
            if (skillAlias == null)
            {
                return null;
            }

			// Trimming strings
            if (!string.IsNullOrEmpty(skillAliasUpdate.Name))
                skillAliasUpdate.Name = skillAliasUpdate.Name.Trim();
            if (!string.IsNullOrEmpty(skillAliasUpdate.Description))
                skillAliasUpdate.Description = skillAliasUpdate.Description.Trim();

            // Mapping
            skillAlias.Name = skillAliasUpdate.Name;
            skillAlias.Description = skillAliasUpdate.Description;
            skillAlias.SkillId = skillAliasUpdate.SkillId;

			// #-#-# {B5914243-E57E-41AE-A7C8-553F2F93267B}
			// Before update
			// #-#-#

			skillAlias = await this.skillAliasRepository.UpdateAsync(skillAlias);

			// #-#-# {983B1B6C-14A7-4925-8571-D77447DF0ADA}
			// After update
			// #-#-#

            return skillAlias;
        }

		/// <summary>
		/// Deletes an existing skillalias record by Id.
		/// </summary>
        public async Task<SkillAlias> DeleteSkillAliasByIdAsync(Guid skillAliasId)
        {
            SkillAlias skillAlias = await this.skillAliasRepository.GetByIdAsync(skillAliasId);

            return await this.DeleteSkillAliasAsync(skillAlias);
        }

		/// <summary>
		/// Deletes an existing skillalias record.
		/// </summary>
        public async Task<SkillAlias> DeleteSkillAliasAsync(SkillAlias skillAlias)
        {
            // Validation
            if (skillAlias == null) { return null; }

			// #-#-# {FE1A99E0-482D-455B-A8C1-3C2C11FACA58}
			// Before deletion
			// #-#-#

            await this.skillAliasRepository.DeleteAsync(skillAlias);

			// #-#-# {F09857C0-44E7-4E6C-B3E6-883C0D28E1A6}
			// After deletion
			// #-#-#

            return skillAlias;
        }
    }
}
