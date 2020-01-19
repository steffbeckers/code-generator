using GraphQL.Types;
using Test.API.DAL.Repositories;
using Test.API.Models;

namespace Test.API.GraphQL.Types
{
    public class AccountType : ObjectGraphType<Account>
    {
        public AccountType(
            AddressRepository addressRepository,
            ContactRepository contactRepository,
			AccountRepository accountRepository,
            NoteRepository noteRepository,
			AccountNoteRepository accountNoteRepository
        )
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Name);
            Field(x => x.Website, nullable: true);
            Field(x => x.Telephone, nullable: true);
            Field(x => x.Email, nullable: true);

            Field<ListGraphType<AddressType>>(
                "addresses",
                resolve: context => addressRepository.GetByAccountId(context.Source.Id)
            );

            Field<ListGraphType<ContactType>>(
                "contacts",
                resolve: context => contactRepository.GetByAccountId(context.Source.Id)
            );

            Field<ListGraphType<NoteType>>(
                "notes",
                resolve: context => accountRepository.GetNotesOfAccountById(context.Source.Id)
            );

        }
    }
}
