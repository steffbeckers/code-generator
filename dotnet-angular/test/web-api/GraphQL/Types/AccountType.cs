using GraphQL.Types;
using System;
using Test.API.DAL.Repositories;
using Test.API.Models;

namespace Test.API.GraphQL.Types
{
    public class AccountType : ObjectGraphType<Account>
    {
        public AccountType(
			AccountRepository accountRepository
        )
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Name);
            Field(x => x.Website, nullable: true);
            Field(x => x.Telephone, nullable: true);
            Field(x => x.Email, nullable: true);

            Field<AccountType>(
                "parentAccount",
                resolve: context =>
                {
                    if (context.Source.ParentAccountId != null)
                        return accountRepository.GetById((Guid)context.Source.ParentAccountId);
                    return null;
                }
            );

            //// Async test
            //FieldAsync<AccountType>(
            //    "parentAccount",
            //    resolve: async context =>
            //    {
            //        if (context.Source.ParentAccountId != null) {
            //            return await context.TryAsyncResolve(
            //                async c => await accountRepository.GetByIdAsync((Guid)context.Source.ParentAccountId)
            //            );
            //        }
            //        
            //        return null;
            //    }
            //);

        }
    }
}
