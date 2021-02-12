using CodeGenOutput.API.DAL;
using CodeGenOutput.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeGenOutput.API.BLL
{
    public interface IAccountBLL
    {
        Task<IEnumerable<Account>> GetAccountsAsync();
        Task<Account> GetAccountByIdAsync(Guid id);
        // Task<IEnumerable<Account>> SearchAccountAsync(string term);
        Task<Account> CreateAccountAsync(Account account);
        Task<Account> UpdateAccountAsync(Account account);
        Task DeleteAccountAsync(Guid id);
    }

    public partial class BusinessLogicLayer : IAccountBLL
    {
        private readonly IRepository<Account> _accountRepository;

        public async Task<IEnumerable<Account>> GetAccountsAsync()
        {
            return await _accountRepository.GetAllAsync();
        }

        public async Task<Account> GetAccountByIdAsync(Guid id)
        {
            Account account = await _accountRepository.GetByIdAsync(id);
            return account;
        }

        // public async Task<IEnumerable<Account>> SearchAccountAsync(string term)
        // {
        //     return await _accountRepository.SearchAccount(term);
        // }

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
