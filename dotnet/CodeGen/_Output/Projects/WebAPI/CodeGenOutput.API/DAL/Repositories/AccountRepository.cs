using CodeGenOutput.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeGenOutput.API.DAL.Repositories
{
    public static class AccountRepositoryExtensions
    {
        public static async Task<IEnumerable<Account>> SearchAccount(
            this IRepository<Account> repository,
            string term
        )
        {
            return await repository.GetAsync(x => x.Name.Contains(term));
        }
    }
}
