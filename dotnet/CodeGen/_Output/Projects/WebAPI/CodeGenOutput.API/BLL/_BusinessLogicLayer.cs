using CodeGenOutput.API.DAL;
using CodeGenOutput.Models;

namespace CodeGenOutput.API.BLL
{
    public interface IBusinessLogicLayer :
        ITestBLL,
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
            _testRepository = unitOfWork.GetRepository<Test>();
            _accountRepository = unitOfWork.GetRepository<Account>();
            _contactRepository = unitOfWork.GetRepository<Contact>();
        }
    }
}
