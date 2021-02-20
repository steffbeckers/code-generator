using CodeGen.API.DAL;

namespace CodeGen.API.BLL
{
    public interface IBusinessLogicLayer :
        IProjectBLL
    { }

    public partial class BusinessLogicLayer : IBusinessLogicLayer
    {
        private readonly IUnitOfWork _unitOfWork;

        public BusinessLogicLayer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
