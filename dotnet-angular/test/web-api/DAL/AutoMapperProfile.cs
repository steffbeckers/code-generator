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
            // Resumes
			CreateMap<Resume, ResumeVM>()
                .ForMember(
                    x => x.Skills,
                    x => x.MapFrom(
                        y => y.ResumeSkill.Select(z => z.Skill)
                    )
                );
            CreateMap<ResumeVM, Resume>()
                .ForMember(
                    x => x.ResumeSkill,
                    x =>
                    {
                        x.PreCondition(z => z.SkillId != null);
                        x.MapFrom(
                            y => new List<ResumeSkill>() {
                                new ResumeSkill()
                                {
                                    SkillId = (Guid)y.SkillId,
                                    Rating = y.SkillRating,
                                    Description = y.SkillDescription
                                }
                            }
                        );
                    }
                );

            // ResumeStates
			CreateMap<ResumeState, ResumeStateVM>();
            CreateMap<ResumeStateVM, ResumeState>();

            // Skills
			CreateMap<Skill, SkillVM>()
                .ForMember(
                    x => x.Resumes,
                    x => x.MapFrom(
                        y => y.ResumeSkill.Select(z => z.Resume)
                    )
                );
            CreateMap<SkillVM, Skill>()
                .ForMember(
                    x => x.ResumeSkill,
                    x =>
                    {
                        x.PreCondition(z => z.ResumeId != null);
                        x.MapFrom(
                            y => new List<ResumeSkill>() {
                                new ResumeSkill()
                                {
                                    ResumeId = (Guid)y.ResumeId,
                                    Rating = y.ResumeRating,
                                    Description = y.ResumeDescription
                                }
                            }
                        );
                    }
                );

            // SkillAliases
			CreateMap<SkillAlias, SkillAliasVM>();
            CreateMap<SkillAliasVM, SkillAlias>();

            // Users
			CreateMap<User, UserVM>();
            CreateMap<UserVM, User>();
        }
    }
}
