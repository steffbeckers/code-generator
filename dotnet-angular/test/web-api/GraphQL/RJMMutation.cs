using GraphQL.Server.Authorization.AspNetCore;
using GraphQL.Types;
using System;
using RJM.API.BLL;
using RJM.API.GraphQL.Types;
using RJM.API.Models;

namespace RJM.API.GraphQL
{
    public class RJMMutation : ObjectGraphType
    {
        public RJMMutation(
			SkillBLL skillBLL
        )
        {
            this.AuthorizeWith("Authorized");

			// Skills
            FieldAsync<SkillType>(
                "createSkill",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<SkillInputType>>
                    {
                        Name = "skill"
                    }
                ),
                resolve: async context =>
                {
                    Skill skill = context.GetArgument<Skill>("skill");

                    return await context.TryAsyncResolve(
                        async c => await skillBLL.CreateSkillAsync(skill)
                    );
                }
            );

            FieldAsync<SkillType>(
                "updateSkill",
                arguments: new QueryArguments(
                    //new QueryArgument<NonNullGraphType<IdGraphType>>
                    //{
                    //    Name = "id"
                    //},
                    new QueryArgument<NonNullGraphType<SkillInputType>>
                    {
                        Name = "skill"
                    }
                ),
                resolve: async context =>
                {
                    //Guid id = context.GetArgument<Guid>("id");
                    Skill skill = context.GetArgument<Skill>("skill");

                    return await context.TryAsyncResolve(
                        async c => await skillBLL.UpdateSkillAsync(skill)
                    );
                }
            );

            FieldAsync<SkillType>(
                "removeSkill",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>>
                    {
                        Name = "id"
                    }
                ),
                resolve: async context =>
                {
                    Guid id = context.GetArgument<Guid>("id");

                    return await context.TryAsyncResolve(
                        async c => await skillBLL.DeleteSkillByIdAsync(id)
                    );
                }
            );

        }
    }
}

