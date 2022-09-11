namespace Engine.Entities
{
    public class EndpointMapping
    {
        public const string ApiVersion = "api/v1";
        public class Auth : EndpointMapping
        {
            public const string ControllerName = nameof(Auth);
            public const string ControllerVersion = $"{ApiVersion}/{ControllerName}";
            public const string Login = "login";
            public const string List = "list";
            public const string Register = "register";
        }
    }
}