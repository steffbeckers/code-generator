using GraphQL.Types;
using System;
using Test.API.DAL.Repositories;
using Test.API.Models;

namespace Test.API.GraphQL.Types
{
    public class CartType : ObjectGraphType<Cart>
    {
        public CartType(
			CartRepository cartRepository,
            ProductRepository productRepository,
			CartProductRepository cartProductRepository
        )
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Name);

            Field<ListGraphType<ProductType>>(
                "products",
                resolve: context => productRepository.GetByCartId(context.Source.Id)
            );

            //// Async test
            //FieldAsync<ListGraphType<ProductType>>(
            //    "products",
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await productRepository.GetByCartIdAsync(context.Source.Id)
            //        );
            //    }
            //);

            Field(x => x.CreatedByUserId, type: typeof(IdGraphType));
            // TODO: Field(x => x.CreatedByUser, type: typeof(UserType));
            Field(x => x.ModifiedByUserId, type: typeof(IdGraphType));
            // TODO: Field(x => x.ModifiedByUser, type: typeof(UserType));
        }
    }
}
