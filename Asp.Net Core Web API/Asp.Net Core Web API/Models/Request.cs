using Asp.Net_Core_Web_API.Interface;

namespace Asp.Net_Core_Web_API.Models
{
    public class Request : IApplication, IUsers
    {
        Guid guid;
        public Request() : this(Guid.NewGuid())
        {

        }
        public Request(Guid guid)
        {
            this.guid = guid;
        }
        public Guid RequestId => new Guid();
    }
}
