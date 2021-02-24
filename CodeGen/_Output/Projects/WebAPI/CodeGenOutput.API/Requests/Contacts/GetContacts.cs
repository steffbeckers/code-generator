using AutoMapper;
using CodeGenOutput.API.BLL;
using CodeGenOutput.API.Models;
using CodeGenOutput.API.ViewModels;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CodeGenOutput.API.Requests.Contacts
{
    public class GetContacts : IRequest<Response>
    {
        public string Include { get; set; }
    }

    public class GetContactsHandler : IRequestHandler<GetContacts, Response>
    {
        private readonly IContactBLL _bll;
        private readonly IMapper _mapper;

        public GetContactsHandler(IBusinessLogicLayer bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        public async Task<Response> Handle(GetContacts request, CancellationToken cancellationToken)
        {
            List<Contact> contacts = (await _bll.GetContactsAsync(request.Include)).ToList();

            return new Response() { Data = _mapper.Map<List<ContactListVM>>(contacts) };
        }
    }
}
