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
            Field(x => x.Id);
            Field(x => x.Street, nullable: true);
            Field(x => x.Number, nullable: true);
            Field(x => x.City, nullable: true);
            Field(x => x.State, nullable: true);
            Field(x => x.PostalCode, nullable: true);
            Field(x => x.Latitude, nullable: true);
            Field(x => x.Longitude, nullable: true);
            Field(x => x.CountryId, nullable: true);

            Field(x => x.CreatedByUserId, type: typeof(IdGraphType));
            // TODO: Field(x => x.CreatedByUser, type: typeof(UserType));
            Field(x => x.ModifiedByUserId, type: typeof(IdGraphType));
            // TODO: Field(x => x.ModifiedByUser, type: typeof(UserType));
        }
    }
}
