using GraphQL.Server.Authorization.AspNetCore;
using GraphQL.Types;
using System;
using System.Linq;
using Test.API.DAL.Repositories;
using Test.API.GraphQL.Types;

namespace Test.API.GraphQL
{
    public class TestQuery : ObjectGraphType
    {
        public TestQuery(
            AccountRepository accountRepository,
            ProductRepository productRepository,
            SupplierRepository supplierRepository,
            ProductDetailRepository productDetailRepository
        )
        {
            this.AuthorizeWith("Authorized");

            // Accounts

            Field<ListGraphType<AccountType>>(
                "accounts",
                resolve: context => accountRepository.Get(null, x => x.OrderBy(x => x.Name))
            );

            //// Async test
            //FieldAsync<ListGraphType<AccountType>>(
            //    "accounts",
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await accountRepository.GetAsync(null, x => x.OrderBy(x => x.Name))
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

            // Products

            Field<ListGraphType<ProductType>>(
                "products",
                resolve: context => productRepository.Get(null, x => x.OrderByDescending(x => x.ModifiedOn))
            );

            //// Async test
            //FieldAsync<ListGraphType<ProductType>>(
            //    "products",
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await productRepository.GetAsync(null, x => x.OrderByDescending(x => x.ModifiedOn))
            //        );
            //    }
            //);

            Field<ProductType>(
                "product",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: context => productRepository.GetById(context.GetArgument<Guid>("id"))
            );

            //// Async test
            //FieldAsync<ProductType>(
            //    "product",
            //    arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await productRepository.GetByIdAsync(context.GetArgument<Guid>("id"))
            //        );
            //    }
            //);

            // Suppliers

            Field<ListGraphType<SupplierType>>(
                "suppliers",
                resolve: context => supplierRepository.Get(null, x => x.OrderByDescending(x => x.ModifiedOn))
            );

            //// Async test
            //FieldAsync<ListGraphType<SupplierType>>(
            //    "suppliers",
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await supplierRepository.GetAsync(null, x => x.OrderByDescending(x => x.ModifiedOn))
            //        );
            //    }
            //);

            Field<SupplierType>(
                "supplier",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: context => supplierRepository.GetById(context.GetArgument<Guid>("id"))
            );

            //// Async test
            //FieldAsync<SupplierType>(
            //    "supplier",
            //    arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await supplierRepository.GetByIdAsync(context.GetArgument<Guid>("id"))
            //        );
            //    }
            //);

            // ProductDetails

            Field<ListGraphType<ProductDetailType>>(
                "productDetails",
                resolve: context => productDetailRepository.Get(null, x => x.OrderByDescending(x => x.ModifiedOn))
            );

            //// Async test
            //FieldAsync<ListGraphType<ProductDetailType>>(
            //    "productDetails",
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await productDetailRepository.GetAsync(null, x => x.OrderByDescending(x => x.ModifiedOn))
            //        );
            //    }
            //);

            Field<ProductDetailType>(
                "productDetail",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: context => productDetailRepository.GetById(context.GetArgument<Guid>("id"))
            );

            //// Async test
            //FieldAsync<ProductDetailType>(
            //    "productDetail",
            //    arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await productDetailRepository.GetByIdAsync(context.GetArgument<Guid>("id"))
            //        );
            //    }
            //);

        }
    }
}
