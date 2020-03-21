using GraphQL.Types;
using System;
using RJM.API.DAL.Repositories;
using RJM.API.Models;

namespace RJM.API.GraphQL.Types
{
    public class JobSkillType : ObjectGraphType<JobSkill>
    {
        public JobSkillType(
            JobRepository jobRepository,
            SkillRepository skillRepository,
			JobSkillRepository jobSkillRepository
        )
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Rating, nullable: true);
            Field(x => x.Description, nullable: true);

            Field<JobType>(
                "job",
                resolve: context =>
                {
                    if (context.Source.JobId != null)
                        return jobRepository.GetById((Guid)context.Source.JobId);
                    return null;
                }
            );

            //// Async test
            //FieldAsync<JobType>(
            //    "job",
            //    resolve: async context =>
            //    {
            //        if (context.Source.JobId != null) {
            //            return await context.TryAsyncResolve(
            //                async c => await jobRepository.GetByIdAsync((Guid)context.Source.JobId)
            //            );
            //        }
            //        
            //        return null;
            //    }
            //);

            Field<SkillType>(
                "skill",
                resolve: context =>
                {
                    if (context.Source.SkillId != null)
                        return skillRepository.GetById((Guid)context.Source.SkillId);
                    return null;
                }
            );

            //// Async test
            //FieldAsync<SkillType>(
            //    "skill",
            //    resolve: async context =>
            //    {
            //        if (context.Source.SkillId != null) {
            //            return await context.TryAsyncResolve(
            //                async c => await skillRepository.GetByIdAsync((Guid)context.Source.SkillId)
            //            );
            //        }
            //        
            //        return null;
            //    }
            //);

            Field(x => x.CreatedByUserId, type: typeof(IdGraphType));
            // TODO: Field(x => x.CreatedByUser, type: typeof(UserType));
            Field(x => x.ModifiedByUserId, type: typeof(IdGraphType));
            // TODO: Field(x => x.ModifiedByUser, type: typeof(UserType));
        }
    }
}
