using AutoMapper;
using CodeGenOutput.API.BLL;
using CodeGenOutput.Models;
using CodeGenOutput.ViewModels;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeGenOutput.API.Requests.Contacts
{
    public class GetContactById : IRequest<Response<ContactVM>>
    {
        public Guid Id { get; set; }
    }

    public class GetContactByIdHandler : IRequestHandler<GetContactById, Response<ContactVM>>
    {
        private readonly IContactBLL _bll;
        private readonly IMapper _mapper;

        public GetContactByIdHandler(IBusinessLogicLayer bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        public async Task<Response<ContactVM>> Handle(GetContactById request, CancellationToken cancellationToken)
        {
            Response<ContactVM> response = new Response<ContactVM>();

            Contact contact = await _bll.GetContactByIdAsync(request.Id);
            response.Data = _mapper.Map<ContactVM>(contact);

            return response;
        }
    }
}
