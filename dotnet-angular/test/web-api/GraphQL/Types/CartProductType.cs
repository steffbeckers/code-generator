using GraphQL.Types;
using System;
using Test.API.DAL.Repositories;
using Test.API.Models;

namespace Test.API.GraphQL.Types
{
    public class CartProductType : ObjectGraphType<CartProduct>
    {
        public CartProductType(
            CartRepository cartRepository,
            ProductRepository productRepository,
			CartProductRepository cartProductRepository
        )
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Quantity);
            Field(x => x.Price);

            Field<CartType>(
                "cart",
                resolve: context =>
                {
                    if (context.Source.CartId != null)
                        return cartRepository.GetById((Guid)context.Source.CartId);
                    return null;
                }
            );

            //// Async test
            //FieldAsync<CartType>(
            //    "cart",
            //    resolve: async context =>
            //    {
            //        if (context.Source.CartId != null) {
            //            return await context.TryAsyncResolve(
            //                async c => await cartRepository.GetByIdAsync((Guid)context.Source.CartId)
            //            );
            //        }
            //        
            //        return null;
            //    }
            //);

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
