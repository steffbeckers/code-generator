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
			DocumentRepository documentRepository,
			ResumeRepository resumeRepository,
			ResumeStateRepository resumeStateRepository,
			SkillRepository skillRepository,
			SkillAliasRepository skillAliasRepository,
			JobRepository jobRepository,
			JobStateRepository jobStateRepository
        )
        {
            this.AuthorizeWith("Authorized");

			// Documents
            
            Field<ListGraphType<DocumentType>>(
                "documents",
                resolve: context => documentRepository.Get(null, x => x.OrderByDescending(x => x.ModifiedOn))
            );

            //// Async test
            //FieldAsync<ListGraphType<DocumentType>>(
            //    "documents",
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await documentRepository.GetAsync(null, x => x.OrderByDescending(x => x.ModifiedOn))
            //        );
            //    }
            //);

            Field<DocumentType>(
                "document",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: context => documentRepository.GetById(context.GetArgument<Guid>("id"))
            );

            //// Async test
            //FieldAsync<DocumentType>(
            //    "document",
            //    arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await documentRepository.GetByIdAsync(context.GetArgument<Guid>("id"))
            //        );
            //    }
            //);

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

			// ResumeStates
            
            Field<ListGraphType<ResumeStateType>>(
                "resumeStates",
                resolve: context => resumeStateRepository.Get(null, x => x.OrderByDescending(x => x.ModifiedOn))
            );

            //// Async test
            //FieldAsync<ListGraphType<ResumeStateType>>(
            //    "resumeStates",
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await resumeStateRepository.GetAsync(null, x => x.OrderByDescending(x => x.ModifiedOn))
            //        );
            //    }
            //);

            Field<ResumeStateType>(
                "resumeState",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: context => resumeStateRepository.GetById(context.GetArgument<Guid>("id"))
            );

            //// Async test
            //FieldAsync<ResumeStateType>(
            //    "resumeState",
            //    arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await resumeStateRepository.GetByIdAsync(context.GetArgument<Guid>("id"))
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

			// SkillAliases
            
            Field<ListGraphType<SkillAliasType>>(
                "skillAliases",
                resolve: context => skillAliasRepository.Get(null, x => x.OrderByDescending(x => x.ModifiedOn))
            );

            //// Async test
            //FieldAsync<ListGraphType<SkillAliasType>>(
            //    "skillAliases",
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await skillAliasRepository.GetAsync(null, x => x.OrderByDescending(x => x.ModifiedOn))
            //        );
            //    }
            //);

            Field<SkillAliasType>(
                "skillAlias",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: context => skillAliasRepository.GetById(context.GetArgument<Guid>("id"))
            );

            //// Async test
            //FieldAsync<SkillAliasType>(
            //    "skillAlias",
            //    arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await skillAliasRepository.GetByIdAsync(context.GetArgument<Guid>("id"))
            //        );
            //    }
            //);

			// Jobs
            
            Field<ListGraphType<JobType>>(
                "jobs",
                resolve: context => jobRepository.Get(null, x => x.OrderByDescending(x => x.ModifiedOn))
            );

            //// Async test
            //FieldAsync<ListGraphType<JobType>>(
            //    "jobs",
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await jobRepository.GetAsync(null, x => x.OrderByDescending(x => x.ModifiedOn))
            //        );
            //    }
            //);

            Field<JobType>(
                "job",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: context => jobRepository.GetById(context.GetArgument<Guid>("id"))
            );

            //// Async test
            //FieldAsync<JobType>(
            //    "job",
            //    arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await jobRepository.GetByIdAsync(context.GetArgument<Guid>("id"))
            //        );
            //    }
            //);

			// JobStates
            
            Field<ListGraphType<JobStateType>>(
                "jobStates",
                resolve: context => jobStateRepository.Get(null, x => x.OrderByDescending(x => x.ModifiedOn))
            );

            //// Async test
            //FieldAsync<ListGraphType<JobStateType>>(
            //    "jobStates",
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await jobStateRepository.GetAsync(null, x => x.OrderByDescending(x => x.ModifiedOn))
            //        );
            //    }
            //);

            Field<JobStateType>(
                "jobState",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: context => jobStateRepository.GetById(context.GetArgument<Guid>("id"))
            );

            //// Async test
            //FieldAsync<JobStateType>(
            //    "jobState",
            //    arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await jobStateRepository.GetByIdAsync(context.GetArgument<Guid>("id"))
            //        );
            //    }
            //);

        }
    }
}
