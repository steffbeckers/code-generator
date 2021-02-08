using CodeGenOutput.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

// TODO: namespace CodeGenOutput.API.DAL.Repositories
namespace CodeGenOutput.API.DAL
{
    public static class AccountRepositoryExtensions
    {
        public static async Task<IEnumerable<Account>> SearchAccount(
            this IRepository<Account> repository,
            string term
        )
        {
            return await repository.GetAsync(0, 20, x => x.Name.Contains(term));
        }
    }
}