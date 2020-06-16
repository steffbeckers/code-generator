using GraphQL.Types;
using System;
using RJM.API.DAL.Repositories;
using RJM.API.Models;

namespace RJM.API.GraphQL.Types
{
    public class SkillAliasType : ObjectGraphType<SkillAlias>
    {
        public SkillAliasType(
            SkillRepository skillRepository,
			SkillAliasRepository skillAliasRepository
        )
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Name);
            Field(x => x.Description, nullable: true);

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
