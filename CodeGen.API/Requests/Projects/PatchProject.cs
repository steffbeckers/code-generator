using AutoMapper;
using CodeGen.API.DAL;
using CodeGen.API.Models;
using CodeGen.API.ViewModels;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeGen.API.Requests.Projects
{
    public class PatchProject : IRequest<Response>
    {
        public Guid Id { get; set; }
        public JsonPatchDocument<ProjectUpdateVM> PatchDocument { get; set; }
    }

    public class PatchProjectHandler : IRequestHandler<PatchProject, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public PatchProjectHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IMediator mediator
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<Response> Handle(PatchProject request, CancellationToken cancellationToken)
        {
            IRepository<Project> repository = _unitOfWork.GetRepository<Project>();

            Project project = await repository.GetByIdAsync(request.Id);
            ProjectUpdateVM projectUpdateVM = _mapper.Map<ProjectUpdateVM>(project);
            request.PatchDocument.ApplyTo(projectUpdateVM);

            return await _mediator.Send(new UpdateProject() { ProjectUpdateVM = projectUpdateVM }, cancellationToken);
        }
    }
}
