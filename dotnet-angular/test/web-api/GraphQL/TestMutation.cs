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
			ProductBLL productBLL,
			CartBLL cartBLL,
			OrderBLL orderBLL,
			OrderStateBLL orderStateBLL,
			AddressBLL addressBLL
        )
        {
            this.AuthorizeWith("Authorized");

			// Products
            FieldAsync<ProductType>(
                "createProduct",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ProductInputType>>
                    {
                        Name = "product"
                    }
                ),
                resolve: async context =>
                {
                    Product product = context.GetArgument<Product>("product");

                    return await context.TryAsyncResolve(
                        async c => await productBLL.CreateProductAsync(product)
                    );
                }
            );

            FieldAsync<ProductType>(
                "updateProduct",
                arguments: new QueryArguments(
                    //new QueryArgument<NonNullGraphType<IdGraphType>>
                    //{
                    //    Name = "id"
                    //},
                    new QueryArgument<NonNullGraphType<ProductInputType>>
                    {
                        Name = "product"
                    }
                ),
                resolve: async context =>
                {
                    //Guid id = context.GetArgument<Guid>("id");
                    Product product = context.GetArgument<Product>("product");

                    return await context.TryAsyncResolve(
                        async c => await productBLL.UpdateProductAsync(product)
                    );
                }
            );

            FieldAsync<ProductType>(
                "removeProduct",
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
                        async c => await productBLL.DeleteProductByIdAsync(id)
                    );
                }
            );

			// Carts
            FieldAsync<CartType>(
                "createCart",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<CartInputType>>
                    {
                        Name = "cart"
                    }
                ),
                resolve: async context =>
                {
                    Cart cart = context.GetArgument<Cart>("cart");

                    return await context.TryAsyncResolve(
                        async c => await cartBLL.CreateCartAsync(cart)
                    );
                }
            );

            FieldAsync<CartType>(
                "updateCart",
                arguments: new QueryArguments(
                    //new QueryArgument<NonNullGraphType<IdGraphType>>
                    //{
                    //    Name = "id"
                    //},
                    new QueryArgument<NonNullGraphType<CartInputType>>
                    {
                        Name = "cart"
                    }
                ),
                resolve: async context =>
                {
                    //Guid id = context.GetArgument<Guid>("id");
                    Cart cart = context.GetArgument<Cart>("cart");

                    return await context.TryAsyncResolve(
                        async c => await cartBLL.UpdateCartAsync(cart)
                    );
                }
            );

            FieldAsync<CartType>(
                "linkProductToCart",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<CartProductInputType>>
                    {
                        Name = "cartProduct"
                    }
                ),
                resolve: async context =>
                {
                    CartProduct cartProduct = context.GetArgument<CartProduct>("cartProduct");

                    return await context.TryAsyncResolve(
                        async c => await cartBLL.LinkProductToCartAsync(cartProduct)
                    );
                }
            );

            FieldAsync<CartType>(
                "unlinkProductFromCart",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<CartProductInputType>>
                    {
                        Name = "cartProduct"
                    }
                ),
                resolve: async context =>
                {
                    CartProduct cartProduct = context.GetArgument<CartProduct>("cartProduct");

                    return await context.TryAsyncResolve(
                        async c => await cartBLL.UnlinkProductFromCartAsync(cartProduct)
                    );
                }
            );

            FieldAsync<CartType>(
                "removeCart",
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
                        async c => await cartBLL.DeleteCartByIdAsync(id)
                    );
                }
            );

			// Orders
            FieldAsync<OrderType>(
                "createOrder",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<OrderInputType>>
                    {
                        Name = "order"
                    }
                ),
                resolve: async context =>
                {
                    Order order = context.GetArgument<Order>("order");

                    return await context.TryAsyncResolve(
                        async c => await orderBLL.CreateOrderAsync(order)
                    );
                }
            );

            FieldAsync<OrderType>(
                "updateOrder",
                arguments: new QueryArguments(
                    //new QueryArgument<NonNullGraphType<IdGraphType>>
                    //{
                    //    Name = "id"
                    //},
                    new QueryArgument<NonNullGraphType<OrderInputType>>
                    {
                        Name = "order"
                    }
                ),
                resolve: async context =>
                {
                    //Guid id = context.GetArgument<Guid>("id");
                    Order order = context.GetArgument<Order>("order");

                    return await context.TryAsyncResolve(
                        async c => await orderBLL.UpdateOrderAsync(order)
                    );
                }
            );

            FieldAsync<OrderType>(
                "removeOrder",
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
                        async c => await orderBLL.DeleteOrderByIdAsync(id)
                    );
                }
            );

			// OrderStates
            FieldAsync<OrderStateType>(
                "createOrderState",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<OrderStateInputType>>
                    {
                        Name = "orderState"
                    }
                ),
                resolve: async context =>
                {
                    OrderState orderState = context.GetArgument<OrderState>("orderState");

                    return await context.TryAsyncResolve(
                        async c => await orderStateBLL.CreateOrderStateAsync(orderState)
                    );
                }
            );

            FieldAsync<OrderStateType>(
                "updateOrderState",
                arguments: new QueryArguments(
                    //new QueryArgument<NonNullGraphType<IdGraphType>>
                    //{
                    //    Name = "id"
                    //},
                    new QueryArgument<NonNullGraphType<OrderStateInputType>>
                    {
                        Name = "orderState"
                    }
                ),
                resolve: async context =>
                {
                    //Guid id = context.GetArgument<Guid>("id");
                    OrderState orderState = context.GetArgument<OrderState>("orderState");

                    return await context.TryAsyncResolve(
                        async c => await orderStateBLL.UpdateOrderStateAsync(orderState)
                    );
                }
            );

            FieldAsync<OrderStateType>(
                "removeOrderState",
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
                        async c => await orderStateBLL.DeleteOrderStateByIdAsync(id)
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

        }
    }
}

