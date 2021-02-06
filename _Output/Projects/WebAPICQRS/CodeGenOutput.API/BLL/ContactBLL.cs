using CodeGenOutput.API.DAL;
using CodeGenOutput.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeGenOutput.API.BLL
{
    public interface IContactBLL
    {
        Task<IEnumerable<Contact>> GetContactsAsync();
        Task<Contact> GetContactByIdAsync(Guid id);
        Task<IEnumerable<Contact>> SearchContactAsync(string term);
        Task<Contact> CreateContactAsync(Contact contact);
        Task<Contact> UpdateContactAsync(Contact contact);
        Task DeleteContactAsync(Contact contact);
    }

    public partial class BusinessLogicLayer : IContactBLL
    {
        private readonly IRepository<Contact> _contactRepository;

        public async Task<IEnumerable<Contact>> GetContactsAsync()
        {
            return await _contactRepository.GetAsync();
        }

        public async Task<Contact> GetContactByIdAsync(Guid id)
        {
            return await _contactRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Contact>> SearchContactAsync(string term)
        {
            return await _contactRepository.SearchContact(term);
        }

        public async Task<Contact> CreateContactAsync(Contact contact)
        {
            Contact createdContact = await _contactRepository.CreateAsync(contact);
            await _unitOfWork.Commit();
            return createdContact;
        }

        public async Task<Contact> UpdateContactAsync(Contact contact)
        {
            Contact updatedContact = await _contactRepository.UpdateAsync(contact);
            await _unitOfWork.Commit();
            return updatedContact;
        }

        public async Task DeleteContactAsync(Contact contact)
        {
            await _contactRepository.DeleteAsync(contact);
            await _unitOfWork.Commit();
        }
    }
}
