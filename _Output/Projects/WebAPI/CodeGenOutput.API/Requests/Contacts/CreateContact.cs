using AutoMapper;
using CodeGenOutput.API.BLL;
using CodeGenOutput.API.Models;
using CodeGenOutput.API.ViewModels;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CodeGenOutput.API.Requests.Contacts
{
    public class CreateContact : IRequest<Response<ContactVM>>
    {
        public ContactCreateVM ContactCreateVM { get; set; }
    }

    public class CreateContactHandler : IRequestHandler<CreateContact, Response<ContactVM>>
    {
        private readonly IContactBLL _bll;
        private readonly IMapper _mapper;

        public CreateContactHandler(IBusinessLogicLayer bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        public async Task<Response<ContactVM>> Handle(CreateContact request, CancellationToken cancellationToken)
        {
            Response<ContactVM> response = new Response<ContactVM>();

            Contact contact = _mapper.Map<Contact>(request.ContactCreateVM);
            contact = await _bll.CreateContactAsync(contact);
            response.Message = "Contact created";
            response.Data = _mapper.Map<ContactVM>(contact);

            return response;
        }
    }
}
