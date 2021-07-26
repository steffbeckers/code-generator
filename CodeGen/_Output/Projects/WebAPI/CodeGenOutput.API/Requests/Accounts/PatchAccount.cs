using AutoMapper;
using CodeGenOutput.API.DAL;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public PatchAccountHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IMediator mediator
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<Response> Handle(PatchAccount request, CancellationToken cancellationToken)
        {
            IRepository<Account> repository = _unitOfWork.GetRepository<Account>();

            Account account = await repository.GetByIdAsync(request.Id);
            AccountUpdateVM accountUpdateVM = _mapper.Map<AccountUpdateVM>(account);
            request.PatchDocument.ApplyTo(accountUpdateVM);

            return await _mediator.Send(new UpdateAccount() { AccountUpdateVM = accountUpdateVM }, cancellationToken);
        }
    }
}
