using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Database.Lib.Data;
using Database.Lib.Models;
using Engine.Entities.Auth;
using Engine.Entities.Base;
using Engine.Entities.Enums;
using Engine.Utils;
using Engine.Utils.Extensions;
using Microsoft.IdentityModel.Tokens;

namespace Minimal.Api.Utils.APIs
{
    public class LoginApiCommand : AbstractRequestApiCommand<LoginApiRequest>
    {
        public override EActions GetEndPoint() => EActions.LOGIN;
    }

    public class LoginApiHandler : AbstractRequestApiHandler<LoginApiCommand, LoginApiRequest>
    {
        private readonly IdentityDbContext _dbContext;

        public LoginApiHandler(IServiceProvider serviceProvider, IdentityDbContext dbContext) : base(serviceProvider)
        {
            _dbContext = dbContext;
        }

        public override async Task<ResponseBase> ProcessAsync(LoginApiCommand request,
            CancellationToken cancellationToken)
        {
            var config = ServiceProvider.GetRequiredService<IConfiguration>();
            var user = _dbContext.Set<User>().FirstOrDefault(x =>
                x.UserName == request.Request.UserName);
            if (user == null)
            {
                return new ResponseBase()
                {
                    Code = "WRONG-PARAM",
                    Message = "Wrong username or password"
                };
            }

            var verify = user.PasswordHash.VerifyHashedPassword(request.Request.Password);
            if (!verify)
            {
                return new ResponseBase()
                {
                    Code = "WRONG-PARAM",
                    Message = "Wrong username or password"
                };
            }
            var claim = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Gender, user.Gender),
                new Claim(ClaimTypes.Name, $"{user.LastName} {user.FirstName}")
            };
            var expiredDateTime = DateTime.UtcNow.AddMinutes(30);
           
            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: claim,
                expires:expiredDateTime,
                notBefore:DateTime.UtcNow,
                signingCredentials:new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"])),SecurityAlgorithms.HmacSha256)
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            var data = new LoginApiResponseEntity()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                AccessCode = Guid.NewGuid(),
                AccessToken = tokenString,
                ExpiredAt = new DateTimeOffset(expiredDateTime).ToUnixTimeMilliseconds()
            };

            return await Task.FromResult(new ResponseBase()
            {
                Code = "OK",
                Data = data
            });
        }

        public override Task<bool> IsEnableValidation() => Task.FromResult(true);
    }
}