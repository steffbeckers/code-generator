using GraphQL.Types;
using System;
using Test.API.DAL.Repositories;
using Test.API.Models;

namespace Test.API.GraphQL.Types
{
    public class AddressType : ObjectGraphType<Address>
    {
        public AddressType(
			AddressRepository addressRepository
        )
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Street);
            Field(x => x.Number);
            Field(x => x.PostalCode);
            Field(x => x.City);
            Field(x => x.Primary, nullable: true);

            Field(x => x.CreatedByUserId, type: typeof(IdGraphType));
            // TODO: Field(x => x.CreatedByUser, type: typeof(UserType));
            Field(x => x.ModifiedByUserId, type: typeof(IdGraphType));
            // TODO: Field(x => x.ModifiedByUser, type: typeof(UserType));
        }
    }
}
