using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using RJM.API.Models;
using RJM.API.ViewModels;
using RJM.API.ViewModels.Identity;

namespace RJM.API.DAL
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
            // Skills
			CreateMap<Skill, SkillVM>();
            CreateMap<SkillVM, Skill>();
            // Users
			CreateMap<User, UserVM>();
            CreateMap<UserVM, User>();
        }
    }
}
