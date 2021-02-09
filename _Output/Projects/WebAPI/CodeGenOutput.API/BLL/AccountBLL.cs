using CodeGenOutput.API.DAL;
using CodeGenOutput.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeGenOutput.API.BLL
{
    public interface IAccountBLL
    {
        Task<IEnumerable<Account>> GetAccountsAsync(int skip, int take);
        Task<Account> GetAccountByCodeAsync(string code);
        // Task<IEnumerable<Account>> SearchAccountAsync(string term);
        Task<Account> CreateAccountAsync(Account account);
        Task<Account> UpdateAccountAsync(Account account);
        Task DeleteAccountAsync(string code);
    }

    public partial class BusinessLogicLayer : IAccountBLL
    {
        private readonly IRepository<Account> _accountRepository;

        public async Task<IEnumerable<Account>> GetAccountsAsync(int skip, int take)
        {
            return await _accountRepository.GetAsync(skip, take);
        }

        public async Task<Account> GetAccountByCodeAsync(string code)
        {
            Account account = await _accountRepository.GetByCodeAsync(code);
            if (account == null) {
                throw new Exception($"Account '{code}' not found.");
            }

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
            Account existingAccount = await GetAccountByCodeAsync(account.Code);
            account.DateCreated = existingAccount.DateCreated;

            Account updatedAccount = await _accountRepository.UpdateAsync(account);
            await _unitOfWork.Commit();
            return updatedAccount;
        }

        public async Task DeleteAccountAsync(string code)
        {
            await _accountRepository.DeleteAsync(code);
            await _unitOfWork.Commit();
        }
    }
}
