using GraphQL.Server.Authorization.AspNetCore;
using GraphQL.Types;
using System;
using Test.API.BLL;
using Test.API.GraphQL.Types;
using Test.API.Models;

namespace Test.API.GraphQL
{
    public class TestMutation : ObjectGraphType
    {
        public TestMutation(
			CountryBLL countryBLL,
			RelationTypeBLL relationTypeBLL,
			AddressBLL addressBLL,
			ContactBLL contactBLL,
			AccountBLL accountBLL,
			WorkOrderBLL workOrderBLL
        )
        {
            this.AuthorizeWith("Authorized");

			// Countries
            FieldAsync<CountryType>(
                "createCountry",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<CountryInputType>>
                    {
                        Name = "country"
                    }
                ),
                resolve: async context =>
                {
                    Country country = context.GetArgument<Country>("country");

                    return await context.TryAsyncResolve(
                        async c => await countryBLL.CreateCountryAsync(country)
                    );
                }
            );

            FieldAsync<CountryType>(
                "updateCountry",
                arguments: new QueryArguments(
                    //new QueryArgument<NonNullGraphType<IdGraphType>>
                    //{
                    //    Name = "id"
                    //},
                    new QueryArgument<NonNullGraphType<CountryInputType>>
                    {
                        Name = "country"
                    }
                ),
                resolve: async context =>
                {
                    //Guid id = context.GetArgument<Guid>("id");
                    Country country = context.GetArgument<Country>("country");

                    return await context.TryAsyncResolve(
                        async c => await countryBLL.UpdateCountryAsync(country)
                    );
                }
            );

            FieldAsync<CountryType>(
                "removeCountry",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>>
                    {
                        Name = "id"
                    }
                ),
                resolve: async context =>
                {
                    Guid id = context.GetArgument<Guid>("id");

                    return await context.TryAsyncResolve(
                        async c => await countryBLL.DeleteCountryByIdAsync(id)
                    );
                }
            );

