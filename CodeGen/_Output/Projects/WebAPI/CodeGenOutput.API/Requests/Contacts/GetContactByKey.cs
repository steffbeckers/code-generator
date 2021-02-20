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
    public class GetContactById : IRequest<Response>
    {
        public Guid Id { get; set; }
        public string Include { get; set; }
    }

    public class GetContactByIdHandler : IRequestHandler<GetContactById, Response>
    {
        private readonly IContactBLL _bll;
        private readonly IMapper _mapper;

        public GetContactByIdHandler(IBusinessLogicLayer bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        public async Task<Response> Handle(GetContactById request, CancellationToken cancellationToken)
        {
            Contact contact = await _bll.GetContactByIdAsync(request.Id, request.Include);
            if (contact == null)
            {
                return new Response() { Success = false, Message = $"Contact {request.Id} not found." };
            }

            return new Response() { Data = _mapper.Map<ContactVM>(contact) };
        }
    }
}
