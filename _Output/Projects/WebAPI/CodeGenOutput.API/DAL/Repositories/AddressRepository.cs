using CodeGenOutput.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeGenOutput.API.DAL.Repositories
{
    public static class AddressRepositoryExtensions
    {
        // Additional repository functions here

        public static async Task<Address> GetByIdAsync(
            this IRepository<Address> repository,
            Guid id,
            string include = ""
        )
        {
            IQueryable<Address> query = repository.GetDbSet();

            foreach (string property in include.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(property);
            }

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
