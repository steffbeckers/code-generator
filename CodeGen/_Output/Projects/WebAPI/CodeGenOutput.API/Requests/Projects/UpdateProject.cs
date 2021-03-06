using AutoMapper;
using CodeGenOutput.API.DAL;
using CodeGenOutput.API.Models;
using CodeGenOutput.API.Validation;
using CodeGenOutput.API.ViewModels;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CodeGenOutput.API.Requests.Projects
{
    public class UpdateProject : IRequest<Response>
    {
        public ProjectUpdateVM ProjectUpdateVM { get; set; }
    }

    public class UpdateProjectHandler : IRequestHandler<UpdateProject, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProjectHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response> Handle(UpdateProject request, CancellationToken cancellationToken)
        {
            IRepository<Project> repository = _unitOfWork.GetRepository<Project>();

            Project project = await repository.GetByIdAsync(request.ProjectUpdateVM.Id);
            _mapper.Map(request.ProjectUpdateVM, project);

            ValidationResult validationResult = await Validators.ProjectValidator.ValidateAsync(project, cancellationToken);
            if (!validationResult.IsValid)
            {
                return new Response()
                {
                    Success = false,
                    Code = "PROJECT_INVALID",
                    Message = "Project data invalid",
                    Data = validationResult.Errors
                };
            }

            project = await repository.UpdateAsync(project);
            await _unitOfWork.Commit();

            return new Response()
            {
                Code = "PROJECT_UPDATED",
                Message = "Project updated",
                Data = _mapper.Map<ProjectVM>(project)
            };
        }
    }
}
