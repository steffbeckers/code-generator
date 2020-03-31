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
	/// The repository for DocumentResumes in the data access layer.
	/// </summary>
    public class DocumentResumeRepository : Repository<DocumentResume>
    {
        private new readonly RJMContext context;

		/// <summary>
		/// The constructor of the DocumentResume repository.
		/// </summary>
        public DocumentResumeRepository(RJMContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides

        public DocumentResume GetByDocumentAndResumeId(Guid documentId, Guid resumeId)
        {
            return this.context.DocumentResume
                .Where(x => x.DocumentId == documentId && x.ResumeId == resumeId)
                .SingleOrDefault();
        }

		public DocumentResume GetByResumeAndDocumentId(Guid resumeId, Guid documentId)
        {
            return this.context.DocumentResume
                .Where(x => x.ResumeId == resumeId && x.DocumentId == documentId)
                .SingleOrDefault();
        }
    }
}
