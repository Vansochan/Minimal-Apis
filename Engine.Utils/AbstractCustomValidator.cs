using Engine.Entities;
using FluentValidation;

namespace Engine.Utils
{
    public class AbstractCustomValidator<T> : AbstractValidator<T> where T : AbstractRequestCommand
    {
        protected string guid = string.Empty;
        protected T Data;
        private async Task<T> LoadData(T data)
        {
            Data = data;
            await PreLoadAsync();
            return data;
        }
        protected virtual Task PreLoadAsync() => Task.CompletedTask;
        protected void Load(Action action)
        {
            guid = Guid.NewGuid().ToString("N");
            RuleFor(x => x.Ts)
                .NotNull()
                .DependentRules(() =>
                {
                    RuleFor(x => x.Ts)
                        .Must(x => x > 0)
                        .WithMessage("Ts is required");
                })
                .WithMessage("Ts is required");
            WhenAsync(async (x, c) => await LoadData(x) != null, action);
/*#if DEBUG
            action();
#endif*/
        }
    }
}
