using GraphQL.Types;
using System;
using Test.API.DAL.Repositories;
using Test.API.Models;

namespace Test.API.GraphQL.Types
{
    public class ProductSupplierType : ObjectGraphType<ProductSupplier>
    {
        public ProductSupplierType(
            ProductRepository productRepository,
            SupplierRepository supplierRepository,
            ProductSupplierRepository productSupplierRepository
        )
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Comment, nullable: true);

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

            Field<SupplierType>(
                "supplier",
                resolve: context =>
                {
                    if (context.Source.SupplierId != null)
                        return supplierRepository.GetById((Guid)context.Source.SupplierId);
                    return null;
                }
            );

            //// Async test
            //FieldAsync<SupplierType>(
            //    "supplier",
            //    resolve: async context =>
            //    {
            //        if (context.Source.SupplierId != null) {
            //            return await context.TryAsyncResolve(
            //                async c => await supplierRepository.GetByIdAsync((Guid)context.Source.SupplierId)
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
