using AutoMapper;
using CodeGenOutput.API.BLL;
using CodeGenOutput.API.Models;
using CodeGenOutput.API.ViewModels;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

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
            Contact contact = _mapper.Map<Contact>(request.ContactUpdateVM);
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
