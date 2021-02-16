using AutoMapper;
using CodeGenOutput.API.BLL;
using CodeGenOutput.API.Models;
using CodeGenOutput.API.ViewModels;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CodeGenOutput.API.Requests.Accounts
{
    public class GetAccounts : IRequest<Response>
    {
        public string Include { get; set; }
    }

    public class GetAccountsHandler : IRequestHandler<GetAccounts, Response>
    {
        private readonly IAccountBLL _bll;
        private readonly IMapper _mapper;

        public GetAccountsHandler(IBusinessLogicLayer bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        public async Task<Response> Handle(GetAccounts request, CancellationToken cancellationToken)
        {
            List<Account> accounts = (await _bll.GetAccountsAsync(request.Include)).ToList();

            return new Response() { Data = _mapper.Map<List<AccountListVM>>(accounts) };
        }
    }
}
