using AutoMapper;
using CodeGenOutput.API.BLL;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeGenOutput.API.Requests.Contacts
{
    public class DeleteContact : IRequest<Response>
    {
        public Guid Id { get; set; }
    }

    public class DeleteContactHandler : IRequestHandler<DeleteContact, Response>
    {
        private readonly IContactBLL _bll;

        public DeleteContactHandler(IBusinessLogicLayer bll, IMapper mapper)
        {
            _bll = bll;
        }

        public async Task<Response> Handle(DeleteContact request, CancellationToken cancellationToken)
        {
            Response response = new Response();

            await _bll.DeleteContactAsync(request.Id);
            response.Message = "Contact deleted";

            return response;
        }
    }
}
