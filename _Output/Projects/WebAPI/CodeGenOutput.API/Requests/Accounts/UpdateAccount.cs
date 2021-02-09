using AutoMapper;
using CodeGenOutput.API.BLL;
using CodeGenOutput.API.Models;
using CodeGenOutput.API.ViewModels;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CodeGenOutput.API.Requests.Accounts
{
    public class UpdateAccount : IRequest<Response<AccountVM>>
    {
        public AccountUpdateVM AccountUpdateVM { get; set; }
    }

    public class UpdateAccountHandler : IRequestHandler<UpdateAccount, Response<AccountVM>>
    {
        private readonly IAccountBLL _bll;
        private readonly IMapper _mapper;

        public UpdateAccountHandler(IBusinessLogicLayer bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        public async Task<Response<AccountVM>> Handle(UpdateAccount request, CancellationToken cancellationToken)
        {
            Response<AccountVM> response = new Response<AccountVM>();

            Account account = _mapper.Map<Account>(request.AccountUpdateVM);

            try
            {
                account = await _bll.UpdateAccountAsync(account);

                response.Message = "Account updated";
                response.Data = _mapper.Map<AccountVM>(account);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            
            return response;
        }
    }
}
