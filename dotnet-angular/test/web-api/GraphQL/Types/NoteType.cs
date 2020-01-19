using GraphQL.Types;
using System;
using Test.API.DAL.Repositories;
using Test.API.Models;

namespace Test.API.GraphQL.Types
{
    public class NoteType : ObjectGraphType<Note>
    {
        public NoteType(
			NoteRepository noteRepository,
            AccountRepository accountRepository,
			AccountNoteRepository accountNoteRepository
        )
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Title);
            Field(x => x.Body, nullable: true);

            Field<ListGraphType<AccountType>>(
                "accounts",
                resolve: context => noteRepository.GetAccountsOfNoteById(context.Source.Id)
            );

        }
    }
}
