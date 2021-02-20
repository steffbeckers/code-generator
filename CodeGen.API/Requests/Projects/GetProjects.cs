using AutoMapper;
using CodeGen.API.BLL;
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
        private readonly IProjectBLL _bll;
        private readonly IMapper _mapper;

        public GetProjectsHandler(IBusinessLogicLayer bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        public async Task<Response> Handle(GetProjects request, CancellationToken cancellationToken)
        {
            List<Project> projects = (await _bll.GetProjectsAsync(request.Include)).ToList();

            return new Response() { Data = _mapper.Map<List<ProjectListVM>>(projects) };
        }
    }
}
