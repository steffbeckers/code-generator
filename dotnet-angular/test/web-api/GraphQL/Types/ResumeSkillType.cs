using GraphQL.Types;
using System;
using RJM.API.DAL.Repositories;
using RJM.API.Models;

namespace RJM.API.GraphQL.Types
{
    public class ResumeSkillType : ObjectGraphType<ResumeSkill>
    {
        public ResumeSkillType(
            ResumeRepository resumeRepository,
            SkillRepository skillRepository,
			ResumeSkillRepository resumeSkillRepository
        )
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Rating);
            Field(x => x.Description, nullable: true);

            Field<ResumeType>(
                "resume",
                resolve: context =>
                {
                    if (context.Source.ResumeId != null)
                        return resumeRepository.GetById((Guid)context.Source.ResumeId);
                    return null;
                }
            );

            //// Async test
            //FieldAsync<ResumeType>(
            //    "resume",
            //    resolve: async context =>
            //    {
            //        if (context.Source.ResumeId != null) {
            //            return await context.TryAsyncResolve(
            //                async c => await resumeRepository.GetByIdAsync((Guid)context.Source.ResumeId)
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
