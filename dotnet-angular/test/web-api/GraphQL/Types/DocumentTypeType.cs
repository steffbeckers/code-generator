using GraphQL.Types;
using System;
using RJM.API.DAL.Repositories;
using RJM.API.Models;

namespace RJM.API.GraphQL.Types
{
    public class DocumentTypeType : ObjectGraphType<DocumentType>
    {
        public DocumentTypeType(
            DocumentRepository documentRepository,
			DocumentTypeRepository documentTypeRepository
        )
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Name);
            Field(x => x.DisplayName);

            Field<ListGraphType<DocumentType>>(
                "documents",
                resolve: context => documentRepository.GetByDocumentTypeId(context.Source.Id)
            );

            //// Async test
            //FieldAsync<ListGraphType<DocumentType>>(
            //    "documents",
            //    resolve: async context =>
            //    {
            //        return await context.TryAsyncResolve(
            //            async c => await documentRepository.GetByDocumentTypeIdAsync(context.Source.Id)
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
