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
			ProductRepository productRepository,
			SupplierRepository supplierRepository,
			ProductDetailRepository productDetailRepository
        )
        {
			// Products
            Field<ListGraphType<ProductType>>(
                "products",
                resolve: context => productRepository.Get(null, x => x.OrderByDescending(x => x.ModifiedOn))
            );
            Field<ProductType>(
                "product",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: context => productRepository.GetById(context.GetArgument<Guid>("id"))
            );

			// Suppliers
            Field<ListGraphType<SupplierType>>(
                "suppliers",
                resolve: context => supplierRepository.Get(null, x => x.OrderByDescending(x => x.ModifiedOn))
            );
            Field<SupplierType>(
                "supplier",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: context => supplierRepository.GetById(context.GetArgument<Guid>("id"))
            );

			// ProductDetails
            Field<ListGraphType<ProductDetailType>>(
                "productDetails",
                resolve: context => productDetailRepository.Get(null, x => x.OrderByDescending(x => x.ModifiedOn))
            );
            Field<ProductDetailType>(
                "productDetail",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: context => productDetailRepository.GetById(context.GetArgument<Guid>("id"))
            );

        }
    }
}
