using CodeGenOutput.API.DAL;
using CodeGenOutput.API.DAL.Repositories;
using CodeGenOutput.API.Models;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ValidationException = CodeGenOutput.API.Validation.ValidationException;

namespace CodeGenOutput.API.BLL
{
    public interface IContactBLL
    {
        Task<IEnumerable<Contact>> GetContactsAsync(string include = "");
        Task<Contact> GetContactByIdAsync(Guid id, string include = "");
        Task<Contact> CreateContactAsync(Contact contact);
        Task<Contact> UpdateContactAsync(Contact contact);
        Task DeleteContactAsync(Guid id);
    }

    public partial class BusinessLogicLayer : IContactBLL
    {
        public async Task<IEnumerable<Contact>> GetContactsAsync(string include = "")
        {
            return await _unitOfWork.GetRepository<Contact>().GetAsync(include: include);
        }

        public async Task<Contact> GetContactByIdAsync(Guid id, string include = "")
        {
            return await _unitOfWork.GetRepository<Contact>().GetByIdAsync(id, include: include);
        }

        public async Task<Contact> CreateContactAsync(Contact contact)
        {
            await ValidateContactAsync(contact);
            Contact createdContact = await _unitOfWork.GetRepository<Contact>().CreateAsync(contact);
            await _unitOfWork.Commit();
            
            return createdContact;
        }

        public async Task<Contact> UpdateContactAsync(Contact contact)
        {
            await ValidateContactAsync(contact);
            Contact updatedContact = await _unitOfWork.GetRepository<Contact>().UpdateAsync(contact);
            await _unitOfWork.Commit();
            
            return updatedContact;
        }

        public async Task DeleteContactAsync(Guid id)
        {
            await _unitOfWork.GetRepository<Contact>().DeleteAsync(id);
            await _unitOfWork.Commit();
        }

        private async Task ValidateContactAsync(Contact contact)
        {
            ContactValidator validator = new ContactValidator();
            ValidationResult validationResult = await validator.ValidateAsync(contact);
            if (!validationResult.IsValid) { throw new ValidationException(validationResult.Errors); }
        }
    }
}
