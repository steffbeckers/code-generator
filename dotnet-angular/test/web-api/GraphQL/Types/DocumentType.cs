using GraphQL.Types;
using System;
using RJM.API.DAL.Repositories;
using RJM.API.Models;

namespace RJM.API.GraphQL.Types
{
    public class DocumentType : ObjectGraphType<Document>
    {
        public DocumentType(
            ResumeStateRepository resumeStateRepository,
			DocumentRepository documentRepository,
            SkillRepository skillRepository,
			ResumeSkillRepository resumeSkillRepository
        )
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Name);
            Field(x => x.DisplayName, nullable: true);
            Field(x => x.Description, nullable: true);
            Field(x => x.Path, nullable: true);
            Field(x => x.URL, nullable: true);
            Field(x => x.MimeType, nullable: true);

            Field<ResumeStateType>(
                "resumeState",
                resolve: context =>
                {
                    if (context.Source.ResumeStateId != null)
                        return resumeStateRepository.GetById((Guid)context.Source.ResumeStateId);
                    return null;
                }
            );

            //// Async test
            //FieldAsync<ResumeStateType>(
            //    "resumeState",
            //    resolve: async context =>
            //    {
            //        if (context.Source.ResumeStateId != null) {
            //            return await context.TryAsyncResolve(
            //                async c => await resumeStateRepository.GetByIdAsync((Guid)context.Source.ResumeStateId)
            //            );
            //        }
            //        
            //        return null;
            //    }
            //);

            Field<ListGraphType<SkillType>>(
                "skills",
                resolve: context => skillRepository.GetByDocumentId(context.Source.Id)
            );

            //// Async test
            //FieldAsync<ListGraphType<SkillType>>(
            //    "skills",
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await skillRepository.GetByDocumentIdAsync(context.Source.Id)
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
