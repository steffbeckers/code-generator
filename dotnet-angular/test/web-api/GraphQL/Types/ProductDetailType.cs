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
        }
    }
}
