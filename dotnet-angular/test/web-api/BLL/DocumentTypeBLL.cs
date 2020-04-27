using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RJM.API.DAL.Repositories;
using RJM.API.Models;

namespace RJM.API.BLL
{
	/// <summary>
	/// The business logic layer for DocumentTypes.
	/// </summary>
    public class DocumentTypeBLL
    {
        private readonly DocumentTypeRepository documentTypeRepository;

		/// <summary>
		/// The constructor of the DocumentType business logic layer.
		/// </summary>
        public DocumentTypeBLL(
			DocumentTypeRepository documentTypeRepository
		)
        {
            this.documentTypeRepository = documentTypeRepository;
        }

		/// <summary>
		/// Retrieves all documenttypes.
		/// </summary>
		public async Task<IEnumerable<DocumentType>> GetAllDocumentTypesAsync()
        {
			// #-#-# {83B8AA9F-713A-42FB-ADE1-8A4AA43886C8}
			// Before retrieval
			// #-#-#

            return await this.documentTypeRepository.GetWithLinkedEntitiesAsync();
        }

		/// <summary>
		/// Retrieves one documenttype by Id.
		/// </summary>
		public async Task<DocumentType> GetDocumentTypeByIdAsync(Guid id)
        {
			// #-#-# {F838CE2A-D0FB-4F8A-A826-0D653DEECB2B}
			// Before retrieval
			// #-#-#

            return await this.documentTypeRepository.GetWithLinkedEntitiesByIdAsync(id);
        }

		/// <summary>
		/// Creates a new documenttype record.
		/// </summary>
        public async Task<DocumentType> CreateDocumentTypeAsync(DocumentType documentType)
        {
            // Validation
            if (documentType == null) { return null; }

			// Trimming strings
            if (!string.IsNullOrEmpty(documentType.Name))
                documentType.Name = documentType.Name.Trim();
            if (!string.IsNullOrEmpty(documentType.DisplayName))
                documentType.DisplayName = documentType.DisplayName.Trim();

			// #-#-# {D4775AF3-4BFA-496A-AA82-001028A22DD6}
			// Before creation
			// #-#-#

			documentType = await this.documentTypeRepository.InsertAsync(documentType);

			// #-#-# {1972C619-D2F2-48FD-8474-3A69621B1F78}
			// After creation
			// #-#-#

            return documentType;
        }

		/// <summary>
		/// Updates an existing documenttype record by Id.
		/// </summary>
        public async Task<DocumentType> UpdateDocumentTypeAsync(DocumentType documentTypeUpdate)
        {
            // Validation
            if (documentTypeUpdate == null) { return null; }

            // Retrieve existing
            DocumentType documentType = await this.documentTypeRepository.GetByIdAsync(documentTypeUpdate.Id);
            if (documentType == null)
            {
                return null;
            }

			// Trimming strings
            if (!string.IsNullOrEmpty(documentTypeUpdate.Name))
                documentTypeUpdate.Name = documentTypeUpdate.Name.Trim();
            if (!string.IsNullOrEmpty(documentTypeUpdate.DisplayName))
                documentTypeUpdate.DisplayName = documentTypeUpdate.DisplayName.Trim();

            // Mapping
            documentType.Name = documentTypeUpdate.Name;
            documentType.DisplayName = documentTypeUpdate.DisplayName;

			// #-#-# {B5914243-E57E-41AE-A7C8-553F2F93267B}
			// Before update
			// #-#-#

			documentType = await this.documentTypeRepository.UpdateAsync(documentType);

			// #-#-# {983B1B6C-14A7-4925-8571-D77447DF0ADA}
			// After update
			// #-#-#

            return documentType;
        }

		/// <summary>
		/// Deletes an existing documenttype record by Id.
		/// </summary>
        public async Task<DocumentType> DeleteDocumentTypeByIdAsync(Guid documentTypeId)
        {
            DocumentType documentType = await this.documentTypeRepository.GetByIdAsync(documentTypeId);

            return await this.DeleteDocumentTypeAsync(documentType);
        }

		/// <summary>
		/// Deletes an existing documenttype record.
		/// </summary>
        public async Task<DocumentType> DeleteDocumentTypeAsync(DocumentType documentType)
        {
            // Validation
            if (documentType == null) { return null; }

			// #-#-# {FE1A99E0-482D-455B-A8C1-3C2C11FACA58}
			// Before deletion
			// #-#-#

            await this.documentTypeRepository.DeleteAsync(documentType);

			// #-#-# {F09857C0-44E7-4E6C-B3E6-883C0D28E1A6}
			// After deletion
			// #-#-#

            return documentType;
        }
    }
}
