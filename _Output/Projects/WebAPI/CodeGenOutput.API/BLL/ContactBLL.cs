using CodeGenOutput.API.DAL;
using CodeGenOutput.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeGenOutput.API.BLL
{
    public interface IContactBLL
    {
        Task<IEnumerable<Contact>> GetContactsAsync(int skip, int take);
        Task<Contact> GetContactByCodeAsync(string code);
        // Task<IEnumerable<Contact>> SearchContactAsync(string term);
        Task<Contact> CreateContactAsync(Contact contact);
        Task<Contact> UpdateContactAsync(Contact contact);
        Task DeleteContactAsync(string code);
    }

    public partial class BusinessLogicLayer : IContactBLL
    {
        private readonly IRepository<Contact> _contactRepository;

        public async Task<IEnumerable<Contact>> GetContactsAsync(int skip, int take)
        {
            return await _contactRepository.GetAsync(skip, take);
        }

        public async Task<Contact> GetContactByCodeAsync(string code)
        {
            Contact contact = await _contactRepository.GetByCodeAsync(code);
            if (contact == null) {
                throw new Exception($"Contact '{code}' not found.");
            }

            return contact;
        }

        // public async Task<IEnumerable<Contact>> SearchContactAsync(string term)
        // {
        //     return await _contactRepository.SearchContact(term);
        // }

        public async Task<Contact> CreateContactAsync(Contact contact)
        {
            Contact createdContact = await _contactRepository.CreateAsync(contact);
            await _unitOfWork.Commit();
            return createdContact;
        }

        public async Task<Contact> UpdateContactAsync(Contact contact)
        {
            // Keep creating auditing details
            Contact existingContact = await GetContactByCodeAsync(contact.Code);
            contact.DateCreated = existingContact.DateCreated;

            Contact updatedContact = await _contactRepository.UpdateAsync(contact);
            await _unitOfWork.Commit();
            return updatedContact;
        }

        public async Task DeleteContactAsync(string code)
        {
            await _contactRepository.DeleteAsync(code);
            await _unitOfWork.Commit();
        }
    }
}
