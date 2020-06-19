using GraphQL.Types;
using System;
using RJM.API.DAL.Repositories;
using RJM.API.Models;

namespace RJM.API.GraphQL.Types
{
    public class SettingType : ObjectGraphType<Setting>
    {
        public SettingType(
			SettingRepository settingRepository
        )
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Key);
            Field(x => x.Value, nullable: true);

            Field(x => x.CreatedByUserId, type: typeof(IdGraphType));
            // TODO: Field(x => x.CreatedByUser, type: typeof(UserType));
            Field(x => x.ModifiedByUserId, type: typeof(IdGraphType));
            // TODO: Field(x => x.ModifiedByUser, type: typeof(UserType));
        }
    }
}
