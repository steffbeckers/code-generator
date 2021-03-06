using AutoMapper;
using CodeGenOutput.API.DAL;
using CodeGenOutput.API.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeGenOutput.API.Requests.Projects
{
    public class DeleteProject : IRequest<Response>
    {
        public Guid Id { get; set; }
    }

    public class DeleteProjectHandler : IRequestHandler<DeleteProject, Response>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProjectHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response> Handle(DeleteProject request, CancellationToken cancellationToken)
        {
            IRepository<Project> repository = _unitOfWork.GetRepository<Project>();

            await repository.DeleteAsync(request.Id);
            await _unitOfWork.Commit();

            return new Response()
            {
                Code = "PROJECT_DELETED",
                Message = "Project deleted"
            };
        }
    }
}
