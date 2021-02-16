using AutoMapper;
using CodeGenOutput.API.BLL;
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
        private readonly IContactBLL _bll;
        private readonly IMapper _mapper;

        public PatchContactHandler(IBusinessLogicLayer bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        public async Task<Response> Handle(PatchContact request, CancellationToken cancellationToken)
        {
            Contact contact = await _bll.GetContactByIdAsync(request.Id);
            ContactUpdateVM contactUpdateVM = _mapper.Map<ContactUpdateVM>(contact);
            request.PatchDocument.ApplyTo(contactUpdateVM);
            _mapper.Map(contactUpdateVM, contact);
            contact = await _bll.UpdateContactAsync(contact);

            return new Response()
            {
                Code = "CONTACT_UPDATED",
                Message = "Contact updated",
                Data = _mapper.Map<ContactVM>(contact)
            };
        }
    }
}
