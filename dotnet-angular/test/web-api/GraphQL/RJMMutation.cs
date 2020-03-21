using GraphQL.Server.Authorization.AspNetCore;
using GraphQL.Types;
using System;
using RJM.API.BLL;
using RJM.API.GraphQL.Types;
using RJM.API.Models;

namespace RJM.API.GraphQL
{
    public class RJMMutation : ObjectGraphType
    {
        public RJMMutation(
			ResumeBLL resumeBLL,
			ResumeStateBLL resumeStateBLL,
			SkillBLL skillBLL,
			SkillAliasBLL skillAliasBLL
        )
        {
            this.AuthorizeWith("Authorized");

			// Resumes
            FieldAsync<ResumeType>(
                "createResume",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ResumeInputType>>
                    {
                        Name = "resume"
                    }
                ),
                resolve: async context =>
                {
                    Resume resume = context.GetArgument<Resume>("resume");

                    return await context.TryAsyncResolve(
                        async c => await resumeBLL.CreateResumeAsync(resume)
                    );
                }
            );

            FieldAsync<ResumeType>(
                "updateResume",
                arguments: new QueryArguments(
                    //new QueryArgument<NonNullGraphType<IdGraphType>>
                    //{
                    //    Name = "id"
                    //},
                    new QueryArgument<NonNullGraphType<ResumeInputType>>
                    {
                        Name = "resume"
                    }
                ),
                resolve: async context =>
                {
                    //Guid id = context.GetArgument<Guid>("id");
                    Resume resume = context.GetArgument<Resume>("resume");

                    return await context.TryAsyncResolve(
                        async c => await resumeBLL.UpdateResumeAsync(resume)
                    );
                }
            );

            FieldAsync<ResumeType>(
                "linkSkillToResume",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ResumeSkillInputType>>
                    {
                        Name = "resumeSkill"
                    }
                ),
                resolve: async context =>
                {
                    ResumeSkill resumeSkill = context.GetArgument<ResumeSkill>("resumeSkill");

                    return await context.TryAsyncResolve(
                        async c => await resumeBLL.LinkSkillToResumeAsync(resumeSkill)
                    );
                }
            );

