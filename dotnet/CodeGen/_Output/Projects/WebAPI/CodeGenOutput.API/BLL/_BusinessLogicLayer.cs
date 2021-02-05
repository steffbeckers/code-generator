using CodeGenOutput.API.DAL;
using CodeGenOutput.API.DAL.Repositories;
using CodeGenOutput.Models;
using System;

namespace CodeGenOutput.API.BLL
{
    public interface IBusinessLogicLayer :
        IAccountBLL
    {}

    public partial class BusinessLogicLayer : IBusinessLogicLayer
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AccountRepository _accountRepository;

        public BusinessLogicLayer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _accountRepository = (AccountRepository)unitOfWork.GetRepository<Account>();
        }
    }
}
