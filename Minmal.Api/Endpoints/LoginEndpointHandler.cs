using Engine.Entities;
using Engine.Entities.Auth;
using Engine.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Minimal.Api.Endpoints
{
    public class LoginEndpointHandler : AbstractEndpointHandler
    {
        public LoginEndpointHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void CreateEndpoints(WebApplication app)
        {
            app.MapPost($"/{EndpointMapping.Auth.ControllerVersion}/{EndpointMapping.Auth.Login}",
                [AllowAnonymous] async (IExecuteHandler handler, LoginApiRequest request) =>
                    await handler.ExecuteAsync(request));
            app.MapPost($"/{EndpointMapping.Auth.ControllerVersion}/{EndpointMapping.Auth.Register}",
                [AllowAnonymous] async (IExecuteHandler handler, CreateUserApiRequest request) =>
                    await handler.ExecuteAsync(request));
        }
    }
}