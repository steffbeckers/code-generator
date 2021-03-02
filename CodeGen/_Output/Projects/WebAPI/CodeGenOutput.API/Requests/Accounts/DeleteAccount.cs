using AutoMapper;
using CodeGenOutput.API.BLL;
using CodeGenOutput.API.DAL;
using CodeGenOutput.API.Models;
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
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAccountHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response> Handle(DeleteAccount request, CancellationToken cancellationToken)
        {
            IRepository<Account> repository = _unitOfWork.GetRepository<Account>();

            await repository.DeleteAsync(request.Id);
            await _unitOfWork.Commit();

            return new Response()
            {
                Code = "ACCOUNT_DELETED",
                Message = "Account deleted"
            };
        }
    }
}
