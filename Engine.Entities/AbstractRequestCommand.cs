using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Entities.Enums;

namespace Engine.Entities
{
    public abstract class AbstractRequestCommand
    {
        internal abstract string GetController();
        internal abstract string GetAction();
        internal virtual string GetPrefix() => EndpointMapping.ApiVersion;
        internal virtual bool IsEnableLog => true;
        public virtual long Ts { get; set; }
        public abstract EActions GetEvent();
    }
}
