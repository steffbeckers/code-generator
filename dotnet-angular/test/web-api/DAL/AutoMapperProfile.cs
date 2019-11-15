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
                        y => this.AccountNoteLinks(y)
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
                        y => this.AccountNoteLinks(y)
                    )
                );

            // Todos
            CreateMap<Todo, TodoVM>();
            CreateMap<TodoVM, Todo>();

        }

        private List<AccountNote> AccountNoteLinks(AccountVM y)
        {
            List<AccountNote> links = new List<AccountNote>();

            if (y.NoteId != null)
            {
                links.Add(new AccountNote()
                {
                    NoteId = (Guid)y.NoteId
                });
            }

            return links;
        }

        private List<AccountNote> AccountNoteLinks(NoteVM y)
        {
            List<AccountNote> links = new List<AccountNote>();

            if (y.AccountId != null)
            {
                links.Add(new AccountNote()
                {
                    AccountId = (Guid)y.AccountId
                });
            }

            return links;
        }
    }
}
