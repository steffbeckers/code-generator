using CodeGenOutput.API.DAL;
using CodeGenOutput.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeGenOutput.API.BLL
{
    public interface IAccountBLL
    {
        Task<IEnumerable<Account>> GetAccountsAsync();
        Task<Account> GetAccountByIdAsync(Guid id);
        Task<IEnumerable<Account>> SearchAccountAsync(string term);
        Task<Account> CreateAccountAsync(Account account);
        Task<Account> UpdateAccountAsync(Account account);
        Task DeleteAccountAsync(Account account);
    }

    public partial class BusinessLogicLayer : IAccountBLL
    {
        private readonly IRepository<Account> _accountRepository;

        public async Task<IEnumerable<Account>> GetAccountsAsync()
        {
            return await _accountRepository.GetAsync();
        }

        public async Task<Account> GetAccountByIdAsync(Guid id)
        {
            return await _accountRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Account>> SearchAccountAsync(string term)
        {
            return await _accountRepository.SearchAccount(term);
        }

        public async Task<Account> CreateAccountAsync(Account account)
        {
            Account createdAccount = await _accountRepository.CreateAsync(account);
            await _unitOfWork.Commit();
            return createdAccount;
        }

        public async Task<Account> UpdateAccountAsync(Account account)
        {
            Account updatedAccount = await _accountRepository.UpdateAsync(account);
            await _unitOfWork.Commit();
            return updatedAccount;
        }

        public async Task DeleteAccountAsync(Account account)
        {
            await _accountRepository.DeleteAsync(account);
            await _unitOfWork.Commit();
        }
    }
}
