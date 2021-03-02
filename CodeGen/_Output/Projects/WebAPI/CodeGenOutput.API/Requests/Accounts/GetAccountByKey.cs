using AutoMapper;
using CodeGenOutput.API.BLL;
using CodeGenOutput.API.DAL;
using CodeGenOutput.API.DAL.Repositories;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAccountByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response> Handle(GetAccountById request, CancellationToken cancellationToken)
        {
            IRepository<Account> repository = _unitOfWork.GetRepository<Account>();

            Account account = await repository.GetByIdAsync(request.Id, request.Include);
            if (account == null)
            {
                return new Response() {
                    Success = false,
                    Code = "ACCOUNT_NOT_FOUND",
                    Message = $"Account {request.Id} not found."
                };
            }

            return new Response() { Data = _mapper.Map<AccountVM>(account) };
        }
    }
}
