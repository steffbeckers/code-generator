using AutoMapper;
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
			CreateMap<Account, AccountVM>()
                .ForMember(
                    x => x.Notes,
                    x => x.MapFrom(
                        y => y.AccountNote.Select(z => z.Note)
                    )
                );
            CreateMap<AccountVM, Account>();

            // Contacts
			CreateMap<Contact, ContactVM>();
            CreateMap<ContactVM, Contact>();

            // Addresses
			CreateMap<Address, AddressVM>();
            CreateMap<AddressVM, Address>();

            // Notes
			CreateMap<Note, NoteVM>()
                .ForMember(
                    x => x.Accounts,
                    x => x.MapFrom(
                        y => y.AccountNote.Select(z => z.Account)
                    )
                );
            CreateMap<NoteVM, Note>();

            // Todos
			CreateMap<Todo, TodoVM>();
            CreateMap<TodoVM, Todo>();

        }
    }
}
