using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Minimal.Api.Endpoints
{
    public class AuthorizationEndpointHandler : AbstractEndpointHandler
    {
        public AuthorizationEndpointHandler(IServiceProvider serviceProvider) : base(
            serviceProvider)
        {
        }

       

        public override void CreateEndpoints(WebApplication app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
            
        }

        public override void CreateService(IServiceCollection services)
        {
            var app = ServiceProvider.GetService<IConfiguration>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateActor = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = app["Jwt:Issuer"],
                    ValidAudience = app["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(app["Jwt:Key"]))
                    
                };
            });
            services.AddAuthorization();
        }
    }
}