using CodeGenOutput.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeGenOutput.API.DAL.Repositories
{
    public static class ContactRepositoryExtensions
    {
        // Additional repository functions here

        public static async Task<Contact> GetByCodeAsync(
            this IRepository<Contact> repository,
            string code
        )
        {
            return (await repository.GetAsync(0, 1, x => x.Code == code)).FirstOrDefault();
        }

        public static async Task DeleteAsync(
            this IRepository<Contact> repository,
            string code
        )
        {
            Contact contact = await repository.GetByCodeAsync(code);
            if (contact != null)
            {
                await repository.DeleteAsync(contact);
            }
        }
        
        // public static async Task<IEnumerable<Contact>> SearchContact(
        //     this IRepository<Contact> repository,
        //     string term
        // )
        // {
        //     return await repository.GetAsync(0, 20, x => x.Name.Contains(term));
        // }
    }
}
