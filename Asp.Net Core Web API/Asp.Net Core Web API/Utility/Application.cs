using Asp.Net_Core_Web_API.Interface;

namespace Asp.Net_Core_Web_API.Utility
{
    public class Application : IApplication
    {
        public Guid RequestId => new Guid();
    }
}
