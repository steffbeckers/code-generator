using CodeGenOutput.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeGenOutput.API.DAL
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
