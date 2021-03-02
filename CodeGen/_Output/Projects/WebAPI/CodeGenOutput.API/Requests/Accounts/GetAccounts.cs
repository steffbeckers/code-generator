using AutoMapper;
using CodeGenOutput.API.BLL;
using CodeGenOutput.API.DAL;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAccountsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response> Handle(GetAccounts request, CancellationToken cancellationToken)
        {
            IRepository<Account> repository = _unitOfWork.GetRepository<Account>();

            IEnumerable<Account> accounts = await repository.GetAsync(include: request.Include);

            return new Response() { Data = _mapper.Map<List<AccountListVM>>(accounts).ToList() };
        }
    }
}
