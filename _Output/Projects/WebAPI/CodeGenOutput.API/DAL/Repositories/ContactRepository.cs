using CodeGenOutput.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
            string include = ""
        )
        {
            IQueryable<Contact> query = repository.GetDbSet();

            foreach (string property in include.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(property);
            }

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public static async Task<IEnumerable<Contact>> SearchContactAsync(
            this IRepository<Contact> repository,
            string term
        )
        {
            return await repository.GetAsync(x => x.FirstName.Contains(term) || x.LastName.Contains(term));
        }
    }
}
