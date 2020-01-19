using GraphQL.Types;
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

	        /// <summary>
            /// The related foreign key AccountId for Account of Address.
            /// </summary>
		    //public Guid? AccountId { get; set; }

		    /// <summary>
            /// The related Account of Address.
            /// </summary>
		    //public Account Account { get; set; }

        }
    }
}
