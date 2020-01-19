using GraphQL.Types;
using Test.API.DAL.Repositories;
using Test.API.GraphQL.Types;
using System;
using System.Linq;

namespace Test.API.GraphQL
{
    public class TestQuery : ObjectGraphType
    {
        public TestQuery(
			AccountRepository accountRepository,
			ContactRepository contactRepository,
			AddressRepository addressRepository,
			NoteRepository noteRepository,
			TodoRepository todoRepository
        )
        {
			// Accounts
            Field<ListGraphType<AccountType>>(
                "accounts",
                resolve: context => accountRepository.Get(null, x => x.OrderBy(x => x.Name))
            );
            Field<AccountType>(
                "account",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: context => accountRepository.GetById(context.GetArgument<Guid>("id"))
            );

			// Contacts
            Field<ListGraphType<ContactType>>(
                "contacts",
                resolve: context => contactRepository.Get(null, x => x.OrderByDescending(x => x.ModifiedOn))
            );
            Field<ContactType>(
                "contact",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: context => contactRepository.GetById(context.GetArgument<Guid>("id"))
            );

			// Addresses
            Field<ListGraphType<AddressType>>(
                "addresses",
                resolve: context => addressRepository.Get(null, x => x.OrderByDescending(x => x.ModifiedOn))
            );
            Field<AddressType>(
                "address",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: context => addressRepository.GetById(context.GetArgument<Guid>("id"))
            );

			// Notes
            Field<ListGraphType<NoteType>>(
                "notes",
                resolve: context => noteRepository.Get(null, x => x.OrderByDescending(x => x.ModifiedOn))
            );
            Field<NoteType>(
                "note",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: context => noteRepository.GetById(context.GetArgument<Guid>("id"))
            );

			// Todos
            Field<ListGraphType<TodoType>>(
                "todos",
                resolve: context => todoRepository.Get(null, x => x.OrderByDescending(x => x.ModifiedOn))
            );
            Field<TodoType>(
                "todo",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: context => todoRepository.GetById(context.GetArgument<Guid>("id"))
            );

        }
    }
}
