using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.API.DAL.Repositories;
using Test.API.Models;

namespace Test.API.BLL
{
    public class DocumentBLL
    {
        private readonly DocumentRepository documentRepository;
        // TODO: private readonly Document...Repository document...Repository;

        public DocumentBLL(
			DocumentRepository documentRepository//,
			// TODO: Document...Repository document...Repository
		)
        {
            this.documentRepository = documentRepository;
            // TODO: this.Document...Repository = Document...Repository;
        }

		public async Task<IEnumerable<Document>> GetAllDocumentsAsync()
        {
            return await this.documentRepository.GetAsync();
        }

		public async Task<Document> GetDocumentByIdAsync(Guid id)
        {
            return await this.documentRepository.GetByIdAsync(id);
        }

        public async Task<Document> CreateDocumentAsync(Document document)
        {
            return await this.documentRepository.InsertAsync(document);
        }

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

        public async Task<bool> RemoveDocumentAsync(Guid id)
        {
            // Retrieve existing
            Document document = await this.documentRepository.GetByIdAsync(id);
            if (document == null)
            {
                return true;
            }

            await this.documentRepository.DeleteAsync(document);

            return true;
        }
    }
}
