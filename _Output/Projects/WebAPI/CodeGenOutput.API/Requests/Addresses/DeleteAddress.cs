using AutoMapper;
using CodeGenOutput.API.BLL;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeGenOutput.API.Requests.Addresses
{
    public class DeleteAddress : IRequest<Response>
    {
        public Guid Id { get; set; }
    }

    public class DeleteAddressHandler : IRequestHandler<DeleteAddress, Response>
    {
        private readonly IAddressBLL _bll;

        public DeleteAddressHandler(IBusinessLogicLayer bll, IMapper mapper)
        {
            _bll = bll;
        }

        public async Task<Response> Handle(DeleteAddress request, CancellationToken cancellationToken)
        {
            await _bll.DeleteAddressAsync(request.Id);

            return new Response()
            {
                Code = "ADDRESS_DELETED",
                Message = "Address deleted"
            };
        }
    }
}
