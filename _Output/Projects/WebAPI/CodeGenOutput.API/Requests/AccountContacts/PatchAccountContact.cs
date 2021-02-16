using AutoMapper;
using CodeGenOutput.API.BLL;
using CodeGenOutput.API.Models;
using CodeGenOutput.API.ViewModels;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeGenOutput.API.Requests.AccountContacts
{
    public class PatchAccountContact : IRequest<Response>
    {
        public Guid Id { get; set; }
        public JsonPatchDocument<AccountContactUpdateVM> PatchDocument { get; set; }
    }

    public class PatchAccountContactHandler : IRequestHandler<PatchAccountContact, Response>
    {
        private readonly IAccountContactBLL _bll;
        private readonly IMapper _mapper;

        public PatchAccountContactHandler(IBusinessLogicLayer bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        public async Task<Response> Handle(PatchAccountContact request, CancellationToken cancellationToken)
        {
            AccountContact accountcontact = await _bll.GetAccountContactByIdAsync(request.Id);
            AccountContactUpdateVM accountcontactUpdateVM = _mapper.Map<AccountContactUpdateVM>(accountcontact);
            request.PatchDocument.ApplyTo(accountcontactUpdateVM);
            _mapper.Map(accountcontactUpdateVM, accountcontact);
            accountcontact = await _bll.UpdateAccountContactAsync(accountcontact);

            return new Response()
            {
                Code = "ACCOUNTCONTACT_UPDATED",
                Message = "AccountContact updated",
                Data = _mapper.Map<AccountContactVM>(accountcontact)
            };
        }
    }
}
