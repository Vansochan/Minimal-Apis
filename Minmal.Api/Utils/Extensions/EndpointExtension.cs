namespace Minimal.Api.Utils.Extensions
{
    public static class EndpointExtensions
    {
        public static void AddEndpoint(this IServiceCollection services, params Type[] types)
        {
            var provider = services.BuildServiceProvider().GetRequiredService<IServiceProvider>();
            var endpoints = types.SelectMany(type =>
                                type.Assembly.ExportedTypes
                                    .Where(x => typeof(IEndpointHandler).IsAssignableFrom(x) && !x.IsAbstract &&
                                                !x.IsInterface)
                                    .Select(x => Activator.CreateInstance(x, provider)).Cast<IEndpointHandler>())?.ToList() ??
                            new List<IEndpointHandler>();
            endpoints.ForEach(endpoint => endpoint.CreateService(services));
            services.AddSingleton(endpoints as IReadOnlyCollection<IEndpointHandler>);
        }

        public static void UseEndpoint(this WebApplication app)
        {
            var endpoints = app.Services.GetService<IReadOnlyCollection<IEndpointHandler>>()?.ToList() ??
                            new List<IEndpointHandler>();
            endpoints.ForEach(endpoint => { endpoint.CreateEndpoints(app); });
        }
    }
}
