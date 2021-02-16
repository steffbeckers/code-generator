using AutoMapper;
using CodeGenOutput.API.BLL;
using CodeGenOutput.API.Models;
using CodeGenOutput.API.ViewModels;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeGenOutput.API.Requests.Accounts
{
    public class PatchAccount : IRequest<Response>
    {
        public Guid Id { get; set; }
        public JsonPatchDocument<AccountUpdateVM> PatchDocument { get; set; }
    }

    public class PatchAccountHandler : IRequestHandler<PatchAccount, Response>
    {
        private readonly IAccountBLL _bll;
        private readonly IMapper _mapper;

        public PatchAccountHandler(IBusinessLogicLayer bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        public async Task<Response> Handle(PatchAccount request, CancellationToken cancellationToken)
        {
            Account account = await _bll.GetAccountByIdAsync(request.Id);
            AccountUpdateVM accountUpdateVM = _mapper.Map<AccountUpdateVM>(account);
            request.PatchDocument.ApplyTo(accountUpdateVM);
            _mapper.Map(accountUpdateVM, account);
            account = await _bll.UpdateAccountAsync(account);

            return new Response()
            {
                Code = "ACCOUNT_UPDATED",
                Message = "Account updated",
                Data = _mapper.Map<AccountVM>(account)
            };
        }
    }
}
