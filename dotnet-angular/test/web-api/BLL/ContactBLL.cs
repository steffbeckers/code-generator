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

		/// <summary>
		/// The constructor of the Contact business logic layer.
		/// </summary>
        public ContactBLL(
			ContactRepository contactRepository
		)
        {
            this.contactRepository = contactRepository;
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
            return await this.contactRepository.GetWithLinkedEntitiesByIdAsync(id);
        }

		/// <summary>
		/// Creates a new contact record.
		/// </summary>
        public async Task<Contact> CreateContactAsync(Contact contact)
        {
			// #-#-#  
			// Before creation
			// #-#-#

			contact = await this.contactRepository.InsertAsync(contact);

			// #-#-#  
			// After creation
			// #-#-#

            return contact;
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
            contact.Website = contactUpdate.Website;
            contact.Telephone = contactUpdate.Telephone;
            contact.Email = contactUpdate.Email;
            contact.AccountId = contactUpdate.AccountId;

            return await this.contactRepository.UpdateAsync(contact);
        }

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
