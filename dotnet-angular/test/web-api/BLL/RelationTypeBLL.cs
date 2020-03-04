using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.API.DAL.Repositories;
using Test.API.Models;

namespace Test.API.BLL
{
	/// <summary>
	/// The business logic layer for RelationTypes.
	/// </summary>
    public class RelationTypeBLL
    {
        private readonly RelationTypeRepository relationTypeRepository;

		/// <summary>
		/// The constructor of the RelationType business logic layer.
		/// </summary>
        public RelationTypeBLL(
			RelationTypeRepository relationTypeRepository
		)
        {
            this.relationTypeRepository = relationTypeRepository;
        }

		/// <summary>
		/// Retrieves all relationtypes.
		/// </summary>
		public async Task<IEnumerable<RelationType>> GetAllRelationTypesAsync()
        {
			// #-#-# {83B8AA9F-713A-42FB-ADE1-8A4AA43886C8}
			// Before retrieval
			// #-#-#

            return await this.relationTypeRepository.GetWithLinkedEntitiesAsync();
        }

		/// <summary>
		/// Retrieves one relationtype by Id.
		/// </summary>
		public async Task<RelationType> GetRelationTypeByIdAsync(Guid id)
        {
			// #-#-# {F838CE2A-D0FB-4F8A-A826-0D653DEECB2B}
			// Before retrieval
			// #-#-#

            return await this.relationTypeRepository.GetWithLinkedEntitiesByIdAsync(id);
        }

		/// <summary>
		/// Creates a new relationtype record.
		/// </summary>
        public async Task<RelationType> CreateRelationTypeAsync(RelationType relationType)
        {
            // Validation
            if (relationType == null) { return null; }

			// Trimming strings
            if (!string.IsNullOrEmpty(relationType.Name))
                relationType.Name = relationType.Name.Trim();

			// #-#-# {D4775AF3-4BFA-496A-AA82-001028A22DD6}
			// Before creation
			// #-#-#

			relationType = await this.relationTypeRepository.InsertAsync(relationType);

			// #-#-# {1972C619-D2F2-48FD-8474-3A69621B1F78}
			// After creation
			// #-#-#

            return relationType;
        }

		/// <summary>
		/// Updates an existing relationtype record by Id.
		/// </summary>
        public async Task<RelationType> UpdateRelationTypeAsync(RelationType relationTypeUpdate)
        {
            // Validation
            if (relationTypeUpdate == null) { return null; }

            // Retrieve existing
            RelationType relationType = await this.relationTypeRepository.GetByIdAsync(relationTypeUpdate.Id);
            if (relationType == null)
            {
                return null;
            }

			// Trimming strings
            if (!string.IsNullOrEmpty(relationTypeUpdate.Name))
                relationTypeUpdate.Name = relationTypeUpdate.Name.Trim();

            // Mapping
            relationType.Id = relationTypeUpdate.Id;
            relationType.Name = relationTypeUpdate.Name;

			// #-#-# {B5914243-E57E-41AE-A7C8-553F2F93267B}
			// Before update
			// #-#-#

			relationType = await this.relationTypeRepository.UpdateAsync(relationType);

			// #-#-# {983B1B6C-14A7-4925-8571-D77447DF0ADA}
			// After update
			// #-#-#

            return relationType;
        }

		/// <summary>
		/// Deletes an existing relationtype record by Id.
		/// </summary>
        public async Task<RelationType> DeleteRelationTypeByIdAsync(Guid relationTypeId)
        {
            RelationType relationType = await this.relationTypeRepository.GetByIdAsync(relationTypeId);

            return await this.DeleteRelationTypeAsync(relationType);
        }

		/// <summary>
		/// Deletes an existing relationtype record.
		/// </summary>
        public async Task<RelationType> DeleteRelationTypeAsync(RelationType relationType)
        {
            // Validation
            if (relationType == null) { return null; }

			// #-#-# {FE1A99E0-482D-455B-A8C1-3C2C11FACA58}
			// Before deletion
			// #-#-#

            await this.relationTypeRepository.DeleteAsync(relationType);

			// #-#-# {F09857C0-44E7-4E6C-B3E6-883C0D28E1A6}
			// After deletion
			// #-#-#

            return relationType;
        }
    }
}
