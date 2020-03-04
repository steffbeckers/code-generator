using GraphQL.Types;
using GraphQL.Server.Authorization.AspNetCore;
using Test.API.DAL.Repositories;
using Test.API.GraphQL.Types;
using System;
using System.Linq;

namespace Test.API.GraphQL
{
    public class TestQuery : ObjectGraphType
    {
        public TestQuery(
			CountryRepository countryRepository,
			RelationTypeRepository relationTypeRepository,
			AddressRepository addressRepository,
			ContactRepository contactRepository,
			AccountRepository accountRepository,
			WorkOrderRepository workOrderRepository
        )
        {
            this.AuthorizeWith("Authorized");

			// Countries
            
            Field<ListGraphType<CountryType>>(
                "countries",
                resolve: context => countryRepository.Get(null, x => x.OrderByDescending(x => x.ModifiedOn))
            );

            //// Async test
            //FieldAsync<ListGraphType<CountryType>>(
            //    "countries",
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await countryRepository.GetAsync(null, x => x.OrderByDescending(x => x.ModifiedOn))
            //        );
            //    }
            //);

            Field<CountryType>(
                "country",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: context => countryRepository.GetById(context.GetArgument<Guid>("id"))
            );

            //// Async test
            //FieldAsync<CountryType>(
            //    "country",
            //    arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await countryRepository.GetByIdAsync(context.GetArgument<Guid>("id"))
            //        );
            //    }
            //);

			// RelationTypes
            
            Field<ListGraphType<RelationTypeType>>(
                "relationTypes",
                resolve: context => relationTypeRepository.Get(null, x => x.OrderByDescending(x => x.ModifiedOn))
            );

            //// Async test
            //FieldAsync<ListGraphType<RelationTypeType>>(
            //    "relationTypes",
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await relationTypeRepository.GetAsync(null, x => x.OrderByDescending(x => x.ModifiedOn))
            //        );
            //    }
            //);

            Field<RelationTypeType>(
                "relationType",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: context => relationTypeRepository.GetById(context.GetArgument<Guid>("id"))
            );

            //// Async test
            //FieldAsync<RelationTypeType>(
            //    "relationType",
            //    arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await relationTypeRepository.GetByIdAsync(context.GetArgument<Guid>("id"))
            //        );
            //    }
            //);

			// Addresses
            
            Field<ListGraphType<AddressType>>(
                "addresses",
                resolve: context => addressRepository.Get(null, x => x.OrderByDescending(x => x.ModifiedOn))
            );

            //// Async test
            //FieldAsync<ListGraphType<AddressType>>(
            //    "addresses",
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await addressRepository.GetAsync(null, x => x.OrderByDescending(x => x.ModifiedOn))
            //        );
            //    }
            //);

            Field<AddressType>(
                "address",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: context => addressRepository.GetById(context.GetArgument<Guid>("id"))
            );

            //// Async test
            //FieldAsync<AddressType>(
            //    "address",
            //    arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await addressRepository.GetByIdAsync(context.GetArgument<Guid>("id"))
            //        );
            //    }
            //);

			// Contacts
            
            Field<ListGraphType<ContactType>>(
                "contacts",
                resolve: context => contactRepository.Get(null, x => x.OrderByDescending(x => x.ModifiedOn))
            );

            //// Async test
            //FieldAsync<ListGraphType<ContactType>>(
            //    "contacts",
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await contactRepository.GetAsync(null, x => x.OrderByDescending(x => x.ModifiedOn))
            //        );
            //    }
            //);

            Field<ContactType>(
                "contact",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: context => contactRepository.GetById(context.GetArgument<Guid>("id"))
            );

            //// Async test
            //FieldAsync<ContactType>(
            //    "contact",
            //    arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await contactRepository.GetByIdAsync(context.GetArgument<Guid>("id"))
            //        );
            //    }
            //);

			// Accounts
            
            Field<ListGraphType<AccountType>>(
                "accounts",
                resolve: context => accountRepository.Get(null, x => x.OrderByDescending(x => x.ModifiedOn))
            );

            //// Async test
            //FieldAsync<ListGraphType<AccountType>>(
            //    "accounts",
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await accountRepository.GetAsync(null, x => x.OrderByDescending(x => x.ModifiedOn))
            //        );
            //    }
            //);

            Field<AccountType>(
                "account",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: context => accountRepository.GetById(context.GetArgument<Guid>("id"))
            );

            //// Async test
            //FieldAsync<AccountType>(
            //    "account",
            //    arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await accountRepository.GetByIdAsync(context.GetArgument<Guid>("id"))
            //        );
            //    }
            //);

			// WorkOrders
            
            Field<ListGraphType<WorkOrderType>>(
                "workOrders",
                resolve: context => workOrderRepository.Get(null, x => x.OrderByDescending(x => x.ModifiedOn))
            );

            //// Async test
            //FieldAsync<ListGraphType<WorkOrderType>>(
            //    "workOrders",
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await workOrderRepository.GetAsync(null, x => x.OrderByDescending(x => x.ModifiedOn))
            //        );
            //    }
            //);

            Field<WorkOrderType>(
                "workOrder",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: context => workOrderRepository.GetById(context.GetArgument<Guid>("id"))
            );

            //// Async test
            //FieldAsync<WorkOrderType>(
            //    "workOrder",
            //    arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await workOrderRepository.GetByIdAsync(context.GetArgument<Guid>("id"))
            //        );
            //    }
            //);

        }
    }
}
