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
        public string Code { get; set; }
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
            Response response = new Response();

            await _bll.DeleteAccountAsync(request.Code);
            response.Message = "Account deleted";

            return response;
        }
    }
}
