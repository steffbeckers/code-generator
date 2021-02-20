using CodeGenOutput.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CodeGenOutput.API.DAL.Repositories
{
    public static class ProjectRepositoryExtensions
    {
        // Additional repository functions here

        public static async Task<Project> GetByIdAsync(
            this IRepository<Project> repository,
            Guid id,
            string include = ""
        )
        {
            IQueryable<Project> query = repository.GetDbSet();

            foreach (string property in include.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(property);
            }

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
