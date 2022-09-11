using Engine.Entities;
using Engine.Entities.Base;
using Engine.Utils.Extensions;
using MediatR;

namespace Engine.Utils
{
    public interface IExecuteHandler
    {
        public Task<ResponseBase> ExecuteAsync<TRequest>(TRequest request) where TRequest : AbstractRequestCommand;
    }

    internal class ExecuteHandler : IExecuteHandler
    {
        private readonly IMediator _mediator;
        private readonly ApiCommandInstance _commandInstance;

        public ExecuteHandler(IMediator mediator, ApiCommandInstance commandInstance)
        {
            _mediator = mediator;
            _commandInstance = commandInstance;
        }

        public async Task<ResponseBase> ExecuteAsync<TRequest>(TRequest request) where TRequest : AbstractRequestCommand
        {
            var cmd = _commandInstance.Commands.FirstOrDefault(x => x.GetEndPoint() == request.GetEvent());
            if (cmd == null)
                return await Task.FromResult(new ResponseBase()
                {
                    Code = "CMD-404",
                    Message = "Command not found"
                });
            cmd.ShareData(request, new Dictionary<string, object>());
            return await _mediator.Send(cmd);
        }
    }
}