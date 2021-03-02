using AutoMapper;
using CodeGenOutput.API.BLL;
using CodeGenOutput.API.DAL;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetContactsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response> Handle(GetContacts request, CancellationToken cancellationToken)
        {
            IRepository<Contact> repository = _unitOfWork.GetRepository<Contact>();

            IEnumerable<Contact> contacts = await repository.GetAsync(include: request.Include);

            return new Response() { Data = _mapper.Map<List<ContactListVM>>(contacts).ToList() };
        }
    }
}
