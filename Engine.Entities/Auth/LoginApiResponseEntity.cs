using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Entities.Auth
{
    public class LoginApiResponseEntity
    {
        public Guid AccessCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AccessToken { get; set; }
        public long ExpiredAt { get; set; }

    }
}
