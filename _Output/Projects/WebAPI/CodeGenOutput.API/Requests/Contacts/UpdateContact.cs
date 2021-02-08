using AutoMapper;
using CodeGenOutput.API.BLL;
using CodeGenOutput.API.Models;
using CodeGenOutput.API.ViewModels;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CodeGenOutput.API.Requests.Contacts
{
    public class UpdateContact : IRequest<Response<ContactVM>>
    {
        public ContactVM ContactVM { get; set; }
    }

    public class UpdateContactHandler : IRequestHandler<UpdateContact, Response<ContactVM>>
    {
        private readonly IContactBLL _bll;
        private readonly IMapper _mapper;

        public UpdateContactHandler(IBusinessLogicLayer bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        public async Task<Response<ContactVM>> Handle(UpdateContact request, CancellationToken cancellationToken)
        {
            Response<ContactVM> response = new Response<ContactVM>();

            Contact contact = _mapper.Map<Contact>(request.ContactVM);
            contact = await _bll.UpdateContactAsync(contact);
            response.Message = "Contact updated";
            response.Data = _mapper.Map<ContactVM>(contact);

            return response;
        }
    }
}
