using AutoMapper;
using CodeGenOutput.API.BLL;
using CodeGenOutput.API.DAL;
using CodeGenOutput.API.Models;
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
        private readonly IUnitOfWork _unitOfWork;

        public DeleteContactHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response> Handle(DeleteContact request, CancellationToken cancellationToken)
        {
            IRepository<Contact> repository = _unitOfWork.GetRepository<Contact>();

            await repository.DeleteAsync(request.Id);
            await _unitOfWork.Commit();

            return new Response()
            {
                Code = "CONTACT_DELETED",
                Message = "Contact deleted"
            };
        }
    }
}
