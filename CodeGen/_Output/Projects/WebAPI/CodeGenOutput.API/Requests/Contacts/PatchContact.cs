using AutoMapper;
using CodeGenOutput.API.BLL;
using CodeGenOutput.API.DAL;
using CodeGenOutput.API.Models;
using CodeGenOutput.API.ViewModels;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeGenOutput.API.Requests.Contacts
{
    public class PatchContact : IRequest<Response>
    {
        public Guid Id { get; set; }
        public JsonPatchDocument<ContactUpdateVM> PatchDocument { get; set; }
    }

    public class PatchContactHandler : IRequestHandler<PatchContact, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public PatchContactHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IMediator mediator
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<Response> Handle(PatchContact request, CancellationToken cancellationToken)
        {
            IRepository<Contact> repository = _unitOfWork.GetRepository<Contact>();

            Contact contact = await repository.GetByIdAsync(request.Id);
            ContactUpdateVM contactUpdateVM = _mapper.Map<ContactUpdateVM>(contact);
            request.PatchDocument.ApplyTo(contactUpdateVM);

            return await _mediator.Send(new UpdateContact() { ContactUpdateVM = contactUpdateVM }, cancellationToken);
        }
    }
}
