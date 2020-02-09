using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Test.API.Models;
using Test.API.ViewModels;

namespace Test.API.DAL
{
	/// <summary>
	/// Profile for mapping models to/from view models with AutoMapper.
	/// </summary>
    public class AutoMapperProfile : Profile
    {
		/// <summary>
		/// The constructor of AutoMapperProfile.
		/// </summary>
        public AutoMapperProfile()
        {
            // Accounts
			CreateMap<Account, AccountVM>();
            CreateMap<AccountVM, Account>();

            // Products
			CreateMap<Product, ProductVM>()
                .ForMember(
                    x => x.Suppliers,
                    x => x.MapFrom(
                        y => y.ProductSupplier.Select(z => z.Supplier)
                    )
                );
            CreateMap<ProductVM, Product>()
                .ForMember(
                    x => x.ProductSupplier,
                    x => {
                        x.PreCondition(z => z.SupplierId != null);
                        x.MapFrom(
                            y => new List<ProductSupplier>() {
                                new ProductSupplier()
                                {
                                    SupplierId = (Guid)y.SupplierId,
                                    Comment = y.SupplierComment
                                }
                            }
                        );
                    }
                );

            // Suppliers
			CreateMap<Supplier, SupplierVM>()
                .ForMember(
                    x => x.Products,
                    x => x.MapFrom(
                        y => y.ProductSupplier.Select(z => z.Product)
                    )
                );
            CreateMap<SupplierVM, Supplier>()
                .ForMember(
                    x => x.ProductSupplier,
                    x => {
                        x.PreCondition(z => z.ProductId != null);
                        x.MapFrom(
                            y => new List<ProductSupplier>() {
                                new ProductSupplier()
                                {
                                    ProductId = (Guid)y.ProductId,
                                    Comment = y.ProductComment
                                }
                            }
                        );
                    }
                );

            // ProductDetails
			CreateMap<ProductDetail, ProductDetailVM>();
            CreateMap<ProductDetailVM, ProductDetail>();

            // Users
			CreateMap<User, UserVM>();
            CreateMap<UserVM, User>();
        }
    }
}
