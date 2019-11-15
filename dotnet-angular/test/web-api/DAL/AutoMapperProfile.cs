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
			CreateMap<Account, AccountVM>()
                .ForMember(
                    x => x.Notes,
                    x => x.MapFrom(
                        y => y.AccountNote.Select(z => z.Note)
                    )
                );
            CreateMap<AccountVM, Account>()
                .ForMember(
                    x => x.AccountNote,
                    x => x.MapFrom(
                        y => new List<AccountNote>() {
                            new AccountNote()
                            {
                                NoteId = (Guid)y.NoteId
                            }
                        }
                    )
                );

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
            CreateMap<NoteVM, Note>()
                .ForMember(
                    x => x.AccountNote,
                    x => x.MapFrom(
                        y => new List<AccountNote>() {
                            new AccountNote()
                            {
                                AccountId = (Guid)y.AccountId
                            }
                        }
                    )
                );

            // Todos
			CreateMap<Todo, TodoVM>();
            CreateMap<TodoVM, Todo>();

        }
    }
}
