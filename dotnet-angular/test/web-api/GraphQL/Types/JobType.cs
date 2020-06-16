using GraphQL.Types;
using System;
using RJM.API.DAL.Repositories;
using RJM.API.Models;

namespace RJM.API.GraphQL.Types
{
    public class JobType : ObjectGraphType<Job>
    {
        public JobType(
            JobStateRepository jobStateRepository,
			JobRepository jobRepository,
            SkillRepository skillRepository,
			JobSkillRepository jobSkillRepository
        )
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Title, nullable: true);
            Field(x => x.Description, nullable: true);

            Field<JobStateType>(
                "jobState",
                resolve: context =>
                {
                    if (context.Source.JobStateId != null)
                        return jobStateRepository.GetById((Guid)context.Source.JobStateId);
                    return null;
                }
            );

            //// Async test
            //FieldAsync<JobStateType>(
            //    "jobState",
            //    resolve: async context =>
            //    {
            //        if (context.Source.JobStateId != null) {
            //            return await context.TryAsyncResolve(
            //                async c => await jobStateRepository.GetByIdAsync((Guid)context.Source.JobStateId)
            //            );
            //        }
            //        
            //        return null;
            //    }
            //);

            Field<ListGraphType<SkillType>>(
                "skills",
                resolve: context => skillRepository.GetByJobId(context.Source.Id)
            );

            //// Async test
            //FieldAsync<ListGraphType<SkillType>>(
            //    "skills",
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await skillRepository.GetByJobIdAsync(context.Source.Id)
            //        );
            //    }
            //);

            Field(x => x.CreatedByUserId, type: typeof(IdGraphType));
            // TODO: Field(x => x.CreatedByUser, type: typeof(UserType));
            Field(x => x.ModifiedByUserId, type: typeof(IdGraphType));
            // TODO: Field(x => x.ModifiedByUser, type: typeof(UserType));
        }
    }
}
