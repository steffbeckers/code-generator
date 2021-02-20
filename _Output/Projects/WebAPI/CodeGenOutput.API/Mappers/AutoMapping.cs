using AutoMapper;
using CodeGenOutput.API.Models;
using CodeGenOutput.API.ViewModels;

namespace CodeGenOutput.API.Mappers
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
