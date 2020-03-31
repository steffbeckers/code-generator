using GraphQL.Types;
using System;
using RJM.API.DAL.Repositories;
using RJM.API.Models;

namespace RJM.API.GraphQL.Types
{
    public class ResumeType : ObjectGraphType<Resume>
    {
        public ResumeType(
            ResumeStateRepository resumeStateRepository,
			ResumeRepository resumeRepository,
            DocumentRepository documentRepository,
			DocumentResumeRepository documentResumeRepository,
            SkillRepository skillRepository,
			ResumeSkillRepository resumeSkillRepository
        )
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.JobTitle, nullable: true);
            Field(x => x.Description, nullable: true);

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

            Field<ListGraphType<DocumentType>>(
                "documents",
                resolve: context => documentRepository.GetByResumeId(context.Source.Id)
            );

            //// Async test
            //FieldAsync<ListGraphType<DocumentType>>(
            //    "documents",
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await documentRepository.GetByResumeIdAsync(context.Source.Id)
            //        );
            //    }
            //);

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
