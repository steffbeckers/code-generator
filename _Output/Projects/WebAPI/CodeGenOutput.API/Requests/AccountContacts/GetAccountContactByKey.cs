using AutoMapper;
using CodeGenOutput.API.BLL;
using CodeGenOutput.API.Models;
using CodeGenOutput.API.ViewModels;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeGenOutput.API.Requests.AccountContacts
{
    public class GetAccountContactById : IRequest<Response>
    {
        public Guid Id { get; set; }
        public string Include { get; set; }
    }

    public class GetAccountContactByIdHandler : IRequestHandler<GetAccountContactById, Response>
    {
        private readonly IAccountContactBLL _bll;
        private readonly IMapper _mapper;

        public GetAccountContactByIdHandler(IBusinessLogicLayer bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        public async Task<Response> Handle(GetAccountContactById request, CancellationToken cancellationToken)
        {
            AccountContact accountcontact = await _bll.GetAccountContactByIdAsync(request.Id, request.Include);
            if (accountcontact == null)
            {
                return new Response() { Success = false, Message = $"AccountContact {request.Id} not found." };
            }

            return new Response() { Data = _mapper.Map<AccountContactVM>(accountcontact) };
        }
    }
}
