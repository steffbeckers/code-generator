using AutoMapper;
using CodeGenOutput.API.BLL;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeGenOutput.API.Requests.AccountContacts
{
    public class DeleteAccountContact : IRequest<Response>
    {
        public Guid Id { get; set; }
    }

    public class DeleteAccountContactHandler : IRequestHandler<DeleteAccountContact, Response>
    {
        private readonly IAccountContactBLL _bll;

        public DeleteAccountContactHandler(IBusinessLogicLayer bll, IMapper mapper)
        {
            _bll = bll;
        }

        public async Task<Response> Handle(DeleteAccountContact request, CancellationToken cancellationToken)
        {
            await _bll.DeleteAccountContactAsync(request.Id);

            return new Response()
            {
                Code = "ACCOUNTCONTACT_DELETED",
                Message = "AccountContact deleted"
            };
        }
    }
}
