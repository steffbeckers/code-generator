using AutoMapper;
using CodeGen.API.BLL;
using CodeGen.API.Models;
using CodeGen.API.ViewModels;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CodeGen.API.Requests.Projects
{
    public class CreateProject : IRequest<Response>
    {
        public ProjectCreateVM ProjectCreateVM { get; set; }
    }

    public class CreateProjectHandler : IRequestHandler<CreateProject, Response>
    {
        private readonly IProjectBLL _bll;
        private readonly IMapper _mapper;

        public CreateProjectHandler(IBusinessLogicLayer bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        public async Task<Response> Handle(CreateProject request, CancellationToken cancellationToken)
        {
            Project project = _mapper.Map<Project>(request.ProjectCreateVM);
            project = await _bll.CreateProjectAsync(project);

            return new Response()
            {
                Code = "PROJECT_CREATED",
                Message = "Project created",
                Data = _mapper.Map<ProjectVM>(project)
            };
        }
    }
}
