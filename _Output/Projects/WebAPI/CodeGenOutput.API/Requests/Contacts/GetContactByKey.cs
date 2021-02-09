using AutoMapper;
using CodeGenOutput.API.BLL;
using CodeGenOutput.API.Models;
using CodeGenOutput.API.ViewModels;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeGenOutput.API.Requests.Contacts
{
    public class GetContactByCode : IRequest<Response<ContactVM>>
    {
        public string Code { get; set; }
    }

    public class GetContactByCodeHandler : IRequestHandler<GetContactByCode, Response<ContactVM>>
    {
        private readonly IContactBLL _bll;
        private readonly IMapper _mapper;

        public GetContactByCodeHandler(IBusinessLogicLayer bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        public async Task<Response<ContactVM>> Handle(GetContactByCode request, CancellationToken cancellationToken)
        {
            Response<ContactVM> response = new Response<ContactVM>();

            Contact contact = await _bll.GetContactByCodeAsync(request.Code);
            response.Data = _mapper.Map<ContactVM>(contact);

            return response;
        }
    }
}
