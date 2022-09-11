using Database.Lib.Data;
using Database.Lib.Models;
using Engine.Entities.Auth;
using Engine.Entities.Base;
using Engine.Entities.Enums;
using Engine.Utils;
using Engine.Utils.Extensions;
using AutoMapper;
namespace Minimal.Api.Utils.APIs
{
    public class CreateUserApiRequestCommand : AbstractRequestApiCommand<CreateUserApiRequest>
    {
        public override EActions GetEndPoint() => EActions.REGISTER;
    }

    public class
        CreateUserApiRequestHandler : AbstractRequestApiHandler<CreateUserApiRequestCommand, CreateUserApiRequest>
    {
        private readonly IdentityDbContext _identityDbContext;
        private readonly IMapper _mapper;
        public CreateUserApiRequestHandler(IServiceProvider serviceProvider, IdentityDbContext identityDbContext, IMapper mapper) :
            base(serviceProvider)
        {
            _identityDbContext = identityDbContext;
            _mapper = mapper;
        }

        public override async Task<ResponseBase> ProcessAsync(CreateUserApiRequestCommand request,
            CancellationToken cancellationToken)
        {
            var password = StringHelper.GetRandomString();
            var rq = request.Request;
            var hash = password.HashPassword();
            //TODO:Send mail
            var user = new User
            {
                FirstName = rq.FirstName,
                LastName = rq.LastName,
                Email = rq.Email,
                UserName = rq.Email,
                Gender = rq.Gender,
                CreatedAt = DateTimeOffset.UtcNow,
                PasswordHash = hash,
                IsActive = true,
                Phone = rq.Phone
            };
            await _identityDbContext.Set<User>().AddAsync(user, cancellationToken);
            await _identityDbContext.SaveChangesAsync(cancellationToken);
            return new ResponseBase()
            {
                Code = "OK",
                Data = new UserDto()
                {
                    Id = user.Id,
                    UserName = user.UserName
                }
            };
        }
    }
}