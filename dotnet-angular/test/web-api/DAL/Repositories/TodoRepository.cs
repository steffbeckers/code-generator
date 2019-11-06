using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.API.Models;

namespace Test.API.DAL.Repositories
{
	/// <summary>
	/// The repository for Todos in the data access layer.
	/// </summary>
    public class TodoRepository : Repository<Todo>
    {
        private new readonly TestContext context;

		/// <summary>
		/// The constructor of the Todo repository.
		/// </summary>
        public TodoRepository(TestContext context) : base(context)
        {
            this.context = context;
        }

        // Additional functionality and overrides

		public async Task<IEnumerable<Todo>> GetWithLinkedEntitiesAsync()
        {
            return await this.context.Todos
                .ToListAsync();
        }

		public async Task<Todo> GetWithLinkedEntitiesByIdAsync(Guid id)
        {
            return await this.context.Todos
                .SingleOrDefaultAsync(x => x.Id == id);
        }

    }
}
