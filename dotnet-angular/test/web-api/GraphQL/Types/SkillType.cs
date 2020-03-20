using GraphQL.Types;
using System;
using RJM.API.DAL.Repositories;
using RJM.API.Models;

namespace RJM.API.GraphQL.Types
{
    public class SkillType : ObjectGraphType<Skill>
    {
        public SkillType(
			SkillRepository skillRepository
        )
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Name);
            Field(x => x.Description, nullable: true);

            Field(x => x.CreatedByUserId, type: typeof(IdGraphType));
            // TODO: Field(x => x.CreatedByUser, type: typeof(UserType));
            Field(x => x.ModifiedByUserId, type: typeof(IdGraphType));
            // TODO: Field(x => x.ModifiedByUser, type: typeof(UserType));
        }
    }
}
