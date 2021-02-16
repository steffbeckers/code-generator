using CodeGenOutput.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CodeGenOutput.API.DAL.Repositories
{
    public static class AccountContactRepositoryExtensions
    {
        // Additional repository functions here

        public static async Task<AccountContact> GetByIdAsync(
            this IRepository<AccountContact> repository,
            Guid id,
            string include = ""
        )
        {
            IQueryable<AccountContact> query = repository.GetDbSet();

            foreach (string property in include.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(property);
            }

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
