using AutoMapper;
using CodeGenOutput.API.BLL;
using CodeGenOutput.API.Models;
using CodeGenOutput.API.ViewModels;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ValidationException = CodeGenOutput.API.Validation.ValidationException;

namespace CodeGenOutput.API.Requests.Contacts
{
    public class UpdateContact : IRequest<Response>
    {
        public ContactUpdateVM ContactUpdateVM { get; set; }
    }

    public class UpdateContactHandler : IRequestHandler<UpdateContact, Response>
    {
        private readonly IContactBLL _bll;
        private readonly IMapper _mapper;

        public UpdateContactHandler(IBusinessLogicLayer bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        public async Task<Response> Handle(UpdateContact request, CancellationToken cancellationToken)
        {
            Contact contact = await _bll.GetContactByIdAsync(request.ContactUpdateVM.Id);
            _mapper.Map(request.ContactUpdateVM, contact);
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
