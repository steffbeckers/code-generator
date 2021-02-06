using CodeGenOutput.API.DAL;
using CodeGenOutput.Models;

namespace CodeGenOutput.API.BLL
{
    public interface IBusinessLogicLayer :
        IAccountBLL,
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
            _contactRepository = unitOfWork.GetRepository<Contact>();
        }
    }
}
