using Engine.Entities;
using Engine.Entities.Base;
using Engine.Entities.Enums;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Engine.Utils
{
    public interface IRequestApiCommand : IRequest<ResponseBase>
    {
        EActions GetEndPoint();
        void ShareData(dynamic request, Dictionary<string, object> parameters);
    }

    public abstract class AbstractRequestApiCommand<TRequest> : IRequestApiCommand
    {
        public abstract EActions GetEndPoint();
        public TRequest Request { get; private set; }


        public Dictionary<string, object> Parameters { get; private set; }

        public void ShareData(dynamic request, Dictionary<string, object> parameters)
        {
            Parameters = parameters;
            Request = request;
        }
    }

    public abstract class AbstractRequestApiHandler<TCmd, TRequest> : IRequestHandler<TCmd, ResponseBase>
        where TCmd : AbstractRequestApiCommand<TRequest> where TRequest : AbstractRequestCommand
    {
        protected ILogger Logger { get; set; }
        protected IServiceProvider ServiceProvider { get; private set; }

        protected AbstractRequestApiHandler(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            Logger = serviceProvider.GetRequiredService<ILogger<AbstractRequestApiHandler<TCmd, TRequest>>>();
        }


        public abstract Task<ResponseBase> ProcessAsync(TCmd request, CancellationToken cancellationToken);

        public async Task<ResponseBase> Handle(TCmd request, CancellationToken cancellationToken)
        {
            try
            {
                if (await IsEnableValidation())
                {
                    var validator = ServiceProvider.GetRequiredService<IValidator<TRequest>>();
                    var validatorResult = await validator.ValidateAsync(request.Request, cancellationToken);
                    var error = new List<ValidationErrorEntity>();
                    if ((validatorResult?.Errors ?? new List<ValidationFailure>()).Any())
                    {
                        error.AddRange(validatorResult?.Errors?.Select(x => new ValidationErrorEntity()
                            { Field = x.PropertyName, Description = x.ErrorMessage }) ?? Array.Empty<ValidationErrorEntity>());
                    }

                    if (error.Any())
                    {
                        return new ResponseBase()
                        {
                            Code = "WRNG-PARAM",
                            Data = error
                        };
                    }
                }

                return await ProcessAsync(request, cancellationToken);
            }
            catch (Exception e)
            {
                Logger.LogError(e,"API ERROR:{msg}",e.Message);
                throw;
                throw;
            }
        }

        public virtual Task<bool> IsEnableValidation() => Task.FromResult(false);
    }
}