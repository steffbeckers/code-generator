using AutoMapper;
using CodeGenOutput.API.BLL;
using CodeGenOutput.Models;
using CodeGenOutput.ViewModels;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CodeGenOutput.API.Requests.Contacts
{
    public class GetContacts : IRequest<Response<List<ContactListVM>>>
    {
        public int Skip { get; set; }
        public int Take { get; set; }
    }

    public class GetContactsHandler : IRequestHandler<GetContacts, Response<List<ContactListVM>>>
    {
        private readonly IContactBLL _bll;
        private readonly IMapper _mapper;

        public GetContactsHandler(IBusinessLogicLayer bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        public async Task<Response<List<ContactListVM>>> Handle(GetContacts request, CancellationToken cancellationToken)
        {
            Response<List<ContactListVM>> response = new Response<List<ContactListVM>>();

            List<Contact> contacts = (await _bll.GetContactsAsync(request.Skip, request.Take)).ToList();
            response.Data = _mapper.Map<List<ContactListVM>>(contacts);

            return response;
        }
    }
}
