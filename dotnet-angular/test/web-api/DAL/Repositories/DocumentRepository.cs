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
	/// The repository for Documents in the data access layer.
	/// </summary>
    public class DocumentRepository : Repository<Document>
    {
        private new readonly RJMContext context;

		/// <summary>
		/// The constructor of the Document repository.
		/// </summary>
        public DocumentRepository(RJMContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides

		public async Task<IEnumerable<Document>> GetWithLinkedEntitiesAsync()
        {
            return await this.context.Documents
                .Include(x => x.CreatedByUser)
                .Include(x => x.ModifiedByUser)
                .ToListAsync();
        }

		public async Task<Document> GetWithLinkedEntitiesByIdAsync(Guid id)
        {
            return await this.context.Documents
                .Include(x => x.CreatedByUser)
                .Include(x => x.ModifiedByUser)
                .SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
