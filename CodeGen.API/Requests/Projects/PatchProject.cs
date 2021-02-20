using AutoMapper;
using CodeGen.API.BLL;
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
        private readonly IProjectBLL _bll;
        private readonly IMapper _mapper;

        public PatchProjectHandler(IBusinessLogicLayer bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        public async Task<Response> Handle(PatchProject request, CancellationToken cancellationToken)
        {
            Project project = await _bll.GetProjectByIdAsync(request.Id);
            ProjectUpdateVM projectUpdateVM = _mapper.Map<ProjectUpdateVM>(project);
            request.PatchDocument.ApplyTo(projectUpdateVM);
            _mapper.Map(projectUpdateVM, project);
            project = await _bll.UpdateProjectAsync(project);

            return new Response()
            {
                Code = "PROJECT_UPDATED",
                Message = "Project updated",
                Data = _mapper.Map<ProjectVM>(project)
            };
        }
    }
}
