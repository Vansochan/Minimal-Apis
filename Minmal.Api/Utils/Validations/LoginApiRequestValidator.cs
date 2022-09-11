using Engine.Entities.Auth;
using Engine.Utils;
using FluentValidation;

namespace Minimal.Api.Utils.Validations
{
    public class LoginApiRequestValidator:AbstractCustomValidator<LoginApiRequest>
    {
        public LoginApiRequestValidator()
        {
            Load(() =>
            {
                RuleFor(x => x.UserName)
                    .NotEmpty()
                    .WithMessage("User Name is required");
                RuleFor(x => x.Password)
                    .NotEmpty()
                    .WithMessage("Password is required");
            });
        }
    }
}
