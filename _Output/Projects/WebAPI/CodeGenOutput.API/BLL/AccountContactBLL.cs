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
    public interface IAccountContactBLL
    {
        Task<IEnumerable<AccountContact>> GetAccountContactsAsync(string include = "");
        Task<AccountContact> GetAccountContactByIdAsync(Guid id, string include = "");
        Task<AccountContact> CreateAccountContactAsync(AccountContact accountcontact);
        Task<AccountContact> UpdateAccountContactAsync(AccountContact accountcontact);
        Task DeleteAccountContactAsync(Guid id);
    }

    public partial class BusinessLogicLayer : IAccountContactBLL
    {
        public async Task<IEnumerable<AccountContact>> GetAccountContactsAsync(string include = "")
        {
            return await _unitOfWork.GetRepository<AccountContact>().GetAsync(include: include);
        }

        public async Task<AccountContact> GetAccountContactByIdAsync(Guid id, string include = "")
        {
            return await _unitOfWork.GetRepository<AccountContact>().GetByIdAsync(id, include: include);
        }

        public async Task<AccountContact> CreateAccountContactAsync(AccountContact accountcontact)
        {
            await ValidateAccountContactAsync(accountcontact);
            AccountContact createdAccountContact = await _unitOfWork.GetRepository<AccountContact>().CreateAsync(accountcontact);
            await _unitOfWork.Commit();
            
            return createdAccountContact;
        }

        public async Task<AccountContact> UpdateAccountContactAsync(AccountContact accountcontact)
        {
            await ValidateAccountContactAsync(accountcontact);
            AccountContact updatedAccountContact = await _unitOfWork.GetRepository<AccountContact>().UpdateAsync(accountcontact);
            await _unitOfWork.Commit();
            
            return updatedAccountContact;
        }

        public async Task DeleteAccountContactAsync(Guid id)
        {
            await _unitOfWork.GetRepository<AccountContact>().DeleteAsync(id);
            await _unitOfWork.Commit();
        }

        private async Task ValidateAccountContactAsync(AccountContact accountcontact)
        {
            AccountContactValidator validator = new AccountContactValidator();
            ValidationResult validationResult = await validator.ValidateAsync(accountcontact);
            if (!validationResult.IsValid) { throw new ValidationException(validationResult.Errors); }
        }
    }
}
