using CodeGenOutput.API.DAL;
using CodeGenOutput.API.DAL.Repositories;
using CodeGenOutput.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeGenOutput.API.BLL
{
    public interface IAccountContactBLL
    {
        Task<IEnumerable<AccountContact>> GetAccountContactsAsync(string include);
        Task<AccountContact> GetAccountContactByIdAsync(Guid id, string include);
        Task<AccountContact> CreateAccountContactAsync(AccountContact accountcontact);
        Task<AccountContact> UpdateAccountContactAsync(AccountContact accountcontact);
        Task DeleteAccountContactAsync(Guid id);
    }

    public partial class BusinessLogicLayer : IAccountContactBLL
    {
        private readonly IRepository<AccountContact> _accountcontactRepository;

        public async Task<IEnumerable<AccountContact>> GetAccountContactsAsync(string include = "")
        {
            return await _accountcontactRepository.GetAsync(include: include);
        }

        public async Task<AccountContact> GetAccountContactByIdAsync(Guid id, string include = "")
        {
            return await _accountcontactRepository.GetByIdAsync(id, include: include);
        }

        public async Task<AccountContact> CreateAccountContactAsync(AccountContact accountcontact)
        {
            AccountContact createdAccountContact = await _accountcontactRepository.CreateAsync(accountcontact);
            await _unitOfWork.Commit();
            return createdAccountContact;
        }

        public async Task<AccountContact> UpdateAccountContactAsync(AccountContact accountcontact)
        {
            // Keep creating auditing details
            AccountContact existingAccountContact = await GetAccountContactByIdAsync(accountcontact.Id);
            accountcontact.DateCreated = existingAccountContact.DateCreated;

            AccountContact updatedAccountContact = await _accountcontactRepository.UpdateAsync(accountcontact);
            await _unitOfWork.Commit();
            return updatedAccountContact;
        }

        public async Task DeleteAccountContactAsync(Guid id)
        {
            await _accountcontactRepository.DeleteAsync(id);
            await _unitOfWork.Commit();
        }
    }
}
