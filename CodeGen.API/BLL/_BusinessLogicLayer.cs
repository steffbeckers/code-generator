using CodeGen.API.DAL;
using CodeGen.API.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace CodeGen.API.BLL
{
    public interface IBusinessLogicLayer :
        IProjectBLL
    { }

    public partial class BusinessLogicLayer : IBusinessLogicLayer
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHubContext<RealtimeHub> _realtimeHub;

        public BusinessLogicLayer(
            IUnitOfWork unitOfWork,
            IHubContext<RealtimeHub> realtimeHub
        )
        {
            _unitOfWork = unitOfWork;
            _realtimeHub = realtimeHub;
        }
    }
}
