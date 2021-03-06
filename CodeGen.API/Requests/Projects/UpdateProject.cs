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