			// RelationTypes
            FieldAsync<RelationTypeType>(
                "createRelationType",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<RelationTypeInputType>>
                    {
                        Name = "relationType"
                    }
                ),
                resolve: async context =>
                {
                    RelationType relationType = context.GetArgument<RelationType>("relationType");

                    return await context.TryAsyncResolve(
                        async c => await relationTypeBLL.CreateRelationTypeAsync(relationType)
                    );
                }
            );

            FieldAsync<RelationTypeType>(
                "updateRelationType",
                arguments: new QueryArguments(
                    //new QueryArgument<NonNullGraphType<IdGraphType>>
                    //{
                    //    Name = "id"
                    //},
                    new QueryArgument<NonNullGraphType<RelationTypeInputType>>
                    {
                        Name = "relationType"
                    }
                ),
                resolve: async context =>
                {
                    //Guid id = context.GetArgument<Guid>("id");
                    RelationType relationType = context.GetArgument<RelationType>("relationType");

                    return await context.TryAsyncResolve(
                        async c => await relationTypeBLL.UpdateRelationTypeAsync(relationType)
                    );
                }
            );

            FieldAsync<RelationTypeType>(
                "removeRelationType",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>>
                    {
                        Name = "id"
                    }
                ),
                resolve: async context =>
                {
                    Guid id = context.GetArgument<Guid>("id");

                    return await context.TryAsyncResolve(
                        async c => await relationTypeBLL.DeleteRelationTypeByIdAsync(id)
                    );
                }
            );

			// Addresses
            FieldAsync<AddressType>(
                "createAddress",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<AddressInputType>>
                    {
                        Name = "address"
                    }
                ),
                resolve: async context =>
                {
                    Address address = context.GetArgument<Address>("address");

                    return await context.TryAsyncResolve(
                        async c => await addressBLL.CreateAddressAsync(address)
                    );
                }
            );

            FieldAsync<AddressType>(
                "updateAddress",
                arguments: new QueryArguments(
                    //new QueryArgument<NonNullGraphType<IdGraphType>>
                    //{
                    //    Name = "id"
                    //},
                    new QueryArgument<NonNullGraphType<AddressInputType>>
                    {
                        Name = "address"
                    }
                ),
                resolve: async context =>
                {
                    //Guid id = context.GetArgument<Guid>("id");
                    Address address = context.GetArgument<Address>("address");

                    return await context.TryAsyncResolve(
                        async c => await addressBLL.UpdateAddressAsync(address)
                    );
                }
            );

            FieldAsync<AddressType>(
                "removeAddress",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>>
                    {
                        Name = "id"
                    }
                ),
                resolve: async context =>
                {
                    Guid id = context.GetArgument<Guid>("id");

                    return await context.TryAsyncResolve(
                        async c => await addressBLL.DeleteAddressByIdAsync(id)
                    );
                }
            );

			// Contacts
            FieldAsync<ContactType>(
                "createContact",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ContactInputType>>
                    {
                        Name = "contact"
                    }
                ),
                resolve: async context =>
                {
                    Contact contact = context.GetArgument<Contact>("contact");

                    return await context.TryAsyncResolve(
                        async c => await contactBLL.CreateContactAsync(contact)
                    );
                }
            );

            FieldAsync<ContactType>(
                "updateContact",
                arguments: new QueryArguments(
                    //new QueryArgument<NonNullGraphType<IdGraphType>>
                    //{
                    //    Name = "id"
                    //},
                    new QueryArgument<NonNullGraphType<ContactInputType>>
                    {
                        Name = "contact"
                    }
                ),
                resolve: async context =>
                {
                    //Guid id = context.GetArgument<Guid>("id");
                    Contact contact = context.GetArgument<Contact>("contact");

                    return await context.TryAsyncResolve(
                        async c => await contactBLL.UpdateContactAsync(contact)
                    );
                }
            );

            FieldAsync<ContactType>(
                "removeContact",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>>
                    {
                        Name = "id"
                    }
                ),
                resolve: async context =>
                {
                    Guid id = context.GetArgument<Guid>("id");

                    return await context.TryAsyncResolve(
                        async c => await contactBLL.DeleteContactByIdAsync(id)
                    );
                }
            );

			// Accounts
            FieldAsync<AccountType>(
                "createAccount",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<AccountInputType>>
                    {
                        Name = "account"
                    }
                ),
                resolve: async context =>
                {
                    Account account = context.GetArgument<Account>("account");

                    return await context.TryAsyncResolve(
                        async c => await accountBLL.CreateAccountAsync(account)
                    );
                }
            );

            FieldAsync<AccountType>(
                "updateAccount",
                arguments: new QueryArguments(
                    //new QueryArgument<NonNullGraphType<IdGraphType>>
                    //{
                    //    Name = "id"
                    //},
                    new QueryArgument<NonNullGraphType<AccountInputType>>
                    {
                        Name = "account"
                    }
                ),
                resolve: async context =>
                {
                    //Guid id = context.GetArgument<Guid>("id");
                    Account account = context.GetArgument<Account>("account");

                    return await context.TryAsyncResolve(
                        async c => await accountBLL.UpdateAccountAsync(account)
                    );
                }
            );

            FieldAsync<AccountType>(
                "removeAccount",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>>
                    {
                        Name = "id"
                    }
                ),
                resolve: async context =>
                {
                    Guid id = context.GetArgument<Guid>("id");

                    return await context.TryAsyncResolve(
                        async c => await accountBLL.DeleteAccountByIdAsync(id)
                    );
                }
            );

			// WorkOrders
            FieldAsync<WorkOrderType>(
                "createWorkOrder",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<WorkOrderInputType>>
                    {
                        Name = "workOrder"
                    }
                ),
                resolve: async context =>
                {
                    WorkOrder workOrder = context.GetArgument<WorkOrder>("workOrder");

                    return await context.TryAsyncResolve(
                        async c => await workOrderBLL.CreateWorkOrderAsync(workOrder)
                    );
                }
            );

            FieldAsync<WorkOrderType>(
                "updateWorkOrder",
                arguments: new QueryArguments(
                    //new QueryArgument<NonNullGraphType<IdGraphType>>
                    //{
                    //    Name = "id"
                    //},
                    new QueryArgument<NonNullGraphType<WorkOrderInputType>>
                    {
                        Name = "workOrder"
                    }
                ),
                resolve: async context =>
                {
                    //Guid id = context.GetArgument<Guid>("id");
                    WorkOrder workOrder = context.GetArgument<WorkOrder>("workOrder");

                    return await context.TryAsyncResolve(
                        async c => await workOrderBLL.UpdateWorkOrderAsync(workOrder)
                    );
                }
            );

            FieldAsync<WorkOrderType>(
                "removeWorkOrder",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>>
                    {
                        Name = "id"
                    }
                ),
                resolve: async context =>
                {
                    Guid id = context.GetArgument<Guid>("id");

                    return await context.TryAsyncResolve(
                        async c => await workOrderBLL.DeleteWorkOrderByIdAsync(id)
                    );
                }
            );

        }
    }
}

