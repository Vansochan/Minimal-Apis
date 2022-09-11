using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace Minimal.Api.Endpoints
{
    public class SwaggerEndpointHandler : AbstractEndpointHandler
    {
        public SwaggerEndpointHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void CreateEndpoints(WebApplication app)
        {
#if DEBUG
            app.UseSwagger();
            app.UseSwaggerUI();
#endif
        }

        public override void CreateService(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer",new OpenApiSecurityScheme()
                {
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Bearer Authorization with JWT token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http
                });
                c.AddSecurityDefinition("AccessCode", new OpenApiSecurityScheme()
                {
                    Scheme = "AccessCode",
                    In = ParameterLocation.Header,
                    Description = "Bearer Authorization with JWT token",
                    Name = "AccessCode",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference()
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme,

                            }
                        },
                        new List<string>()
                    },
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference()
                            {
                                Id = "AccessCode",
                                Type = ReferenceType.SecurityScheme,

                            }
                        },
                        new List<string>()
                    }
                });
                var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                new[]
                {
                    "MinimalApi.xml"
                }.ToList().ForEach(fileName => { c.IncludeXmlComments(Path.Combine(baseDirectory, fileName)); });
                var sb = new StringBuilder();
                sb.AppendLine("A sample API for testing and prototyping Swashbuckle features.");
                sb.AppendLine();
                c.EnableAnnotations();
            });
        }
    }
}