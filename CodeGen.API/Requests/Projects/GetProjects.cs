using AutoMapper;
using CodeGen.API.DAL;
using CodeGen.API.Models;
using CodeGen.API.ViewModels;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CodeGen.API.Requests.Projects
{
    public class GetProjects : IRequest<Response>
    {
        public string Include { get; set; }
    }

    public class GetProjectsHandler : IRequestHandler<GetProjects, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProjectsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response> Handle(GetProjects request, CancellationToken cancellationToken)
        {
            IRepository<Project> repository = _unitOfWork.GetRepository<Project>();

            IEnumerable<Project> projects = await repository.GetAsync(include: request.Include);

            return new Response() { Data = _mapper.Map<List<ProjectListVM>>(projects).ToList() };
        }
    }
}
