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
			// #-#-# {83B8AA9F-713A-42FB-ADE1-8A4AA43886C8}
			// Before retrieval
			// #-#-#

            return await this.contactRepository.GetWithLinkedEntitiesAsync();
        }

		/// <summary>
		/// Retrieves one contact by Id.
		/// </summary>
		public async Task<Contact> GetContactByIdAsync(Guid id)
        {
			// #-#-# {F838CE2A-D0FB-4F8A-A826-0D653DEECB2B}
			// Before retrieval
			// #-#-#

            return await this.contactRepository.GetWithLinkedEntitiesByIdAsync(id);
        }

		/// <summary>
		/// Creates a new contact record.
		/// </summary>
        public async Task<Contact> CreateContactAsync(Contact contact)
        {
			// Trimming strings
            contact.FirstName = contact.FirstName.Trim();
            contact.LastName = contact.LastName.Trim();
            contact.Website = contact.Website.Trim();
            contact.Telephone = contact.Telephone.Trim();
            contact.Email = contact.Email.Trim();

			// #-#-# {D4775AF3-4BFA-496A-AA82-001028A22DD6}
			// Before creation
			// #-#-#

			contact = await this.contactRepository.InsertAsync(contact);

			// #-#-# {1972C619-D2F2-48FD-8474-3A69621B1F78}
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

			// Trimming strings
            contactUpdate.FirstName = contactUpdate.FirstName.Trim();
            contactUpdate.LastName = contactUpdate.LastName.Trim();
            contactUpdate.Website = contactUpdate.Website.Trim();
            contactUpdate.Telephone = contactUpdate.Telephone.Trim();
            contactUpdate.Email = contactUpdate.Email.Trim();

            // Mapping
            contact.FirstName = contactUpdate.FirstName;
            contact.LastName = contactUpdate.LastName;
            contact.Website = contactUpdate.Website;
            contact.Telephone = contactUpdate.Telephone;
            contact.Email = contactUpdate.Email;
            contact.AccountId = contactUpdate.AccountId;

			// #-#-# {B5914243-E57E-41AE-A7C8-553F2F93267B}
			// Before update
			// #-#-#

			contact = await this.contactRepository.UpdateAsync(contact);

			// #-#-# {983B1B6C-14A7-4925-8571-D77447DF0ADA}
			// After update
			// #-#-#

            return contact;
        }

		/// <summary>
		/// Deletes an existing contact record by Id.
		/// </summary>
        public async Task<Contact> DeleteContactAsync(Contact contact)
        {
			// #-#-# {FE1A99E0-482D-455B-A8C1-3C2C11FACA58}
			// Before deletion
			// #-#-#

            await this.contactRepository.DeleteAsync(contact);

			// #-#-# {F09857C0-44E7-4E6C-B3E6-883C0D28E1A6}
			// After deletion
			// #-#-#

            return contact;
        }
    }
}
