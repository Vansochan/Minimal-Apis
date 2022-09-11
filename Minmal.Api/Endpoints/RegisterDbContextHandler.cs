using Database.Lib.Extensions;

namespace Minimal.Api.Endpoints
{
    public class RegisterDbContextHandler : AbstractEndpointHandler
    {
        public RegisterDbContextHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void CreateEndpoints(WebApplication app)
        {
        }

        public override void CreateService(IServiceCollection services)
        {
            var connectionString = ServiceProvider.GetRequiredService<IConfiguration>().GetConnectionString("Default");
            services.AddIdentityDbService(connectionString);
        }
    }
}