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
			// #-#-# {6B392F7F-C4B3-4E64-8703-AE95C834E86A}
			// Before creation
			// #-#-#

			contact = await this.contactRepository.InsertAsync(contact);

			// #-#-# {086618AE-01D1-4162-8C4C-03080741C2CB}
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

			// #-#-# {573CD65B-4771-4335-85AC-74C5FB2E2AC8}
			// Before update mapping
			// #-#-#

            // Mapping
            contact.FirstName = contactUpdate.FirstName;
            contact.LastName = contactUpdate.LastName;
            contact.Website = contactUpdate.Website;
            contact.Telephone = contactUpdate.Telephone;
            contact.Email = contactUpdate.Email;
            contact.AccountId = contactUpdate.AccountId;

			// #-#-# {61904A6D-4EB9-47DF-B58E-8DDA26B0FB8C}
			// Before update
			// #-#-#

			contact = await this.contactRepository.UpdateAsync(contact);

			// #-#-# {0DB85255-BF4B-462A-A3E9-847F47A6C1F0}
			// After update
			// #-#-#

            return contact;
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
