using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Connections.Features;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeGen.API.Hubs
{
    public class RealtimeHub : Hub
    {
        private readonly ILogger<RealtimeHub> _logger;
        private readonly RealtimeHubState _state;

        public RealtimeHub(
            ILogger<RealtimeHub> logger,
            RealtimeHubState state
        )
        {
            _logger = logger;
            _state = state;
        }

        public override async Task OnConnectedAsync()
        {
            HttpContext httpContext = Context.GetHttpContext();
            if (httpContext == null)
            {
                throw new Exception("HttpContext in RealtimeHub.OnConnectedAsync() is null");
            }

            // Check if the connected client is a code generator
            bool isCodeGenerator = httpContext.Request.Query.GetQueryParameterValue<bool>("IsCodeGenerator");
            if (isCodeGenerator)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "code-generators");

                _state.ConnectedClients.Add(new RealtimeHubClient()
                {
                    ConnectionId = Context.ConnectionId,
                    IsCodeGenerator = true
                });

                _logger.LogInformation("RealtimeHub code generator connected: " + Context.ConnectionId);
            }
            else // For normal connections
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "users");

                _state.ConnectedClients.Add(new RealtimeHubClient()
                {
                    ConnectionId = Context.ConnectionId
                });

                _logger.LogInformation("RealtimeHub user connected");
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            if (ex != null)
            {
                _logger.LogError("RealtimeHub client disconnected", ex.Message);
            }
            else
            {
                _logger.LogError("RealtimeHub client disconnected");
            }

            HttpContext httpContext = Context.GetHttpContext();
            if (httpContext == null)
            {
                throw new Exception("HttpContext in RealtimeHub.OnDisconnectedAsync() is null");
            }

            // Check if the connected client is a code generator
            bool isCodeGenerator = httpContext.Request.Query.GetQueryParameterValue<bool>("IsCodeGenerator");
            if (isCodeGenerator)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, "code-generators");

                _logger.LogInformation("RealtimeHub code generator disconnected: " + Context.ConnectionId);
            }
            else // For normal connections
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, "users");

                _logger.LogInformation("RealtimeHub user disconnected");
            }

            RealtimeHubClient clientFromState = _state.ConnectedClients.SingleOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (clientFromState != null)
            {
                _state.ConnectedClients.Remove(clientFromState);
            }

            await base.OnDisconnectedAsync(ex);
        }
    }

    public class RealtimeHubState
    {
        public RealtimeHubState()
        {
            ConnectedClients = new List<RealtimeHubClient>();
        }

        public List<RealtimeHubClient> ConnectedClients { get; private set; }
    }

    public class RealtimeHubClient
    {
        public string ConnectionId { get; set; }
        public bool IsCodeGenerator { get; set; }
    }

    public static class SignalrExtensions
    {
        static public HttpContext GetHttpContext(this HubCallerContext context) =>
           context
             ?.Features
             .Select(x => x.Value as IHttpContextFeature)
             .FirstOrDefault(x => x != null)
             ?.HttpContext;

        static public T GetQueryParameterValue<T>(this IQueryCollection httpQuery, string queryParameterName) =>
           httpQuery.TryGetValue(queryParameterName, out var value) && value.Any()
             ? (T)Convert.ChangeType(value.FirstOrDefault(), typeof(T))
             : default;
    }
}
