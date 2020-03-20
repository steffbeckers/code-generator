using GraphQL.Types;
using GraphQL.Server.Authorization.AspNetCore;
using RJM.API.DAL.Repositories;
using RJM.API.GraphQL.Types;
using System;
using System.Linq;

namespace RJM.API.GraphQL
{
    public class RJMQuery : ObjectGraphType
    {
        public RJMQuery(
			ResumeRepository resumeRepository,
			SkillRepository skillRepository
        )
        {
            this.AuthorizeWith("Authorized");

			// Resumes
            
            Field<ListGraphType<ResumeType>>(
                "resumes",
                resolve: context => resumeRepository.Get(null, x => x.OrderByDescending(x => x.ModifiedOn))
            );

            //// Async test
            //FieldAsync<ListGraphType<ResumeType>>(
            //    "resumes",
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await resumeRepository.GetAsync(null, x => x.OrderByDescending(x => x.ModifiedOn))
            //        );
            //    }
            //);

            Field<ResumeType>(
                "resume",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: context => resumeRepository.GetById(context.GetArgument<Guid>("id"))
            );

            //// Async test
            //FieldAsync<ResumeType>(
            //    "resume",
            //    arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await resumeRepository.GetByIdAsync(context.GetArgument<Guid>("id"))
            //        );
            //    }
            //);

			// Skills
            
            Field<ListGraphType<SkillType>>(
                "skills",
                resolve: context => skillRepository.Get(null, x => x.OrderByDescending(x => x.ModifiedOn))
            );

            //// Async test
            //FieldAsync<ListGraphType<SkillType>>(
            //    "skills",
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await skillRepository.GetAsync(null, x => x.OrderByDescending(x => x.ModifiedOn))
            //        );
            //    }
            //);

            Field<SkillType>(
                "skill",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: context => skillRepository.GetById(context.GetArgument<Guid>("id"))
            );

            //// Async test
            //FieldAsync<SkillType>(
            //    "skill",
            //    arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await skillRepository.GetByIdAsync(context.GetArgument<Guid>("id"))
            //        );
            //    }
            //);

        }
    }
}
