using CodeGenOutput.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeGenOutput.API.DAL.Repositories
{
    public static class ContactRepositoryExtensions
    {
        // Additional repository functions here

        public static async Task<IEnumerable<Contact>> SearchContactAsync(
            this IRepository<Contact> repository,
            string term
        )
        {
            return await repository.GetAsync(x =>
                x.FirstName.Contains(term) ||
                x.LastName.Contains(term)
            );
        }
    }
}
