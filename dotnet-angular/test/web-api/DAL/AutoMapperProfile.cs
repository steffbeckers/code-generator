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
            // Countries
			CreateMap<Country, CountryVM>();
            CreateMap<CountryVM, Country>();

            // RelationTypes
			CreateMap<RelationType, RelationTypeVM>();
            CreateMap<RelationTypeVM, RelationType>();

            // Addresses
			CreateMap<Address, AddressVM>();
            CreateMap<AddressVM, Address>();

            // Contacts
			CreateMap<Contact, ContactVM>();
            CreateMap<ContactVM, Contact>();

            // Accounts
			CreateMap<Account, AccountVM>();
            CreateMap<AccountVM, Account>();

            // WorkOrders
			CreateMap<WorkOrder, WorkOrderVM>();
            CreateMap<WorkOrderVM, WorkOrder>();
            // Users
			CreateMap<User, UserVM>();
            CreateMap<UserVM, User>();
        }
    }
}
