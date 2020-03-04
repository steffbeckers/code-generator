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
            Field(x => x.Email, nullable: true);
            Field(x => x.Telephone, nullable: true);
            Field(x => x.Fax, nullable: true);
            Field(x => x.Website, nullable: true);
            Field(x => x.VATNumber, nullable: true);
            Field(x => x.Description, nullable: true);
            Field(x => x.AddressId, nullable: true);
            Field(x => x.ParentAccountId, nullable: true);
            Field(x => x.BillingAccountId, nullable: true);
            Field(x => x.RelationTypeId, nullable: true);
            Field(x => x.PrimaryContactId, nullable: true);

            Field(x => x.CreatedByUserId, type: typeof(IdGraphType));
            // TODO: Field(x => x.CreatedByUser, type: typeof(UserType));
            Field(x => x.ModifiedByUserId, type: typeof(IdGraphType));
            // TODO: Field(x => x.ModifiedByUser, type: typeof(UserType));
        }
    }
}
