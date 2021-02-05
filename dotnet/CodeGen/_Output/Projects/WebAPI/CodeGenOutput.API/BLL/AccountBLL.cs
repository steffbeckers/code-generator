using CodeGenOutput.API.DAL.Repositories;
using CodeGenOutput.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeGenOutput.API.DAL;

namespace CodeGenOutput.API.BLL
{
    public interface IAccountBLL {
        Task<List<Account>> GetAccountsAsync();
        Task<Account> GetAccountByIdAsync(Guid id);
        Task<Account> CreateAccountAsync(Account account);
        Task<Account> UpdateAccountAsync(Account account);
        Task DeleteAccountAsync(Account account);
    }

    public class AccountBLL : BusinessLogicLayer, IAccountBLL
    {
        private readonly AccountRepository _accountRepository;

        public AccountBLL(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _accountRepository = (AccountRepository)unitOfWork.GetRepository<Account>();
        }

        public async Task<List<Account>> GetAccountsAsync()
        {
            return (List<Account>) await accountRepository.GetAsync();
        }

        public async Task<Account> GetAccountByIdAsync(Guid id)
        {
            
            return await accountRepository.GetByIdAsync(id);
        }

        public async Task<Account> CreateAccountAsync(Account account)
        {
            AccountRepository accountRepository = (AccountRepository)_unitOfWork.GetRepository<Account>();
            Account createdAccount = await accountRepository.CreateAsync(account);
            return createdAccount;
        }

        public async Task<Account> UpdateAccountAsync(Account account)
        {
            AccountRepository accountRepository = (AccountRepository)_unitOfWork.GetRepository<Account>();
            Account updatedAccount = await accountRepository.UpdateAsync(account);
            return updatedAccount;
        }

        public async Task DeleteAccountAsync(Account account)
        {
            AccountRepository accountRepository = (AccountRepository)_unitOfWork.GetRepository<Account>();
            await accountRepository.DeleteAsync(account);
        }
    }
}
