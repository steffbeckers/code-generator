using AutoMapper;
using CodeGenOutput.Models;
using CodeGenOutput.ViewModels;

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
        }
    }
}
