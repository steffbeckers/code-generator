using GraphQL.Types;
using System;
using Test.API.DAL.Repositories;
using Test.API.Models;

namespace Test.API.GraphQL.Types
{
    public class ContactType : ObjectGraphType<Contact>
    {
        public ContactType(
            AccountRepository accountRepository,
			ContactRepository contactRepository
        )
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.FirstName);
            Field(x => x.LastName);
            Field(x => x.Website, nullable: true);
            Field(x => x.Telephone, nullable: true);
            Field(x => x.Email, nullable: true);

            Field<AccountType>(
                "account",
                resolve: context =>
                {
                    if (context.Source.AccountId != null)
                        return accountRepository.GetById((Guid)context.Source.AccountId);
                    return null;
                }
            );
        }
    }
}
