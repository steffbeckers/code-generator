using GraphQL.Types;
using System;
using Test.API.DAL.Repositories;
using Test.API.Models;

namespace Test.API.GraphQL.Types
{
    public class ContactType : ObjectGraphType<Contact>
    {
        public ContactType(
			ContactRepository contactRepository
        )
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.LastName);
            Field(x => x.AccountId);
            Field(x => x.Id);
            Field(x => x.FirstName, nullable: true);
            Field(x => x.JobTitle, nullable: true);
            Field(x => x.Email, nullable: true);
            Field(x => x.Telephone, nullable: true);
            Field(x => x.MobilePhone, nullable: true);
            Field(x => x.Gender, nullable: true);

            Field(x => x.CreatedByUserId, type: typeof(IdGraphType));
            // TODO: Field(x => x.CreatedByUser, type: typeof(UserType));
            Field(x => x.ModifiedByUserId, type: typeof(IdGraphType));
            // TODO: Field(x => x.ModifiedByUser, type: typeof(UserType));
        }
    }
}
