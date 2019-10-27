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

            // Projects
            CreateMap<Project, ProjectVM>();
            CreateMap<ProjectVM, Project>();

            // Todoes
            CreateMap<Todo, TodoVM>();
            CreateMap<TodoVM, Todo>();
        }
    }
}
