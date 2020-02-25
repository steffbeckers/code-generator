using GraphQL.Types;
using System;
using Test.API.DAL.Repositories;
using Test.API.Models;

namespace Test.API.GraphQL.Types
{
    public class ProductDetailType : ObjectGraphType<ProductDetail>
    {
        public ProductDetailType(
            ProductRepository productRepository,
            ProductDetailRepository productDetailRepository
        )
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Comment);

            Field<ProductType>(
                "product",
                resolve: context =>
                {
                    if (context.Source.ProductId != null)
                        return productRepository.GetById((Guid)context.Source.ProductId);
                    return null;
                }
            );

            //// Async test
            //FieldAsync<ProductType>(
            //    "product",
            //    resolve: async context =>
            //    {
            //        if (context.Source.ProductId != null) {
            //            return await context.TryAsyncResolve(
            //                async c => await productRepository.GetByIdAsync((Guid)context.Source.ProductId)
            //            );
            //        }
            //        
            //        return null;
            //    }
            //);

            Field(x => x.CreatedByUserId, type: typeof(IdGraphType));
            // TODO: Field(x => x.CreatedByUser, type: typeof(UserType));
            Field(x => x.ModifiedByUserId, type: typeof(IdGraphType));
            // TODO: Field(x => x.ModifiedByUser, type: typeof(UserType));
        }
    }
}
