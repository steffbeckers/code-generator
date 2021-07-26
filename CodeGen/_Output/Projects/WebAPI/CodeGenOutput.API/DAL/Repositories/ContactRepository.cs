using CodeGenOutput.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CodeGenOutput.API.DAL.Repositories
{
    public static class ContactRepositoryExtensions
    {
        // Additional repository functions here

        public static async Task<Contact> GetByIdAsync(
            this IRepository<Contact> repository,
            Guid id,
            string include
        )
        {
            IQueryable<Contact> query = repository.GetDbSet();

            if (!string.IsNullOrEmpty(include)) {
                foreach (string property in include.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
