using AutoMapper;
using CodeGenOutput.API.BLL;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeGenOutput.API.Requests.Accounts
{
    public class DeleteAccount : IRequest<Response>
    {
        public Guid Id { get; set; }
    }

    public class DeleteAccountHandler : IRequestHandler<DeleteAccount, Response>
    {
        private readonly IAccountBLL _bll;

        public DeleteAccountHandler(IBusinessLogicLayer bll, IMapper mapper)
        {
            _bll = bll;
        }

        public async Task<Response> Handle(DeleteAccount request, CancellationToken cancellationToken)
        {
            await _bll.DeleteAccountAsync(request.Id);

            return new Response()
            {
                Code = "ACCOUNT_DELETED",
                Message = "Account deleted"
            };
        }
    }
}
