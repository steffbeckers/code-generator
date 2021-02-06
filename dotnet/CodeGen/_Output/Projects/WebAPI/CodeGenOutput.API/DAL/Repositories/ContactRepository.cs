using CodeGenOutput.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeGenOutput.API.DAL.Repositories
{
    public static class ContactRepositoryExtensions
    {
        public static async Task<IEnumerable<Contact>> SearchContact(
            this IRepository<Contact> repository,
            string term
        )
        {
            return await repository.GetAsync(x => x.Name.Contains(term));
        }
    }
}
