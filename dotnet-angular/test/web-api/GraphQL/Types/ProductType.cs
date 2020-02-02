using GraphQL.Types;
using System;
using Test.API.DAL.Repositories;
using Test.API.Models;

namespace Test.API.GraphQL.Types
{
    public class ProductType : ObjectGraphType<Product>
    {
        public ProductType(
            ProductDetailRepository productDetailRepository,
			ProductRepository productRepository,
            SupplierRepository supplierRepository,
			ProductSupplierRepository productSupplierRepository
        )
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Name);
            Field(x => x.Code, nullable: true);
            Field(x => x.Quantity, nullable: true);
            Field(x => x.Price, nullable: true);

            Field<ListGraphType<ProductDetailType>>(
                "productDetails",
                resolve: context => productDetailRepository.GetByDetailsId(context.Source.DetailsId)
            );

            //// Async test
            //FieldAsync<ListGraphType<ProductDetailType>>(
            //    "productDetails",
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await productDetailRepository.GetByDetailsIdAsync(context.Source.DetailsId)
            //        );
            //    }
            //);

            Field<ListGraphType<SupplierType>>(
                "suppliers",
                resolve: context => supplierRepository.GetByProductId(context.Source.Id)
            );

            //// Async test
            //FieldAsync<ListGraphType<SupplierType>>(
            //    "suppliers",
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await supplierRepository.GetByProductIdAsync(context.Source.Id)
            //        );
            //    }
            //);

        }
    }
}
