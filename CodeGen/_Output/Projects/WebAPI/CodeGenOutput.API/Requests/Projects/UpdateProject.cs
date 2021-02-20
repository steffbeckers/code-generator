using AutoMapper;
using CodeGenOutput.API.BLL;
using CodeGenOutput.API.Models;
using CodeGenOutput.API.ViewModels;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ValidationException = CodeGenOutput.API.Validation.ValidationException;

namespace CodeGenOutput.API.Requests.Projects
{
    public class UpdateProject : IRequest<Response>
    {
        public ProjectUpdateVM ProjectUpdateVM { get; set; }
    }

    public class UpdateProjectHandler : IRequestHandler<UpdateProject, Response>
    {
        private readonly IProjectBLL _bll;
        private readonly IMapper _mapper;

        public UpdateProjectHandler(IBusinessLogicLayer bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        public async Task<Response> Handle(UpdateProject request, CancellationToken cancellationToken)
        {
            Project project = await _bll.GetProjectByIdAsync(request.ProjectUpdateVM.Id);
            _mapper.Map(request.ProjectUpdateVM, project);
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
