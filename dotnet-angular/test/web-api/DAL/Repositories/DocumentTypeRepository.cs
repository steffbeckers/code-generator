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
	/// The repository for DocumentTypes in the data access layer.
	/// </summary>
    public class DocumentTypeRepository : Repository<DocumentType>
    {
        private new readonly RJMContext context;

		/// <summary>
		/// The constructor of the DocumentType repository.
		/// </summary>
        public DocumentTypeRepository(RJMContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides

		public async Task<IEnumerable<DocumentType>> GetWithLinkedEntitiesAsync()
        {
            return await this.context.DocumentTypes
                .Include(x => x.Documents)
                .Include(x => x.CreatedByUser)
                .Include(x => x.ModifiedByUser)
                .ToListAsync();
        }

		public async Task<DocumentType> GetWithLinkedEntitiesByIdAsync(Guid id)
        {
            return await this.context.DocumentTypes
                .Include(x => x.Documents)
                .Include(x => x.CreatedByUser)
                .Include(x => x.ModifiedByUser)
                .SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
