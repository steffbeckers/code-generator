using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Test.API.Models;
using Test.API.ViewModels;
using Test.API.ViewModels.Identity;

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
            // Products
			CreateMap<Product, ProductVM>()
                .ForMember(
                    x => x.Carts,
                    x => x.MapFrom(
                        y => y.CartProduct.Select(z => z.Cart)
                    )
                );
            CreateMap<ProductVM, Product>()
                .ForMember(
                    x => x.CartProduct,
                    x =>
                    {
                        x.PreCondition(z => z.CartId != null);
                        x.MapFrom(
                            y => new List<CartProduct>() {
                                new CartProduct()
                                {
                                    CartId = (Guid)y.CartId,
                                    Quantity = y.CartQuantity,
                                    Price = y.CartPrice
                                }
                            }
                        );
                    }
                );

            // Carts
			CreateMap<Cart, CartVM>()
                .ForMember(
                    x => x.Products,
                    x => x.MapFrom(
                        y => y.CartProduct.Select(z => z.Product)
                    )
                );
            CreateMap<CartVM, Cart>()
                .ForMember(
                    x => x.CartProduct,
                    x =>
                    {
                        x.PreCondition(z => z.ProductId != null);
                        x.MapFrom(
                            y => new List<CartProduct>() {
                                new CartProduct()
                                {
                                    ProductId = (Guid)y.ProductId,
                                    Quantity = y.ProductQuantity,
                                    Price = y.ProductPrice
                                }
                            }
                        );
                    }
                );

            // Orders
			CreateMap<Order, OrderVM>();
            CreateMap<OrderVM, Order>();

            // OrderStates
			CreateMap<OrderState, OrderStateVM>();
            CreateMap<OrderStateVM, OrderState>();

            // Addresses
			CreateMap<Address, AddressVM>();
            CreateMap<AddressVM, Address>();
            // Users
			CreateMap<User, UserVM>();
            CreateMap<UserVM, User>();
        }
    }
}
