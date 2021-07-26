using AutoMapper;
using CodeGenOutput.API.DAL;
using CodeGenOutput.API.DAL.Repositories;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetContactByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response> Handle(GetContactById request, CancellationToken cancellationToken)
        {
            IRepository<Contact> repository = _unitOfWork.GetRepository<Contact>();

            Contact contact = await repository.GetByIdAsync(request.Id, request.Include);
            if (contact == null)
            {
                return new Response() {
                    Success = false,
                    Code = "CONTACT_NOT_FOUND",
                    Message = $"Contact {request.Id} not found."
                };
            }

            return new Response() { Data = _mapper.Map<ContactVM>(contact) };
        }
    }
}
