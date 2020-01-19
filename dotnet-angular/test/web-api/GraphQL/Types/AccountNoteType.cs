using GraphQL.Types;
using Test.API.DAL.Repositories;
using Test.API.Models;

namespace Test.API.GraphQL.Types
{
    public class AccountNoteType : ObjectGraphType<AccountNote>
    {
        public AccountNoteType(
			AccountNoteRepository accountNoteRepository
        )
        {
            Field(x => x.Id, type: typeof(IdGraphType));

	        /// <summary>
            /// The related foreign key AccountId for Account of AccountNote.
            /// </summary>
		    //public Guid AccountId { get; set; }

		    /// <summary>
            /// The related Account of AccountNote.
            /// </summary>
		    //public Account Account { get; set; }

	        /// <summary>
            /// The related foreign key NoteId for Note of AccountNote.
            /// </summary>
		    //public Guid NoteId { get; set; }

		    /// <summary>
            /// The related Note of AccountNote.
            /// </summary>
		    //public Note Note { get; set; }

        }
    }
}
