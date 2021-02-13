using CodeGenOutput.API.DAL;
using CodeGenOutput.API.Models;

namespace CodeGenOutput.API.BLL
{
    public interface IBusinessLogicLayer :
        IAccountBLL,
        IAccountContactBLL,
        IAddressBLL,
        IContactBLL
    { }

    public partial class BusinessLogicLayer : IBusinessLogicLayer
    {
        private readonly IUnitOfWork _unitOfWork;

        public BusinessLogicLayer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            // Repositories
            _accountRepository = unitOfWork.GetRepository<Account>();
            _accountcontactRepository = unitOfWork.GetRepository<AccountContact>();
            _addressRepository = unitOfWork.GetRepository<Address>();
            _contactRepository = unitOfWork.GetRepository<Contact>();
        }
    }
}
