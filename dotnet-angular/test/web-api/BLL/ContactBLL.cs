using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.API.DAL.Repositories;
using Test.API.Models;

namespace Test.API.BLL
{
	/// <summary>
	/// The business logic layer for Contacts.
	/// </summary>
    public class ContactBLL
    {
        private readonly ContactRepository contactRepository;
        // TODO: private readonly Contact...Repository contact...Repository;

		/// <summary>
		/// The constructor of the Contact business logic layer.
		/// </summary>
        public ContactBLL(
			ContactRepository contactRepository//,
			// TODO: Contact...Repository contact...Repository
		)
        {
            this.contactRepository = contactRepository;
            // TODO: this.Contact...Repository = Contact...Repository;
        }

		/// <summary>
		/// Retrieves all contacts.
		/// </summary>
		public async Task<IEnumerable<Contact>> GetAllContactsAsync()
        {
            return await this.contactRepository.GetAsync();
        }

		/// <summary>
		/// Retrieves one contact by Id.
		/// </summary>
		public async Task<Contact> GetContactByIdAsync(Guid id)
        {
            return await this.contactRepository.GetByIdAsync(id);
        }

		/// <summary>
		/// Creates a new contact record.
		/// </summary>
        public async Task<Contact> CreateContactAsync(Contact contact)
        {
            return await this.contactRepository.InsertAsync(contact);
        }

		/// <summary>
		/// Updates an existing contact record by Id.
		/// </summary>
        public async Task<Contact> UpdateContactAsync(Guid id, Contact contactUpdate)
        {
            // Retrieve existing
            Contact contact = await this.contactRepository.GetByIdAsync(id);
            if (contact == null)
            {
                return null;
            }

            // Mapping
            contact.FirstName = contactUpdate.FirstName;
            contact.LastName = contactUpdate.LastName;

            return await this.contactRepository.UpdateAsync(contact);
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
		/// Deletes an existing contact record by Id.
		/// </summary>
        public async Task<Contact> DeleteContactAsync(Contact contact)
        {
            await this.contactRepository.DeleteAsync(contact);

            return contact;
        }
    }
}