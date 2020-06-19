using GraphQL.Types;
using System;
using RJM.API.DAL.Repositories;
using RJM.API.Models;

namespace RJM.API.GraphQL.Types
{
    public class JobStateType : ObjectGraphType<JobState>
    {
        public JobStateType(
            JobRepository jobRepository,
			JobStateRepository jobStateRepository
        )
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Name);
            Field(x => x.DisplayName);

            Field<ListGraphType<JobType>>(
                "jobs",
                resolve: context => jobRepository.GetByJobStateId(context.Source.Id)
            );

            //// Async test
            //FieldAsync<ListGraphType<JobType>>(
            //    "jobs",
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await jobRepository.GetByJobStateIdAsync(context.Source.Id)
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