            FieldAsync<ResumeType>(
                "unlinkSkillFromResume",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ResumeSkillInputType>>
                    {
                        Name = "resumeSkill"
                    }
                ),
                resolve: async context =>
                {
                    ResumeSkill resumeSkill = context.GetArgument<ResumeSkill>("resumeSkill");

                    return await context.TryAsyncResolve(
                        async c => await resumeBLL.UnlinkSkillFromResumeAsync(resumeSkill)
                    );
                }
            );

            FieldAsync<ResumeType>(
                "removeResume",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>>
                    {
                        Name = "id"
                    }
                ),
                resolve: async context =>
                {
                    Guid id = context.GetArgument<Guid>("id");

                    return await context.TryAsyncResolve(
                        async c => await resumeBLL.DeleteResumeByIdAsync(id)
                    );
                }
            );

			// ResumeStates
            FieldAsync<ResumeStateType>(
                "createResumeState",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ResumeStateInputType>>
                    {
                        Name = "resumeState"
                    }
                ),
                resolve: async context =>
                {
                    ResumeState resumeState = context.GetArgument<ResumeState>("resumeState");

                    return await context.TryAsyncResolve(
                        async c => await resumeStateBLL.CreateResumeStateAsync(resumeState)
                    );
                }
            );

            FieldAsync<ResumeStateType>(
                "updateResumeState",
                arguments: new QueryArguments(
                    //new QueryArgument<NonNullGraphType<IdGraphType>>
                    //{
                    //    Name = "id"
                    //},
                    new QueryArgument<NonNullGraphType<ResumeStateInputType>>
                    {
                        Name = "resumeState"
                    }
                ),
                resolve: async context =>
                {
                    //Guid id = context.GetArgument<Guid>("id");
                    ResumeState resumeState = context.GetArgument<ResumeState>("resumeState");

                    return await context.TryAsyncResolve(
                        async c => await resumeStateBLL.UpdateResumeStateAsync(resumeState)
                    );
                }
            );

            FieldAsync<ResumeStateType>(
                "removeResumeState",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>>
                    {
                        Name = "id"
                    }
                ),
                resolve: async context =>
                {
                    Guid id = context.GetArgument<Guid>("id");

                    return await context.TryAsyncResolve(
                        async c => await resumeStateBLL.DeleteResumeStateByIdAsync(id)
                    );
                }
            );

			// Skills
            FieldAsync<SkillType>(
                "createSkill",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<SkillInputType>>
                    {
                        Name = "skill"
                    }
                ),
                resolve: async context =>
                {
                    Skill skill = context.GetArgument<Skill>("skill");

                    return await context.TryAsyncResolve(
                        async c => await skillBLL.CreateSkillAsync(skill)
                    );
                }
            );

            FieldAsync<SkillType>(
                "updateSkill",
                arguments: new QueryArguments(
                    //new QueryArgument<NonNullGraphType<IdGraphType>>
                    //{
                    //    Name = "id"
                    //},
                    new QueryArgument<NonNullGraphType<SkillInputType>>
                    {
                        Name = "skill"
                    }
                ),
                resolve: async context =>
                {
                    //Guid id = context.GetArgument<Guid>("id");
                    Skill skill = context.GetArgument<Skill>("skill");

                    return await context.TryAsyncResolve(
                        async c => await skillBLL.UpdateSkillAsync(skill)
                    );
                }
            );

            FieldAsync<SkillType>(
                "linkResumeToSkill",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ResumeSkillInputType>>
                    {
                        Name = "resumeSkill"
                    }
                ),
                resolve: async context =>
                {
                    ResumeSkill resumeSkill = context.GetArgument<ResumeSkill>("resumeSkill");

                    return await context.TryAsyncResolve(
                        async c => await skillBLL.LinkResumeToSkillAsync(resumeSkill)
                    );
                }
            );

            FieldAsync<SkillType>(
                "unlinkResumeFromSkill",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ResumeSkillInputType>>
                    {
                        Name = "resumeSkill"
                    }
                ),
                resolve: async context =>
                {
                    ResumeSkill resumeSkill = context.GetArgument<ResumeSkill>("resumeSkill");

                    return await context.TryAsyncResolve(
                        async c => await skillBLL.UnlinkResumeFromSkillAsync(resumeSkill)
                    );
                }
            );

            FieldAsync<SkillType>(
                "removeSkill",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>>
                    {
                        Name = "id"
                    }
                ),
                resolve: async context =>
                {
                    Guid id = context.GetArgument<Guid>("id");

                    return await context.TryAsyncResolve(
                        async c => await skillBLL.DeleteSkillByIdAsync(id)
                    );
                }
            );

			// SkillAliases
            FieldAsync<SkillAliasType>(
                "createSkillAlias",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<SkillAliasInputType>>
                    {
                        Name = "skillAlias"
                    }
                ),
                resolve: async context =>
                {
                    SkillAlias skillAlias = context.GetArgument<SkillAlias>("skillAlias");

                    return await context.TryAsyncResolve(
                        async c => await skillAliasBLL.CreateSkillAliasAsync(skillAlias)
                    );
                }
            );

            FieldAsync<SkillAliasType>(
                "updateSkillAlias",
                arguments: new QueryArguments(
                    //new QueryArgument<NonNullGraphType<IdGraphType>>
                    //{
                    //    Name = "id"
                    //},
                    new QueryArgument<NonNullGraphType<SkillAliasInputType>>
                    {
                        Name = "skillAlias"
                    }
                ),
                resolve: async context =>
                {
                    //Guid id = context.GetArgument<Guid>("id");
                    SkillAlias skillAlias = context.GetArgument<SkillAlias>("skillAlias");

                    return await context.TryAsyncResolve(
                        async c => await skillAliasBLL.UpdateSkillAliasAsync(skillAlias)
                    );
                }
            );

            FieldAsync<SkillAliasType>(
                "removeSkillAlias",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>>
                    {
                        Name = "id"
                    }
                ),
                resolve: async context =>
                {
                    Guid id = context.GetArgument<Guid>("id");

                    return await context.TryAsyncResolve(
                        async c => await skillAliasBLL.DeleteSkillAliasByIdAsync(id)
                    );
                }
            );

        }
    }
}

