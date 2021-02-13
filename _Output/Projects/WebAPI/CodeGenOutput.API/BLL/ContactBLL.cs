using CodeGenOutput.API.DAL;
using CodeGenOutput.API.DAL.Repositories;
using CodeGenOutput.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeGenOutput.API.BLL
{
    public interface IContactBLL
    {
        Task<IEnumerable<Contact>> GetContactsAsync(string include);
        Task<Contact> GetContactByIdAsync(Guid id, string include);
        Task<Contact> CreateContactAsync(Contact contact);
        Task<Contact> UpdateContactAsync(Contact contact);
        Task DeleteContactAsync(Guid id);
    }

    public partial class BusinessLogicLayer : IContactBLL
    {
        private readonly IRepository<Contact> _contactRepository;

        public async Task<IEnumerable<Contact>> GetContactsAsync(string include = "")
        {
            return await _contactRepository.GetAsync(include: include);
        }

        public async Task<Contact> GetContactByIdAsync(Guid id, string include = "")
        {
            return await _contactRepository.GetByIdAsync(id, include: include);
        }

        public async Task<Contact> CreateContactAsync(Contact contact)
        {
            Contact createdContact = await _contactRepository.CreateAsync(contact);
            await _unitOfWork.Commit();
            return createdContact;
        }

        public async Task<Contact> UpdateContactAsync(Contact contact)
        {
            // Keep creating auditing details
            Contact existingContact = await GetContactByIdAsync(contact.Id);
            contact.DateCreated = existingContact.DateCreated;

            Contact updatedContact = await _contactRepository.UpdateAsync(contact);
            await _unitOfWork.Commit();
            return updatedContact;
        }

        public async Task DeleteContactAsync(Guid id)
        {
            await _contactRepository.DeleteAsync(id);
            await _unitOfWork.Commit();
        }
    }
}
