using GraphQL.Types;
using System;
using RJM.API.DAL.Repositories;
using RJM.API.Models;

namespace RJM.API.GraphQL.Types
{
    public class ResumeType : ObjectGraphType<Resume>
    {
        public ResumeType(
			ResumeRepository resumeRepository,
            SkillRepository skillRepository,
			ResumeSkillRepository resumeSkillRepository
        )
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.JobTitle, nullable: true);
            Field(x => x.Description, nullable: true);

            Field<ListGraphType<SkillType>>(
                "skills",
                resolve: context => skillRepository.GetByResumeId(context.Source.Id)
            );

            //// Async test
            //FieldAsync<ListGraphType<SkillType>>(
            //    "skills",
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await skillRepository.GetByResumeIdAsync(context.Source.Id)
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
