using AutoMapper;
using Test.API.Models;
using Test.API.ViewModels;

namespace Test.API.DAL
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Accounts
			CreateMap<Account, AccountVM>();
            CreateMap<AccountVM, Account>();

            // Contacts
			CreateMap<Contact, ContactVM>();
            CreateMap<ContactVM, Contact>();

            // Calls
			CreateMap<Call, CallVM>();
            CreateMap<CallVM, Call>();

            // Notes
			CreateMap<Note, NoteVM>();
            CreateMap<NoteVM, Note>();

            // Documents
			CreateMap<Document, DocumentVM>();
            CreateMap<DocumentVM, Document>();

            // Emails
			CreateMap<Email, EmailVM>();
            CreateMap<EmailVM, Email>();
        }
    }
}
