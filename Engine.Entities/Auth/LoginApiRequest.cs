using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Entities.Enums;

namespace Engine.Entities.Auth
{
    public class LoginApiRequest : AuthAbstractRequestCommand
    {
        internal override string GetAction() => EndpointMapping.Auth.Login;

        public override EActions GetEvent() => EActions.LOGIN;
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}