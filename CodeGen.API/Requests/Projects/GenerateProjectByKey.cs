using AutoMapper;
using CodeGen.API.BLL;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeGen.API.Requests.Projects
{
    public class GenerateProjectById : IRequest<Response>
    {
        public Guid Id { get; set; }
    }

    public class GenerateProjectByIdHandler : IRequestHandler<GenerateProjectById, Response>
    {
        private readonly IProjectBLL _bll;
        private readonly IMapper _mapper;

        public GenerateProjectByIdHandler(IBusinessLogicLayer bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = mapper;
        }

        public async Task<Response> Handle(GenerateProjectById request, CancellationToken cancellationToken)
        {
            await _bll.GenerateProjectByIdAsync(request.Id);
            return new Response();
        }
    }
}
