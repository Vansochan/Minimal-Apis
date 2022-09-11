using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Entities.Enums;

namespace Engine.Entities.Auth
{
    public class CreateUserApiRequest : AuthAbstractRequestCommand
    {
        internal override string GetAction() => EndpointMapping.Auth.List;

        public override EActions GetEvent() => EActions.REGISTER;
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
}