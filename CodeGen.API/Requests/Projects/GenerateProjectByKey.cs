using AutoMapper;
using CodeGen.API.DAL;
using CodeGen.API.Hubs;
using CodeGen.API.Models;
using CodeGen.API.ViewModels;
using MediatR;
using Microsoft.AspNetCore.SignalR;
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
        private readonly IMediator _mediator;
        private readonly IHubContext<RealtimeHub> _realtimeHub;

        public GenerateProjectByIdHandler(IMediator mediator, IHubContext<RealtimeHub> realtimeHub)
        {
            _mediator = mediator;
            _realtimeHub = realtimeHub;
        }

        public async Task<Response> Handle(GenerateProjectById request, CancellationToken cancellationToken)
        {
            Response getProjectResponse = await _mediator.Send(new GetProjectById() { Id = request.Id });
            if (!getProjectResponse.Success)
            {
                return getProjectResponse;
            }

            ProjectVM projectVM = getProjectResponse.Data as ProjectVM;

            await _realtimeHub.Clients.Group("code-generators").SendAsync("Generate", projectVM.Config);

            return new Response() { Data = projectVM };
        }
    }
}
