using GraphQL.Types;
using System;
using Test.API.DAL.Repositories;
using Test.API.Models;

namespace Test.API.GraphQL.Types
{
    public class CountryType : ObjectGraphType<Country>
    {
        public CountryType(
			CountryRepository countryRepository
        )
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Id);
            Field(x => x.Name);

            Field(x => x.CreatedByUserId, type: typeof(IdGraphType));
            // TODO: Field(x => x.CreatedByUser, type: typeof(UserType));
            Field(x => x.ModifiedByUserId, type: typeof(IdGraphType));
            // TODO: Field(x => x.ModifiedByUser, type: typeof(UserType));
        }
    }
}
