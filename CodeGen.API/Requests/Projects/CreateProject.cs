using AutoMapper;
using CodeGen.API.DAL;
using CodeGen.API.Models;
using CodeGen.API.Validation;
using CodeGen.API.ViewModels;
using FluentValidation.Results;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateProjectHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response> Handle(CreateProject request, CancellationToken cancellationToken)
        {
            IRepository<Project> repository = _unitOfWork.GetRepository<Project>();

            Project project = _mapper.Map<Project>(request.ProjectCreateVM);

            ValidationResult validationResult = await Validators.ProjectValidator.ValidateAsync(project, cancellationToken);
            if (!validationResult.IsValid) {
                return new Response()
                {
                    Success = false,
                    Code = "PROJECT_INVALID",
                    Message = "Project data invalid",
                    Data = validationResult.Errors
                };
            }

            project = await repository.CreateAsync(project);
            await _unitOfWork.Commit();

            return new Response()
            {
                Code = "PROJECT_CREATED",
                Message = "Project created",
                Data = _mapper.Map<ProjectVM>(project)
            };
        }
    }
}
