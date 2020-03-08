using GraphQL.Types;
using System;
using Test.API.DAL.Repositories;
using Test.API.Models;

namespace Test.API.GraphQL.Types
{
    public class ProductType : ObjectGraphType<Product>
    {
        public ProductType(
			ProductRepository productRepository,
            CartRepository cartRepository,
			CartProductRepository cartProductRepository
        )
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Name);
            Field(x => x.Description, nullable: true);
            Field(x => x.Price, nullable: true);

            Field<ListGraphType<CartType>>(
                "carts",
                resolve: context => cartRepository.GetByProductId(context.Source.Id)
            );

            //// Async test
            //FieldAsync<ListGraphType<CartType>>(
            //    "carts",
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await cartRepository.GetByProductIdAsync(context.Source.Id)
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
