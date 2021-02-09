using AutoMapper;
using CodeGenOutput.API.BLL;
using CodeGenOutput.API.Models;
using CodeGenOutput.API.ViewModels;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeGenOutput.API.Requests.Accounts
{
    public class GetAccountByCode : IRequest<Response<AccountVM>>
    {
        public string Code { get; set; }
    }

    public class GetAccountByCodeHandler : IRequestHandler<GetAccountByCode, Response<AccountVM>>
    {
        private readonly IAccountBLL _bll;
        private readonly IMapper _mapper;

        public GetAccountByCodeHandler(IBusinessLogicLayer bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        public async Task<Response<AccountVM>> Handle(GetAccountByCode request, CancellationToken cancellationToken)
        {
            Response<AccountVM> response = new Response<AccountVM>();

            Account account = await _bll.GetAccountByCodeAsync(request.Code);
            response.Data = _mapper.Map<AccountVM>(account);

            return response;
        }
    }
}
