namespace Minimal.Api;

public interface IEndpointHandler
{
    void CreateService(IServiceCollection services);
    void CreateEndpoints(WebApplication app);
}

public abstract class AbstractEndpointHandler : IEndpointHandler
{
    protected AbstractEndpointHandler(IServiceProvider serviceProvider)
    {
        Logger = serviceProvider.GetRequiredService<ILogger<AbstractEndpointHandler>>();
        ServiceProvider = serviceProvider;
    }

    protected ILogger Logger { get; }
    protected IServiceProvider ServiceProvider { get; }

    public virtual void CreateService(IServiceCollection services)
    {
    }

    public abstract void CreateEndpoints(WebApplication app);
}