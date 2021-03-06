using AutoMapper;
using CodeGenOutput.API.DAL;
using CodeGenOutput.API.DAL.Repositories;
using CodeGenOutput.API.Models;
using CodeGenOutput.API.ViewModels;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeGenOutput.API.Requests.Projects
{
    public class GetProjectById : IRequest<Response>
    {
        public Guid Id { get; set; }
        public string Include { get; set; }
    }

    public class GetProjectByIdHandler : IRequestHandler<GetProjectById, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProjectByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response> Handle(GetProjectById request, CancellationToken cancellationToken)
        {
            IRepository<Project> repository = _unitOfWork.GetRepository<Project>();

            Project project = await repository.GetByIdAsync(request.Id, request.Include);
            if (project == null)
            {
                return new Response() {
                    Success = false,
                    Code = "PROJECT_NOT_FOUND",
                    Message = $"Project {request.Id} not found."
                };
            }

            return new Response() { Data = _mapper.Map<ProjectVM>(project) };
        }
    }
}
