using AutoMapper;
using CodeGenOutput.API.BLL;
using CodeGenOutput.API.Models;
using CodeGenOutput.API.ViewModels;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CodeGenOutput.API.Requests.AccountContacts
{
    public class GetAccountContacts : IRequest<Response>
    {
        public string Include { get; set; }
    }

    public class GetAccountContactsHandler : IRequestHandler<GetAccountContacts, Response>
    {
        private readonly IAccountContactBLL _bll;
        private readonly IMapper _mapper;

        public GetAccountContactsHandler(IBusinessLogicLayer bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        public async Task<Response> Handle(GetAccountContacts request, CancellationToken cancellationToken)
        {
            List<AccountContact> accountcontacts = (await _bll.GetAccountContactsAsync(request.Include)).ToList();

            return new Response() { Data = _mapper.Map<List<AccountContactListVM>>(accountcontacts) };
        }
    }
}
