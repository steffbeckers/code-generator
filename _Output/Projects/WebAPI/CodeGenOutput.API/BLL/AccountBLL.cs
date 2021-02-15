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
        Task<IEnumerable<Account>> GetAccountsAsync(string include = "");
        Task<Account> GetAccountByIdAsync(Guid id, string include = "");
        Task<Account> CreateAccountAsync(Account account);
        Task<Account> UpdateAccountAsync(Account account);
        Task DeleteAccountAsync(Guid id);
    }

    public partial class BusinessLogicLayer : IAccountBLL
    {
        public async Task<IEnumerable<Account>> GetAccountsAsync(string include = "")
        {
            return await _unitOfWork.GetRepository<Account>().GetAsync(include: include);
        }

        public async Task<Account> GetAccountByIdAsync(Guid id, string include = "")
        {
            return await _unitOfWork.GetRepository<Account>().GetByIdAsync(id, include: include);
        }

        public async Task<Account> CreateAccountAsync(Account account)
        {
            Account createdAccount = await _unitOfWork.GetRepository<Account>().CreateAsync(account);
            await _unitOfWork.Commit();
            return createdAccount;
        }

        public async Task<Account> UpdateAccountAsync(Account account)
        {
            Account updatedAccount = await _unitOfWork.GetRepository<Account>().UpdateAsync(account);
            await _unitOfWork.Commit();
            return updatedAccount;
        }

        public async Task DeleteAccountAsync(Guid id)
        {
            await _unitOfWork.GetRepository<Account>().DeleteAsync(id);
            await _unitOfWork.Commit();
        }
    }
}
