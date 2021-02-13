using CodeGenOutput.API.DAL;
using CodeGenOutput.API.DAL.Repositories;
using CodeGenOutput.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeGenOutput.API.BLL
{
    public interface IAccountBLL
    {
        Task<IEnumerable<Account>> GetAccountsAsync(string include);
        Task<Account> GetAccountByIdAsync(Guid id, string include);
        Task<Account> CreateAccountAsync(Account account);
        Task<Account> UpdateAccountAsync(Account account);
        Task DeleteAccountAsync(Guid id);
    }

    public partial class BusinessLogicLayer : IAccountBLL
    {
        private readonly IRepository<Account> _accountRepository;

        public async Task<IEnumerable<Account>> GetAccountsAsync(string include = "")
        {
            return await _accountRepository.GetAsync(include: include);
        }

        public async Task<Account> GetAccountByIdAsync(Guid id, string include = "")
        {
            return await _accountRepository.GetByIdAsync(id, include: include);
        }

        public async Task<Account> CreateAccountAsync(Account account)
        {
            Account createdAccount = await _accountRepository.CreateAsync(account);
            await _unitOfWork.Commit();
            return createdAccount;
        }

        public async Task<Account> UpdateAccountAsync(Account account)
        {
            // Keep creating auditing details
            Account existingAccount = await GetAccountByIdAsync(account.Id);
            account.DateCreated = existingAccount.DateCreated;

            Account updatedAccount = await _accountRepository.UpdateAsync(account);
            await _unitOfWork.Commit();
            return updatedAccount;
        }

        public async Task DeleteAccountAsync(Guid id)
        {
            await _accountRepository.DeleteAsync(id);
            await _unitOfWork.Commit();
        }
    }
}
