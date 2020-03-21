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

            // Skills
			CreateMap<Skill, SkillVM>()
                .ForMember(
                    x => x.Resumes,
                    x => x.MapFrom(
                        y => y.ResumeSkill.Select(z => z.Resume)
                    )
                );
			CreateMap<Skill, SkillVM>()
                .ForMember(
                    x => x.Jobs,
                    x => x.MapFrom(
                        y => y.JobSkill.Select(z => z.Job)
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
            CreateMap<SkillVM, Skill>()
                .ForMember(
                    x => x.JobSkill,
                    x =>
                    {
                        x.PreCondition(z => z.JobId != null);
                        x.MapFrom(
                            y => new List<JobSkill>() {
                                new JobSkill()
                                {
                                    JobId = (Guid)y.JobId,
                                    Rating = y.JobRating,
                                    Description = y.JobDescription
                                }
                            }
                        );
                    }
                );

            // SkillAliases

            // Jobs
			CreateMap<Job, JobVM>()
                .ForMember(
                    x => x.Skills,
                    x => x.MapFrom(
                        y => y.JobSkill.Select(z => z.Skill)
                    )
                );
            CreateMap<JobVM, Job>()
                .ForMember(
                    x => x.JobSkill,
                    x =>
                    {
                        x.PreCondition(z => z.SkillId != null);
                        x.MapFrom(
                            y => new List<JobSkill>() {
                                new JobSkill()
                                {
                                    SkillId = (Guid)y.SkillId,
                                    Rating = y.SkillRating,
                                    Description = y.SkillDescription
                                }
                            }
                        );
                    }
                );

            // JobStates

            // Users
			CreateMap<User, UserVM>();
            CreateMap<UserVM, User>();
        }
    }
}
