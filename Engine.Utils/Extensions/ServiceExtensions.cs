using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Engine.Utils.Extensions
{
    public class ApiCommandInstance
    {
        public List<IRequestApiCommand> Commands { get; set; }
    }

    public static class ServiceExtensions
    {
        public static void AddRequestApiHandlers(this IServiceCollection services, Type type)
        {
           
            services.AddMediatR(type);
            services.AddValidatorsFromAssemblyContaining(type);
            services.AddTransient(provider =>
            {
                return new ApiCommandInstance()
                {
                    Commands = type.Assembly.ExportedTypes
                        .Where(x => typeof(IRequestApiCommand).IsAssignableFrom(x) &&
                                    !(x.IsInterface || x.IsAbstract))
                        .Select(Activator.CreateInstance).Cast<IRequestApiCommand>().ToList()
                };
            });

            services.AddTransient<IExecuteHandler, ExecuteHandler>();
        }
    }
}