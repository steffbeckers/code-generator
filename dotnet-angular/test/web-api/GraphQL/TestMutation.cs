using GraphQL.Types;
using Test.API.BLL;
using Test.API.GraphQL.Types;
using Test.API.Models;
using System;

namespace Test.API.GraphQL
{
    public class TestMutation : ObjectGraphType
    {
        public TestMutation(
			AccountBLL accountBLL,
			ProductBLL productBLL,
			SupplierBLL supplierBLL,
			ProductDetailBLL productDetailBLL
        )
        {
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
                "linkSupplierToProduct",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ProductSupplierInputType>>
                    {
                        Name = "productSupplier"
                    }
                ),
                resolve: async context =>
                {
                    ProductSupplier productSupplier = context.GetArgument<ProductSupplier>("productSupplier");

                    return await context.TryAsyncResolve(
                        async c => await productBLL.LinkSupplierToProductAsync(productSupplier)
                    );
                }
            );

            FieldAsync<ProductType>(
                "unlinkSupplierFromProduct",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ProductSupplierInputType>>
                    {
                        Name = "productSupplier"
                    }
                ),
                resolve: async context =>
                {
                    ProductSupplier productSupplier = context.GetArgument<ProductSupplier>("productSupplier");

                    return await context.TryAsyncResolve(
                        async c => await productBLL.UnlinkSupplierFromProductAsync(productSupplier)
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

			// Suppliers
            FieldAsync<SupplierType>(
                "createSupplier",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<SupplierInputType>>
                    {
                        Name = "supplier"
                    }
                ),
                resolve: async context =>
                {
                    Supplier supplier = context.GetArgument<Supplier>("supplier");

                    return await context.TryAsyncResolve(
                        async c => await supplierBLL.CreateSupplierAsync(supplier)
                    );
                }
            );

            FieldAsync<SupplierType>(
                "updateSupplier",
                arguments: new QueryArguments(
                    //new QueryArgument<NonNullGraphType<IdGraphType>>
                    //{
                    //    Name = "id"
                    //},
                    new QueryArgument<NonNullGraphType<SupplierInputType>>
                    {
                        Name = "supplier"
                    }
                ),
                resolve: async context =>
                {
                    //Guid id = context.GetArgument<Guid>("id");
                    Supplier supplier = context.GetArgument<Supplier>("supplier");

                    return await context.TryAsyncResolve(
                        async c => await supplierBLL.UpdateSupplierAsync(supplier)
                    );
                }
            );

            FieldAsync<SupplierType>(
                "linkProductToSupplier",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ProductSupplierInputType>>
                    {
                        Name = "productSupplier"
                    }
                ),
                resolve: async context =>
                {
                    ProductSupplier productSupplier = context.GetArgument<ProductSupplier>("productSupplier");

                    return await context.TryAsyncResolve(
                        async c => await supplierBLL.LinkProductToSupplierAsync(productSupplier)
                    );
                }
            );

            FieldAsync<SupplierType>(
                "unlinkProductFromSupplier",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ProductSupplierInputType>>
                    {
                        Name = "productSupplier"
                    }
                ),
                resolve: async context =>
                {
                    ProductSupplier productSupplier = context.GetArgument<ProductSupplier>("productSupplier");

                    return await context.TryAsyncResolve(
                        async c => await supplierBLL.UnlinkProductFromSupplierAsync(productSupplier)
                    );
                }
            );

            FieldAsync<SupplierType>(
                "removeSupplier",
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
                        async c => await supplierBLL.DeleteSupplierByIdAsync(id)
                    );
                }
            );

			// ProductDetails
            FieldAsync<ProductDetailType>(
                "createProductDetail",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ProductDetailInputType>>
                    {
                        Name = "productDetail"
                    }
                ),
                resolve: async context =>
                {
                    ProductDetail productDetail = context.GetArgument<ProductDetail>("productDetail");

                    return await context.TryAsyncResolve(
                        async c => await productDetailBLL.CreateProductDetailAsync(productDetail)
                    );
                }
            );

            FieldAsync<ProductDetailType>(
                "updateProductDetail",
                arguments: new QueryArguments(
                    //new QueryArgument<NonNullGraphType<IdGraphType>>
                    //{
                    //    Name = "id"
                    //},
                    new QueryArgument<NonNullGraphType<ProductDetailInputType>>
                    {
                        Name = "productDetail"
                    }
                ),
                resolve: async context =>
                {
                    //Guid id = context.GetArgument<Guid>("id");
                    ProductDetail productDetail = context.GetArgument<ProductDetail>("productDetail");

                    return await context.TryAsyncResolve(
                        async c => await productDetailBLL.UpdateProductDetailAsync(productDetail)
                    );
                }
            );

            FieldAsync<ProductDetailType>(
                "removeProductDetail",
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
                        async c => await productDetailBLL.DeleteProductDetailByIdAsync(id)
                    );
                }
            );

        }
    }
}

