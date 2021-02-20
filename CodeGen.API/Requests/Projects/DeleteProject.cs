using AutoMapper;
using CodeGen.API.BLL;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeGen.API.Requests.Projects
{
    public class DeleteProject : IRequest<Response>
    {
        public Guid Id { get; set; }
    }

    public class DeleteProjectHandler : IRequestHandler<DeleteProject, Response>
    {
        private readonly IProjectBLL _bll;

        public DeleteProjectHandler(IBusinessLogicLayer bll, IMapper mapper)
        {
            _bll = bll;
        }

        public async Task<Response> Handle(DeleteProject request, CancellationToken cancellationToken)
        {
            await _bll.DeleteProjectAsync(request.Id);

            return new Response()
            {
                Code = "PROJECT_DELETED",
                Message = "Project deleted"
            };
        }
    }
}
