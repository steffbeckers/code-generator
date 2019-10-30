using AutoMapper;
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

            // Contacts
			CreateMap<Contact, ContactVM>();
            CreateMap<ContactVM, Contact>();

            // Addresses
			CreateMap<Address, AddressVM>();
            CreateMap<AddressVM, Address>();
        }
    }
}
