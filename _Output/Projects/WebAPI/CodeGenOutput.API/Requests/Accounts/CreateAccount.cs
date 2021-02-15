using AutoMapper;
using CodeGenOutput.API.BLL;
using CodeGenOutput.API.Models;
using CodeGenOutput.API.ViewModels;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CodeGenOutput.API.Requests.Accounts
{
    public class CreateAccount : IRequest<Response>
    {
        public AccountCreateVM AccountCreateVM { get; set; }
    }

    public class CreateAccountHandler : IRequestHandler<CreateAccount, Response>
    {
        private readonly IAccountBLL _bll;
        private readonly IMapper _mapper;

        public CreateAccountHandler(IBusinessLogicLayer bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        public async Task<Response> Handle(CreateAccount request, CancellationToken cancellationToken)
        {
            Account account = _mapper.Map<Account>(request.AccountCreateVM);
            account = await _bll.CreateAccountAsync(account);

            return new Response()
            {
                Code = "ACCOUNT_CREATED",
                Message = "Account created",
                Data = _mapper.Map<AccountVM>(account)
            };
        }
    }
}
