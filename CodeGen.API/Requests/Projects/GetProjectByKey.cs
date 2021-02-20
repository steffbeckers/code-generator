using AutoMapper;
using CodeGen.API.BLL;
using CodeGen.API.Models;
using CodeGen.API.ViewModels;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeGen.API.Requests.Projects
{
    public class GetProjectById : IRequest<Response>
    {
        public Guid Id { get; set; }
        public string Include { get; set; }
    }

    public class GetProjectByIdHandler : IRequestHandler<GetProjectById, Response>
    {
        private readonly IProjectBLL _bll;
        private readonly IMapper _mapper;

        public GetProjectByIdHandler(IBusinessLogicLayer bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        public async Task<Response> Handle(GetProjectById request, CancellationToken cancellationToken)
        {
            Project project = await _bll.GetProjectByIdAsync(request.Id, request.Include);
            if (project == null)
            {
                return new Response() { Success = false, Message = $"Project {request.Id} not found." };
            }

            return new Response() { Data = _mapper.Map<ProjectVM>(project) };
        }
    }
}
