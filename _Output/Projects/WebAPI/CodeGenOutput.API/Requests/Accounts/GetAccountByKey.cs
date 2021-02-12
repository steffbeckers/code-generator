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
    public class GetAccountById : IRequest<Response>
    {
        public Guid Id { get; set; }
        public string Include { get; set; }
    }

    public class GetAccountByIdHandler : IRequestHandler<GetAccountById, Response>
    {
        private readonly IAccountBLL _bll;
        private readonly IMapper _mapper;

        public GetAccountByIdHandler(IBusinessLogicLayer bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        public async Task<Response> Handle(GetAccountById request, CancellationToken cancellationToken)
        {
            Account account = await _bll.GetAccountByIdAsync(request.Id, request.Include);
            if (account == null)
            {
                return new Response() { Success = false, Message = $"Account {request.Id} not found." };
            }

            return new Response() { Data = _mapper.Map<AccountVM>(account) };
        }
    }
}
