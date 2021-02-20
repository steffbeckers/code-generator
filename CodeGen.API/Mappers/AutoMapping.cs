using AutoMapper;
using CodeGen.API.Models;
using CodeGen.API.ViewModels;

namespace CodeGen.API.Mappers
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Project, ProjectVM>();
            CreateMap<Project, ProjectListVM>();
            CreateMap<Project, ProjectUpdateVM>();
            CreateMap<ProjectVM, Project>();
            CreateMap<ProjectCreateVM, Project>();
            CreateMap<ProjectUpdateVM, Project>();

        }
    }
}
