using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RJM.API.DAL.Repositories;
using RJM.API.Models;

namespace RJM.API.BLL
{
	/// <summary>
	/// The business logic layer for Documents.
	/// </summary>
    public class DocumentBLL
    {
        private readonly DocumentRepository documentRepository;
        private readonly ResumeRepository resumeRepository;
        private readonly DocumentResumeRepository documentResumeRepository;

		/// <summary>
		/// The constructor of the Document business logic layer.
		/// </summary>
        public DocumentBLL(
			DocumentRepository documentRepository,
            ResumeRepository resumeRepository,
			DocumentResumeRepository documentResumeRepository
		)
        {
            this.documentRepository = documentRepository;
            this.resumeRepository = resumeRepository;
			this.documentResumeRepository = documentResumeRepository;
        }

		/// <summary>
		/// Retrieves all documents.
		/// </summary>
		public async Task<IEnumerable<Document>> GetAllDocumentsAsync()
        {
            return await this.documentRepository.GetWithLinkedEntitiesAsync();
        }

		/// <summary>
		/// Retrieves one document by Id.
		/// </summary>
		public async Task<Document> GetDocumentByIdAsync(Guid id)
        {
			// #-#-# {F838CE2A-D0FB-4F8A-A826-0D653DEECB2B}
			// Before retrieval
			// #-#-#

            return await this.documentRepository.GetWithLinkedEntitiesByIdAsync(id);
        }

		/// <summary>
		/// Creates a new document record.
		/// </summary>
        public async Task<Document> CreateDocumentAsync(Document document)
        {
            // Validation
            if (document == null) { return null; }

			// Trimming strings
            if (!string.IsNullOrEmpty(document.Name))
                document.Name = document.Name.Trim();
            if (!string.IsNullOrEmpty(document.DisplayName))
                document.DisplayName = document.DisplayName.Trim();
            if (!string.IsNullOrEmpty(document.Description))
                document.Description = document.Description.Trim();
            if (!string.IsNullOrEmpty(document.Path))
                document.Path = document.Path.Trim();
            if (!string.IsNullOrEmpty(document.URL))
                document.URL = document.URL.Trim();
            if (!string.IsNullOrEmpty(document.MimeType))
                document.MimeType = document.MimeType.Trim();

			// #-#-# {D4775AF3-4BFA-496A-AA82-001028A22DD6}
			// Before creation
			// #-#-#

			document = await this.documentRepository.InsertAsync(document);

			// #-#-# {1972C619-D2F2-48FD-8474-3A69621B1F78}
			// After creation
			// #-#-#

            return document;
        }

		/// <summary>
		/// Updates an existing document record by Id.
		/// </summary>
        public async Task<Document> UpdateDocumentAsync(Document documentUpdate)
        {
            // Validation
            if (documentUpdate == null) { return null; }

            // Retrieve existing
            Document document = await this.documentRepository.GetByIdAsync(documentUpdate.Id);
            if (document == null)
            {
                return null;
            }

			// Trimming strings
            if (!string.IsNullOrEmpty(documentUpdate.Name))
                documentUpdate.Name = documentUpdate.Name.Trim();
            if (!string.IsNullOrEmpty(documentUpdate.DisplayName))
                documentUpdate.DisplayName = documentUpdate.DisplayName.Trim();
            if (!string.IsNullOrEmpty(documentUpdate.Description))
                documentUpdate.Description = documentUpdate.Description.Trim();
            if (!string.IsNullOrEmpty(documentUpdate.Path))
                documentUpdate.Path = documentUpdate.Path.Trim();
            if (!string.IsNullOrEmpty(documentUpdate.URL))
                documentUpdate.URL = documentUpdate.URL.Trim();
            if (!string.IsNullOrEmpty(documentUpdate.MimeType))
                documentUpdate.MimeType = documentUpdate.MimeType.Trim();

            // Mapping
            document.Name = documentUpdate.Name;
            document.DisplayName = documentUpdate.DisplayName;
            document.Description = documentUpdate.Description;
            document.Path = documentUpdate.Path;
            document.URL = documentUpdate.URL;
            document.SizeInBytes = documentUpdate.SizeInBytes;
            document.FileLastModifiedOn = documentUpdate.FileLastModifiedOn;
            document.MimeType = documentUpdate.MimeType;

			// #-#-# {B5914243-E57E-41AE-A7C8-553F2F93267B}
			// Before update
			// #-#-#

			document = await this.documentRepository.UpdateAsync(document);

			// #-#-# {983B1B6C-14A7-4925-8571-D77447DF0ADA}
			// After update
			// #-#-#

            return document;
        }

        public async Task<Document> LinkResumeToDocumentAsync(DocumentResume documentResume)
        {
            // Validation
            if (documentResume == null) { return null; }

            // Check if document exists
            Document document = await this.documentRepository.GetByIdAsync(documentResume.DocumentId);
            if (document == null)
            {
                return null;
            }

            // Check if resume exists
            Resume resume = await this.resumeRepository.GetByIdAsync(documentResume.ResumeId);
            if (resume == null)
            {
                return null;
            }

            // Retrieve existing link
            DocumentResume documentResumeLink = this.documentResumeRepository.GetByDocumentAndResumeId(documentResume.DocumentId, documentResume.ResumeId);

            if (documentResumeLink == null)
            {
                await this.documentResumeRepository.InsertAsync(documentResume);
            }
            else
            {
                // Mapping of fields on many-to-many

                await this.documentResumeRepository.UpdateAsync(documentResumeLink);
            }

            return await this.GetDocumentByIdAsync(documentResume.DocumentId);
        }

        public async Task<Document> UnlinkResumeFromDocumentAsync(DocumentResume documentResume)
        {
            // Validation
            if (documentResume == null) { return null; }

            // Retrieve existing link
            DocumentResume documentResumeLink = this.documentResumeRepository.GetByDocumentAndResumeId(documentResume.DocumentId, documentResume.ResumeId);
		
            if (documentResumeLink != null)
            {
                await this.documentResumeRepository.DeleteAsync(documentResumeLink);
            }

            return await this.GetDocumentByIdAsync(documentResume.DocumentId);
        }

		/// <summary>
		/// Deletes an existing document record by Id.
		/// </summary>
        public async Task<Document> DeleteDocumentByIdAsync(Guid documentId)
        {
            Document document = await this.documentRepository.GetByIdAsync(documentId);

            return await this.DeleteDocumentAsync(document);
        }

		/// <summary>
		/// Deletes an existing document record.
		/// </summary>
        public async Task<Document> DeleteDocumentAsync(Document document)
        {
            // Validation
            if (document == null) { return null; }

			// #-#-# {FE1A99E0-482D-455B-A8C1-3C2C11FACA58}
			// Before deletion
			// #-#-#

            await this.documentRepository.DeleteAsync(document);

			// #-#-# {F09857C0-44E7-4E6C-B3E6-883C0D28E1A6}
			// After deletion
			// #-#-#

            return document;
        }
    }
}
