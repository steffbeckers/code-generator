using GraphQL.Types;
using System;
using RJM.API.DAL.Repositories;
using RJM.API.Models;

namespace RJM.API.GraphQL.Types
{
    public class DocumentResumeType : ObjectGraphType<DocumentResume>
    {
        public DocumentResumeType(
            DocumentRepository documentRepository,
            ResumeRepository resumeRepository,
			DocumentResumeRepository documentResumeRepository
        )
        {
            Field(x => x.Id, type: typeof(IdGraphType));

            Field<DocumentType>(
                "document",
                resolve: context =>
                {
                    if (context.Source.DocumentId != null)
                        return documentRepository.GetById((Guid)context.Source.DocumentId);
                    return null;
                }
            );

            //// Async test
            //FieldAsync<DocumentType>(
            //    "document",
            //    resolve: async context =>
            //    {
            //        if (context.Source.DocumentId != null) {
            //            return await context.TryAsyncResolve(
            //                async c => await documentRepository.GetByIdAsync((Guid)context.Source.DocumentId)
            //            );
            //        }
            //        
            //        return null;
            //    }
            //);

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

            Field(x => x.CreatedByUserId, type: typeof(IdGraphType));
            // TODO: Field(x => x.CreatedByUser, type: typeof(UserType));
            Field(x => x.ModifiedByUserId, type: typeof(IdGraphType));
            // TODO: Field(x => x.ModifiedByUser, type: typeof(UserType));
        }
    }
}
