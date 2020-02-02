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
            Field<SupplierType>(
                "supplier",
                resolve: context =>
                {
                    if (context.Source.SupplierId != null)
                        return supplierRepository.GetById((Guid)context.Source.SupplierId);
                    return null;
                }
            );
        }
    }
}
