using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Entities
{
    public abstract class AuthAbstractRequestCommand:AbstractRequestCommand
    {
        internal override string GetController()=> EndpointMapping.Auth.ControllerName;
    }
}
