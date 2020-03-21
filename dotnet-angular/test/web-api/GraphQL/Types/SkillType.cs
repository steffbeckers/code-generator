using GraphQL.Types;
using System;
using RJM.API.DAL.Repositories;
using RJM.API.Models;

namespace RJM.API.GraphQL.Types
{
    public class SkillType : ObjectGraphType<Skill>
    {
        public SkillType(
            SkillAliasRepository skillAliasRepository,
			SkillRepository skillRepository,
            ResumeRepository resumeRepository,
			ResumeSkillRepository resumeSkillRepository
        )
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Name);
            Field(x => x.Description, nullable: true);

            Field<ListGraphType<SkillAliasType>>(
                "skillAliases",
                resolve: context => skillAliasRepository.GetBySkillAliasId(context.Source.Id)
            );

            //// Async test
            //FieldAsync<ListGraphType<SkillAliasType>>(
            //    "skillAliases",
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await skillAliasRepository.GetBySkillAliasIdAsync(context.Source.Id)
            //        );
            //    }
            //);

            Field<ListGraphType<ResumeType>>(
                "resumes",
                resolve: context => resumeRepository.GetBySkillId(context.Source.Id)
            );

            //// Async test
            //FieldAsync<ListGraphType<ResumeType>>(
            //    "resumes",
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await resumeRepository.GetBySkillIdAsync(context.Source.Id)
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
