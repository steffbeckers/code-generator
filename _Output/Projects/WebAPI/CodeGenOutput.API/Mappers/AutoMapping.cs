using AutoMapper;
using CodeGenOutput.API.Models;
using CodeGenOutput.API.ViewModels;

namespace CodeGenOutput.API.Mappers
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Account, AccountVM>();
            CreateMap<Account, AccountListVM>();
            CreateMap<AccountVM, Account>();
            CreateMap<AccountCreateVM, Account>();
            CreateMap<AccountUpdateVM, Account>();
            
            CreateMap<Contact, ContactVM>();
            CreateMap<Contact, ContactListVM>();
            CreateMap<ContactVM, Contact>();
            CreateMap<ContactCreateVM, Contact>();
            CreateMap<ContactUpdateVM, Contact>();
            
        }
    }
}
