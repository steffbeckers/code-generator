using AutoMapper;
using CodeGenOutput.API.BLL;
using CodeGenOutput.Models;
using CodeGenOutput.ViewModels;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CodeGenOutput.API.Requests.Accounts
{
    public class CreateAccount : IRequest<Response<AccountVM>>
    {
        public AccountCreateVM AccountCreateVM { get; set; }
    }

    public class CreateAccountHandler : IRequestHandler<CreateAccount, Response<AccountVM>>
    {
        private readonly IAccountBLL _bll;
        private readonly IMapper _mapper;

        public CreateAccountHandler(IBusinessLogicLayer bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        public async Task<Response<AccountVM>> Handle(CreateAccount request, CancellationToken cancellationToken)
        {
            Response<AccountVM> response = new Response<AccountVM>();

            Account account = _mapper.Map<Account>(request.AccountCreateVM);
            account = await _bll.CreateAccountAsync(account);
            response.Message = "Account created";
            response.Data = _mapper.Map<AccountVM>(account);

            return response;
        }
    }
}
