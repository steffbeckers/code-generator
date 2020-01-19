using GraphQL.Types;
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
            Field(x => x.FirstName);
            Field(x => x.LastName);
            Field(x => x.Website, nullable: true);
            Field(x => x.Telephone, nullable: true);
            Field(x => x.Email, nullable: true);

	        /// <summary>
            /// The related foreign key AccountId for Account of Contact.
            /// </summary>
		    //public Guid? AccountId { get; set; }

		    /// <summary>
            /// The related Account of Contact.
            /// </summary>
		    //public Account Account { get; set; }

        }
    }
}
