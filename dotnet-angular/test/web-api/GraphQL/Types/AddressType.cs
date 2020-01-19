using GraphQL.Types;
using System;
using Test.API.DAL.Repositories;
using Test.API.Models;

namespace Test.API.GraphQL.Types
{
    public class AddressType : ObjectGraphType<Address>
    {
        public AddressType(
            AccountRepository accountRepository,
			AddressRepository addressRepository
        )
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Street);
            Field(x => x.Number);
            Field(x => x.PostalCode);
            Field(x => x.City);
            Field(x => x.Primary, nullable: true);

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
