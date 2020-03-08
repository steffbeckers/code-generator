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
			ProductRepository productRepository,
			CartRepository cartRepository,
			OrderRepository orderRepository,
			OrderStateRepository orderStateRepository,
			AddressRepository addressRepository
        )
        {
            this.AuthorizeWith("Authorized");

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

			// Carts
            
            Field<ListGraphType<CartType>>(
                "carts",
                resolve: context => cartRepository.Get(null, x => x.OrderByDescending(x => x.ModifiedOn))
            );

            //// Async test
            //FieldAsync<ListGraphType<CartType>>(
            //    "carts",
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await cartRepository.GetAsync(null, x => x.OrderByDescending(x => x.ModifiedOn))
            //        );
            //    }
            //);

            Field<CartType>(
                "cart",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: context => cartRepository.GetById(context.GetArgument<Guid>("id"))
            );

            //// Async test
            //FieldAsync<CartType>(
            //    "cart",
            //    arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await cartRepository.GetByIdAsync(context.GetArgument<Guid>("id"))
            //        );
            //    }
            //);

			// Orders
            
            Field<ListGraphType<OrderType>>(
                "orders",
                resolve: context => orderRepository.Get(null, x => x.OrderByDescending(x => x.ModifiedOn))
            );

            //// Async test
            //FieldAsync<ListGraphType<OrderType>>(
            //    "orders",
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await orderRepository.GetAsync(null, x => x.OrderByDescending(x => x.ModifiedOn))
            //        );
            //    }
            //);

            Field<OrderType>(
                "order",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: context => orderRepository.GetById(context.GetArgument<Guid>("id"))
            );

            //// Async test
            //FieldAsync<OrderType>(
            //    "order",
            //    arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await orderRepository.GetByIdAsync(context.GetArgument<Guid>("id"))
            //        );
            //    }
            //);

			// OrderStates
            
            Field<ListGraphType<OrderStateType>>(
                "orderStates",
                resolve: context => orderStateRepository.Get(null, x => x.OrderByDescending(x => x.ModifiedOn))
            );

            //// Async test
            //FieldAsync<ListGraphType<OrderStateType>>(
            //    "orderStates",
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await orderStateRepository.GetAsync(null, x => x.OrderByDescending(x => x.ModifiedOn))
            //        );
            //    }
            //);

            Field<OrderStateType>(
                "orderState",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: context => orderStateRepository.GetById(context.GetArgument<Guid>("id"))
            );

            //// Async test
            //FieldAsync<OrderStateType>(
            //    "orderState",
            //    arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await orderStateRepository.GetByIdAsync(context.GetArgument<Guid>("id"))
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

        }
    }
}
