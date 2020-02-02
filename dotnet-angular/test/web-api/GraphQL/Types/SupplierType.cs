using GraphQL.Types;
using System;
using Test.API.DAL.Repositories;
using Test.API.Models;

namespace Test.API.GraphQL.Types
{
    public class SupplierType : ObjectGraphType<Supplier>
    {
        public SupplierType(
			SupplierRepository supplierRepository,
            ProductRepository productRepository,
			ProductSupplierRepository productSupplierRepository
        )
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Name);
            Field(x => x.Phone, nullable: true);

            Field<ListGraphType<ProductType>>(
                "products",
                resolve: context => productRepository.GetBySupplierId(context.Source.Id)
            );

            //// Async test
            //FieldAsync<ListGraphType<ProductType>>(
            //    "products",
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await productRepository.GetBySupplierIdAsync(context.Source.Id)
            //        );
            //    }
            //);

        }
    }
}
