using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.API.DAL.Repositories;
using Test.API.Models;

namespace Test.API.BLL
{
	/// <summary>
	/// The business logic layer for Documents.
	/// </summary>
    public class DocumentBLL
    {
        private readonly DocumentRepository documentRepository;
        // TODO: private readonly Document...Repository document...Repository;

		/// <summary>
		/// The constructor of the Document business logic layer.
		/// </summary>
        public DocumentBLL(
			DocumentRepository documentRepository//,
			// TODO: Document...Repository document...Repository
		)
        {
            this.documentRepository = documentRepository;
            // TODO: this.Document...Repository = Document...Repository;
        }

		/// <summary>
		/// Retrieves all documents.
		/// </summary>
		public async Task<IEnumerable<Document>> GetAllDocumentsAsync()
        {
            return await this.documentRepository.GetAsync();
        }

		/// <summary>
		/// Retrieves one document by Id.
		/// </summary>
		public async Task<Document> GetDocumentByIdAsync(Guid id)
        {
            return await this.documentRepository.GetByIdAsync(id);
        }

		/// <summary>
		/// Creates a new document record.
		/// </summary>
        public async Task<Document> CreateDocumentAsync(Document document)
        {
            return await this.documentRepository.InsertAsync(document);
        }

		/// <summary>
		/// Updates an existing document record by Id.
		/// </summary>
        public async Task<Document> UpdateDocumentAsync(Guid id, Document documentUpdate)
        {
            // Retrieve existing
            Document document = await this.documentRepository.GetByIdAsync(id);
            if (document == null)
            {
                return null;
            }

            // Mapping
            document.Name = documentUpdate.Name;

            return await this.documentRepository.UpdateAsync(document);
        }

		// TODO
        //public async Task<League> LinkPlayerToLeagueAsync(LeaguePlayer leaguePlayer)
        //{
        //    LeaguePlayer leaguePlayerLink = this.leaguePlayerRepository.GetByLeagueAndPlayerId(leaguePlayer.LeagueId, leaguePlayer.PlayerId);
		//
        //    if (leaguePlayerLink == null)
        //    {
        //        await this.leaguePlayerRepository.InsertAsync(leaguePlayer);
        //    }
        //    else
        //    {
        //        // Mapping
        //        leaguePlayerLink.Handicap = leaguePlayer.Handicap;
		//
        //        await this.leaguePlayerRepository.UpdateAsync(leaguePlayerLink);
        //    }
		//
        //    return this.leagueRepository.GetWithPlayersById(leaguePlayer.LeagueId);
        //}

		// TODO
        //public async Task<League> UnlinkPlayerFromLeagueAsync(LeaguePlayer leaguePlayer)
        //{
        //    LeaguePlayer leaguePlayerLink = this.leaguePlayerRepository.GetByLeagueAndPlayerId(leaguePlayer.LeagueId, leaguePlayer.PlayerId);
		//
        //    if (leaguePlayerLink != null)
        //    {
        //        await this.leaguePlayerRepository.DeleteAsync(leaguePlayerLink);
        //    }

        //    return this.leagueRepository.GetWithPlayersById(leaguePlayer.LeagueId);
        //}

		/// <summary>
		/// Deletes an existing document record by Id.
		/// </summary>
        public async Task<Document> DeleteDocumentAsync(Document document)
        {
            await this.documentRepository.DeleteAsync(document);

            return document;
        }
    }
}
