using AutoMapper;
using CodeGenOutput.API.BLL;
using CodeGenOutput.Models;
using CodeGenOutput.ViewModels;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeGenOutput.API.Requests.Accounts
{
    public class GetAccountById : IRequest<Response<AccountVM>>
    {
        public Guid Id { get; set; }
    }

    public class GetAccountByIdHandler : IRequestHandler<GetAccountById, Response<AccountVM>>
    {
        private readonly IAccountBLL _bll;
        private readonly IMapper _mapper;

        public GetAccountByIdHandler(IBusinessLogicLayer bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        public async Task<Response<AccountVM>> Handle(GetAccountById request, CancellationToken cancellationToken)
        {
            Response<AccountVM> response = new Response<AccountVM>();

            Account account = await _bll.GetAccountByIdAsync(request.Id);
            response.Data = _mapper.Map<AccountVM>(account);

            return response;
        }
    }
}
