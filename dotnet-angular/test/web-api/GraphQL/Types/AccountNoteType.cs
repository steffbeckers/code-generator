using GraphQL.Types;
using System;
using Test.API.DAL.Repositories;
using Test.API.Models;

namespace Test.API.GraphQL.Types
{
    public class AccountNoteType : ObjectGraphType<AccountNote>
    {
        public AccountNoteType(
            AccountRepository accountRepository,
            NoteRepository noteRepository,
			AccountNoteRepository accountNoteRepository
        )
        {
            Field(x => x.Id, type: typeof(IdGraphType));

            Field<AccountType>(
                "account",
                resolve: context =>
                {
                    if (context.Source.AccountId != null)
                        return accountRepository.GetById((Guid)context.Source.AccountId);
                    return null;
                }
            );
            Field<NoteType>(
                "note",
                resolve: context =>
                {
                    if (context.Source.NoteId != null)
                        return noteRepository.GetById((Guid)context.Source.NoteId);
                    return null;
                }
            );
        }
    }
}
