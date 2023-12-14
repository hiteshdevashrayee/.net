using System.Security.Principal;

namespace JWTLibrary
{
    public class JWTBearerClient : IIdentity
    {
        public string? AuthenticationType { get; set; }

        public bool IsAuthenticated { get; set; }

        public string? Name { get; set; }
    }
}